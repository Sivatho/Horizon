using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Models.General;

namespace ClientServicing.Main.Models.Policy
{
    public class GetUnmentPremiumResponse
    {
        public ExecutionOutcome executionOutcome { get; set; }
        public UnmetPremiumData data { get; set; }
    }
    public class UnmetPremiumData
    {
        public List<TotalUnmetPremiumResult> TotalUnmetPremiumResult { get; set; }
        public List<UnmetPremiumSummary> UnmetPremiumSummary { get; set; }
    }
    public class TotalUnmetPremiumResult
    {
        public int numberOfMonths { get; set; }
        public double? totalAmountMissed { get; set; }
        public string description { get; set; }
    }
    public class UnmetPremiumSummary
    {
        public int policyNo { get; set; }
        public string legacy_Pol_No { get; set; }
        public string month { get; set; }
        public DateTime paymentDate { get; set; }
        public int trackingDays { get; set; }
        public string paymentType { get; set; }
        public string description { get; set; }
        public double premiumAmount { get; set; }
        public double amountPaid { get; set; }
    }
}
