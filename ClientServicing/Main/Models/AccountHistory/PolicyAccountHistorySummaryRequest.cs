using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace ClientServicing.Main.Models.AccountHistory
{
    public class PolicyAccountHistorySummaryRequest
    {
        [JsonPropertyName("policyNo")]
        public int? PolicyNo { get; set; }

        [JsonPropertyName("billingPeriod")]
        public short? BillingPeriod { get; set; }
    }
}
