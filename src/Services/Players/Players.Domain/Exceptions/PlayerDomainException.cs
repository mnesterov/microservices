namespace Players.Domain.Exceptions
{
    public abstract class PlayerDomainException : Exception
    {
        public PlayerDomainException()
        { }

        public PlayerDomainException(string message)
            : base(message)
        { }

        public PlayerDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
