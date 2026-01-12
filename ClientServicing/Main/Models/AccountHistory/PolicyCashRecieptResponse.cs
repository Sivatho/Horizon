using ClientServicing.Main.Models.General;
using System.Collections.Generic;

namespace ClientServicing.Main.Models.AccountHistory
{
    public class PolicyCashRecieptResponse
    {
        // Provide defaults so consumers can rely on non-null values
        public ExecutionOutcome? responseMessage { get; set; }
        public List<PolicyCashRecieptResponse>? data { get; set; }
    }
}

