using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.Debicheck
{
    public class DebicheckRetryCheckStatusResponse
    {
        public string policyNumber { get; set; }
        public string debiCheckStatus { get; set; }
        public int retryCount { get; set; }
        public bool retryAllowed { get; set; }
        public DateTime latestMandateCreatedAt { get; set; }
    }
}
