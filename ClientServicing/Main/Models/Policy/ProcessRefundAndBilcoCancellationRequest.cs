using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.Policy
{
    public class ProcessRefundAndBilcoCancellationRequest
    {
        public int billingSubmitted { get; set; }
        public int changeType { get; set; }
        public int refundAvail { get; set; }
        public int bilcoCancellationOnly { get; set; }
        public int policyNo { get; set; }
        public int refundStatus { get; set; }
        public int refundAmount { get; set; }
    }
}
