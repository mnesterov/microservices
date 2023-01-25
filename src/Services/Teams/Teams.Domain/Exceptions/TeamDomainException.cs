namespace Teams.Domain.Exceptions
{
    public abstract class TeamDomainException : Exception
    {
        public TeamDomainException()
        { }

        public TeamDomainException(string message)
            : base(message)
        { }

        public TeamDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
