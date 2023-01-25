namespace Teams.Domain.Common
{
    public abstract class EntityWithKey<T> : Entity
    {
        public T Id { get; protected set; }

        public void AssignId(T id)
        {
            Id = id;
        }
    }
}
