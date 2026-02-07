using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Models.General;

namespace ClientServicing.Main.Models.RemovalFromBillings
{
    public class RemovalFromBillingsHistoryResponse
    {
        public ExecutionOutcome executionOutcome { get; set; }
        public RemovalFromBillingHistory[] data { get; set; }
    }
    public class RemovalFromBillingHistory {
        public int removeID { get; set; }
        public int policyNo { get; set; }
        public int removeCD { get; set; }
        public DateTime removalDate { get; set; }
        public double premiumAmt { get; set; }
        public DateTime effDate { get; set; }
        public DateTime endDate { get; set; }
        public string? months { get; set; }
        public int statusCD { get; set; }
        public string s_Desc { get; set; }
        public string comments { get; set; }
        public string audModUser { get; set; }
    }
}
