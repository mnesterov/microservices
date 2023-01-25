using Microsoft.EntityFrameworkCore;
using Players.Domain.Models.PlayerAggregate;
using Players.Domain.Common;
using MediatR;
using Players.Infrastructure.EntityTypeConfigurations.Domain;
using Players.Infrastructure.EntityTypeConfigurations;
using Players.Application.Commands.IdentifiedCommand.Request;
using Microsoft.EntityFrameworkCore.Storage;
using Players.Infrastructure.IntegrationEvents.EventLog;
using Players.Application.Exceptions;

namespace Players.Infrastructure;

public class AppDbContext : DbContext, IUnitOfWork
{
    private readonly IMediator _mediator;

    public AppDbContext(DbContextOptions<AppDbContext> options, IMediator mediator) : base(options)
    {
        _mediator = mediator;
    }

    public IDbContextTransaction CurrentTransaction { get; private set; }
    public bool TransactionInProgress => CurrentTransaction != null;

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        //Dispatch domain events RIGHT before commit for data consistency purposes.
        await _mediator.DispatchDomainEventsAsync(this);
        
        await base.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        if (CurrentTransaction != null)
            throw new AppException("Concurrent command transaction is not allowed within the same scope.");

        CurrentTransaction = await Database.BeginTransactionAsync(System.Data.IsolationLevel.ReadCommitted);

        return CurrentTransaction;
    }

    public async Task CommitTransactionAsync()
    {
        if (CurrentTransaction == null)
            throw new AppException("No transactions in progress to commit.");

        try
        {
            await SaveChangesAsync();
            await CurrentTransaction.CommitAsync();
        }
        catch
        {
            CurrentTransaction.Rollback();
            throw;
        }
        finally
        {
            if (CurrentTransaction != null)
            {
                CurrentTransaction.Dispose();
                CurrentTransaction = null;
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new PlayerTypeConfiguration().Configure(modelBuilder.Entity<Player>());
        
        new IdentifiedCommandRequestTypeConfiguration().Configure(modelBuilder.Entity<IdentifiedCommandRequest>());
        new IntegrationEventLogTypeConfiguration().Configure(modelBuilder.Entity<IntegrationEventLog>());
    }

}
