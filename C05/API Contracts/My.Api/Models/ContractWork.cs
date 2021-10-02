using My.Api.Contracts;

namespace My.Api.Models
{
    public class ContractWork
    {
        public int Total { get; set; }
        public int Done { get; set; }

        public WorkState State => 
            Done == 0 ? WorkState.New : 
            Done == Total ? WorkState.Completed : 
            WorkState.InProgress;
    }
}
