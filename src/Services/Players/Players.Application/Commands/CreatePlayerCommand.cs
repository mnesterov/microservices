using Players.Application.Dtos;

namespace Players.Application.Commands
{
    public class CreatePlayerCommand : ICommand<PlayerDto>
    {
        public CreatePlayerCommand() 
        { 
        }

        public CreatePlayerCommand(DateTime birthday, string firstName, string lastName, int? teamId, int contractLength, double salary)
        {
            Birthday = birthday;
            FirstName = firstName;
            LastName = lastName;
            TeamId = teamId;
            ContractLength = contractLength;
            Salary = salary;
        }

        public DateTime Birthday { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? TeamId { get; set; }
        public int ContractLength { get; set; }
        public double Salary { get; set; }
    }
}
