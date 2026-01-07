using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Models.General;

namespace ClientServicing.Main.Models.Policy
{
    public class CheckRestartEligibilityResponse
    {
        public ExecutionOutcome executionOutcome { get; set; }
        public CheckRestartEligibilityData data { get; set; }
    }
    public class CheckRestartEligibilityData
    {
        public bool isEligibile { get; set; }
        public string? message { get; set; }
    }
}
