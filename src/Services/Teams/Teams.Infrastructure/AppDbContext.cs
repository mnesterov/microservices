using Microsoft.EntityFrameworkCore;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using Teams.Domain.Common;
using Teams.Application.Exceptions;
using Teams.Infrastructure.EntityTypeConfigurations.Domain;
using Teams.Domain.Models.CityAggregate;
using Teams.Domain.Models.TeamAggregate;
using Teams.Application.Commands.IdentifiedCommand.Request;
using Teams.Infrastructure.EntityTypeConfigurations;
using Teams.Infrastructure.IntegrationEvents.EventLog.Models;

namespace Teams.Infrastructure;

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
        new CityEntityTypeConfiguration().Configure(modelBuilder.Entity<City>());
        new TeamEntityTypeConfiguration().Configure(modelBuilder.Entity<Team>());

        new IdentifiedCommandRequestTypeConfiguration().Configure(modelBuilder.Entity<IdentifiedCommandRequest>());
        new IntegrationEventLogTypeConfiguration().Configure(modelBuilder.Entity<IntegrationEventLog>());

        //SeedData(modelBuilder);
    }

    //migrations data seed
    private static void SeedData(ModelBuilder modelBuilder)
    {
        var atl = new City(1, "Atlanta");
        var bos = new City(2, "Boston");
        var cha = new City(3, "Charlotte");
        var chi = new City(4, "Chicago");
        var cle = new City(5, "Cleveland");
        var det = new City(6, "Detroit");
        var la = new City(7, "Los Angeles");

        modelBuilder.Entity<City>().HasData(
            atl,
            bos,
            cha,
            chi,
            cle,
            det,
            la);

        modelBuilder.Entity<Team>().HasData(
            new Team(1, "Hawks", 1),
            new Team(2, "Celtics", 2),
            new Team(3, "Hornets", 3),
            new Team(4, "Bulls", 4),
            new Team(5, "Cavaliers", 5),
            new Team(6, "Pistons", 6),
            new Team(7, "Clippers", 7),
            new Team(8, "Lakers", 7));
    }
}
