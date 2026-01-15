using ClientServicing.Main.Models.General;

namespace ClientServicing.Main.Models.Bank
{
    public class FetchBanksResponse
    {
        public ExecutionOutcome responseMessage { get; set; }
        public List<FetchBanksRequest> data { get; set; }
    }
}
