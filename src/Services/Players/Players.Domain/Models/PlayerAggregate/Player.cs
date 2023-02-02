using Players.Domain.Common;
using Players.Domain.Events;

namespace Players.Domain.Models.PlayerAggregate;

public class Player : EntityWithKey<int>, IAggregateRoot
{
    #region Constructor

    private int? _teamId;

    protected Player()
    {
    }

    public Player(CreateData data)
    {
        Birthday = data.Birthday;
        FirstName = data.FirstName;
        LastName = data.LastName;
        SalaryInfo = new SalaryInfo(data.Salary, data.ContractLength);

        AssignToNewTeam(data.TeamId);
    }

    public Player(int id, DateTime birthday, string firstName, string lastName, double salary)
    {
        Id = id;
        Birthday = birthday;
        FirstName = firstName;
        LastName = lastName;
        SalaryInfo = new SalaryInfo(salary, 1);
    }

    #endregion

    #region Public Properties

    public DateTime Birthday { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string FullName => $"{FirstName} {LastName}";
    public SalaryInfo SalaryInfo { get; private set; }
    public int? TeamId => _teamId;

    #endregion

    #region Public Methods

    public void AssignToNewTeam(int? teamId)
    {
        if (_teamId != teamId)
        {
            AddDomainEvent(new ChangePlayerTeamDomainEvent(this, teamId, _teamId));
            _teamId = teamId;
        }
    }

    public void AssignBackToOldTeam(int? teamId)
    {
        _teamId = teamId;
    }

    public void SetSalary(SalaryInfo info)
    {
        SalaryInfo = info;
    }

    #endregion

    #region Create Data

    public class CreateData
    {
        public DateTime Birthday { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? TeamId { get; set; }
        public int ContractLength { get; set; }
        public double Salary { get; set; }
    }

    #endregion
}