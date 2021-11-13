namespace WebApi.Models
{
    public class Contract
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ContractWork Work { get; set; }
        public ContactInformation PrimaryContact { get; set; }
    }
}
