namespace Players.Application.Commands.IdentifiedCommand.Request
{
    public class IdentifiedCommandRequest
    {
        public IdentifiedCommandRequest(Guid id, string name)
        {
            Id = id;
            Name = name;
            Created = DateTime.Now;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public DateTime Created { get; private set; }
    }
}
