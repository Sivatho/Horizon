using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Models.General;

namespace ClientServicing.Main.Models.CCMEvent
{
    public class GetEventDetailConstructBPEResponse
    {
        public ExecutionOutcome executionOutcome { get; set; }
        public GetEventDetailConstructBPE data { get; set; }
    }

    public class GetEventDetailConstructBPE
    {
        public string jsonData { get; set; }
        public bool success { get; set; }
        public string message { get; set; }
    }
}
    