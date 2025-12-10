using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.AddAdjustementToBillings
{
    public class AddAdjustementToBillingsRequest
    {
        public BillingsAdjustmentInformation billingsAdjustmentInformation { get; set; }
        public List<BillingAdjustmentPeriods> billingAdjustmentPeriods { get; set; }
    }
}
