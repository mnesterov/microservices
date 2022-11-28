namespace Domain.Models;

public abstract class Entity<T>
{
    public T Id { get; protected set; }

    public void AssignId(T id)
    {
        Id = id;
    }
}

