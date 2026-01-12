using ClientServicing.Main.Models.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.AccountHistory
{
    public class PolicyCashRecieptRequest
    {
        [JsonPropertyName("policyNo")]
        public int policyNo { get; set; }

         [JsonPropertyName("legacyPolicyNumber")]
        public string legacyPolicyNumber { get; set; }
    }
}
