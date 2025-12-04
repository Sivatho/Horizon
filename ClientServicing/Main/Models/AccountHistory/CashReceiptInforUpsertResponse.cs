
using ClientServicing.Main.Models.Bank;
using ClientServicing.Main.Models.General;



namespace ClientServicing.Main.Models.AccountHistory

{
    public class CashReceiptInforUpsertResponse
    {
        public ExecutionOutcome responseMessage { get; set; }
        public List<FetchBanksRequest> data { get; set; }
    }
}
