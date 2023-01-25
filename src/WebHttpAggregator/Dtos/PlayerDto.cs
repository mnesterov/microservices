namespace WebHttpAggregator.Dtos;

public class PlayerDto
{
    public int Id { get; set; }
    public DateTime Birthday { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int? TeamId { get; set; }
    public int ContractLength { get; set; }
    public double Salary { get; set; }
}

