using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.Bank
{
    public class ValidateAccountNumberUsageLimitResponse
    {
        public ExecutionOutcome executionOutcome { get; set; }
        public int totalPolicies { get; set; }
        public bool limitExceeded { get; set; }
    }
}
