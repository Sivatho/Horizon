using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.CancelPolicy
{
    public class UpdateCancelPolicyDetailsRequest
    {
        public int policyNo { get; set; }
        public int statusCd { get; set; }
        public int reasonCd { get; set; }
        public int subReasonCd { get; set; }
        public DateTime effectiveDate { get; set; }
        public string comment { get; set; }
        public string userID { get; set; }
        public int paymentTypeCD { get; set; }
        public string providerReference { get; set; }
    }
}
