using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Models.General;

namespace ClientServicing.Main.Models.Policy
{
    public class StoreOTPResponse
    {
        public ExecutionOutcome executionOutcome { get; set; }
        public int data { get; set; }
    }
}
