using ClientServicing.Main.Models.Bank;
using ClientServicing.Main.Models.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.AccountHistory
{
    public class ManualReceiptInforUpsertResponse
    {
        public ExecutionOutcome responseMessage { get; set; }
        public List<FetchBanksRequest> data { get; set; }
    }
}
