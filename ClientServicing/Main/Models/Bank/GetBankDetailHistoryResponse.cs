using ClientServicing.Main.Models.General;

namespace ClientServicing.Main.Models.Bank
{
    public class GetBankDetailHistoryResponse
    {
        public ExecutionOutcome executionOutcome { get; set; }
        public List<GetBankDetailHistory> data { get; set; }
    }
}
