using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.AddAdjustementToBillings
{
    public class BillingAdjustmentPeriods
    {
        public int policyNo { get; set; }
        public string legacyPolNo { get; set; }
        public string referenceNO { get; set; }
        public int billingPeriod { get; set; }
        public DateTime raisedDate { get; set; }
        public string mandateType { get; set; }
        public string description { get; set; }
        public string paymentType { get; set; }
        public int premiumAmount { get; set; }
        public int amountPaid { get; set; }
        public DateTime effectiveDate { get; set; }
    }
}
