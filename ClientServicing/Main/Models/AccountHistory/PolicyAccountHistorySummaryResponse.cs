using ClientServicing.Main.Models.General;

namespace ClientServicing.Main.Models.AccountHistory
{
    public class PolicyAccountHistorySummaryResponse
    {
        public ExecutionOutcome executionOutcome { get; set; }
        public List<AccountingHistoryPolicyResults> data { get; set; }
    }
}
