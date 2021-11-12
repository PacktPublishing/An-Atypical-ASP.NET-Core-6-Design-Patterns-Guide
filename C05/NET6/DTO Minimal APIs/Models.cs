namespace DTO.Data.Models;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<Contract> Contracts { get; set; }
}

public class Contract
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public ContractWork Work { get; set; }
    public ContactInformation PrimaryContact { get; set; }
}

public class ContractWork
{
    public int Total { get; set; }
    public int Done { get; set; }

    public WorkState State =>
        Done == 0 ? WorkState.New :
        Done == Total ? WorkState.Completed :
        WorkState.InProgress;
}

public class ContactInformation
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
}

public enum WorkState
{
    New,
    InProgress,
    Completed
}
