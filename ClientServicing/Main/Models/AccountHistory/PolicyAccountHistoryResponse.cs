using ClientServicing.Main.Models.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.AccountHistory
{
    public class PolicyAccountHistoryResponse
    {
        public ExecutionOutcome responseMessage { get; set; }

        // Add this 'data' property because your test expects a top-level "data" JSON array
        public List<PolicyAccountHistoryRequest> data { get; set; }

        // keep existing names if the API can return them; optional
        public List<PolicyAccountHistoryRequest> accountingHistoryPaymentResults { get; set; }
        public List<PolicyAccountHistoryRequest> accountingHistoryPolicyResults { get; set; } 
    }
}
