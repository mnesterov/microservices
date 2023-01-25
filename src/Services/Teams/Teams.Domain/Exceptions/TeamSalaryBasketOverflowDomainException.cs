namespace Teams.Domain.Exceptions
{
    public class TeamSalaryBasketOverflowDomainException : TeamDomainException
    {
        public TeamSalaryBasketOverflowDomainException()
        { }

        public TeamSalaryBasketOverflowDomainException(string message)
            : base(message)
        { }

        public TeamSalaryBasketOverflowDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
