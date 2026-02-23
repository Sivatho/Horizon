using ClientServicing.Main.Models.General;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.AddAdjustementToBillings
{
    public class AddAdjustementToBillingsRequest
    {
        public ExecutionOutcome executionOutcome { get; set; }
       
        public AddAdjustementToBillingsRequestData data { get; set; }

        public class AddAdjustementToBillingsRequestData {

            public BillingsAdjustmentInformationRequest[] billingsadjustmentinformation { get; set;}
        public BillingAdjustmentPeriodsRequest[] billingadjustmentperiods { get;set }
	}
        public class BillingsAdjustmentInformationRequest
		{

            public int policyNo { get; set; }
			public DateTime effectiveDate { get; set; }
            public DateTime adjustmentDateFrom { get; set; }
            public Double adjustmentAmount { get; set; }
            public Double totalAdjAmount { get; set; }
            public int adjustedMonthCnt { get; set; }
            public DateTime adjustmentEndDate { get; set; }
            public String comment { get; set; }
            public String actionID { get; set; }


		}
        public class BillingAdjustmentPeriodsRequest {
            public int policyNo { get; set; }
            public String legacyPolNo { get;set }
            public String referenceNO { get; set; }
            public int billingPeriod { get;set }
            public DateTime raisedDate { get; set; }
            public String mandateType { get; set; }
            public String paymentType { get; set; }
            public Double premiumAmount { get; set; }
            public Double amountPaid { get; set; }
            public DateTime effectiveDate { get; set; }






		}
}
