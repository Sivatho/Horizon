using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.AddAdjustementToBillings
{
    public class GetAdjustedPeriodsRequest
    {
        public int adjustmentID { get; set; }
        public int policyNo { get; set; }
        public DateTime adjustmentDateFrom { get; set; }
        
    }
}
