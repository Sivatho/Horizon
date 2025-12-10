using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.AddAdjustementToBillings
{
    public class BillingsAdjustmentInformation
    {
        public int policyNo { get; set; }
        public DateTime effectiveDate { get; set; }
        public DateTime adjustmentDateFrom { get; set; }
        public int adjustmentAmount { get; set; }
        public int totalAdjAmount { get; set; }
        public int adjustedMonthCnt { get; set; }
        public DateTime adjustmentEndDate { get; set; }
        public string comment { get; set; }
        public string actionID { get; set; }
    }
}
