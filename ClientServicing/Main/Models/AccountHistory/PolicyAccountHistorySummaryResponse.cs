using ClientServicing.Main.Models.General;

namespace ClientServicing.Main.Models.AccountHistory
{
    public class PolicyAccountHistorySummaryResponse
    {
        public ExecutionOutcome responseMessage { get; set; }
        public List<PolicyAccountHistorySummaryRequest> data { get; set; }

    }
}
