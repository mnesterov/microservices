namespace Teams.Domain.Exceptions
{
    public class CityArgumentNullDomainException : TeamDomainException
    {
        public CityArgumentNullDomainException()
        { }

        public CityArgumentNullDomainException(string message)
            : base(message)
        { }

        public CityArgumentNullDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
