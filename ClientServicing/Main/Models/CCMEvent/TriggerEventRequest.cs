using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.CCMEvent
{
    public class TriggerEventRequest
    {
        public int policyNo { get; set; }
        public string? legacyPolicyNo { get; set; }
        public int partnerCd { get; set; }
        public int eventTypeCd { get; set; }
        public string? eventTypeDesc { get; set; }
        public int quoteId { get; set; }
        public DateTime effectiveDate { get; set; }
        public string? userId { get; set; }
    }
}
