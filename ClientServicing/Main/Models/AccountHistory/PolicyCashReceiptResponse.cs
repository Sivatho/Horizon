using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Models.General;

namespace ClientServicing.Main.Models.AccountHistory
{
    public class PolicyCashReceiptResponse
    {
        public ExecutionOutcome executionOutcome { get; set; }
        public List<PolicyCashReceipt> data { get; set; }
    }
    public class PolicyCashReceipt {
        public int policyNo { get; set; }
        public string reference { get; set; }
        public int billingPeriod { get; set; }
        public DateTime raisedDate { get; set; }
        public string mandateType { get; set; }
        public string description { get; set; }
        public int premium { get; set; }
        public int? susTransTotal { get; set; }
    }
}
