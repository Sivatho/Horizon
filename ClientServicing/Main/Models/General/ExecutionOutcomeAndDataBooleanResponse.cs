using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.General
{
    public class ExecutionOutcomeAndDataBooleanResponse
    {
        public ExecutionOutcome executionOutcome { get; set; }
        public bool? data { get; set; }
    }
}
