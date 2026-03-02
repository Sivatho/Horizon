using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.Debicheck
{
    public class DebicheckRetryCheckStatusResponseData
    {
        public bool success { get; set; }
        public bool didError { get; set; }
        public string message { get; set; }
        public DebitCheckRetryStatusResponseDataModel data { get; set; }
    }
    public class DebitCheckRetryStatusResponseDataModel {
        public string mandateType { get; set; }
        public string? statusReason { get; set; }
    }
}
