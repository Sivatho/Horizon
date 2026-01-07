using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Models.General;

namespace ClientServicing.Main.Models.Policy
{
    public class GetCustomerPolicyInfoByEntityResponse 
    {
        public ExecutionOutcome executionOutcome { get; set; }
        public List<CusomterPolicyInfo>? data { get; set; }
    }
    public class CusomterPolicyInfo {
        public int policyNo { get; set; }
        public int? ifaNo { get; set; }
        public string channelDescr { get; set; }
        public string productDescr { get; set; }
        public string planTypeDescr { get; set; }
        public string policyStatus { get; set; }
        public int statusCd { get; set; }
        public DateTime dateOfCommencement { get; set; }
        public string payer { get; set; }
        public int policyPremium { get; set; }
        public DateTime billedTo { get; set; }
        public DateTime paidTo { get; set; }
        public int? premiumCount { get; set; }
        public int premiumFrequency { get; set; }
        public string salesPerson { get; set; }
        public string debiCheckStatus { get; set; }
        public string legacyPolicyNo { get; set; }
        public DateTime statusDate { get; set; }
        public int partnerCD { get; set; }
        public int inspiratorNo { get; set; }
    }
}
