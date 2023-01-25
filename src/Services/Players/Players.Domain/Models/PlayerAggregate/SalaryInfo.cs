using Players.Domain.Common;

namespace Players.Domain.Models.PlayerAggregate
{
    public class SalaryInfo : ValueObject
    {
        public double ContractAnnualSalary { get; private set; }
        public int ContractLength { get; private set; }

        protected SalaryInfo()
        { 
        }

        public SalaryInfo(double contractAnnualSalary, int contractLength)
        {
            ContractAnnualSalary = contractAnnualSalary;
            ContractLength = contractLength;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ContractAnnualSalary;
            yield return ContractLength;
        }
    }
}
