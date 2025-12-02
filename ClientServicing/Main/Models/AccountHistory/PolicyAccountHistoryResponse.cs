using ClientServicing.Main.Models.General;

namespace ClientServicing.Main.Models.Bank
{
    public class PolicyAccountHistoryResponse
    {
        public ExecutionOutcome responseMessage { get; set; }
        public List<PolicyAccountHistoryRequest> data { get; set; }
    }
}
