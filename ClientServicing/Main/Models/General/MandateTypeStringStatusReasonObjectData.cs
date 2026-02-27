using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.General
{
    public class MandateTypeStringStatusReasonObjectData
    {
        public string mandateType { get; set; }
        public List<statusReason> listOfStatusReason { get; set; }
    }
    public class statusReason {
        public string message { get; set; }
        public string policyNumber { get; set; }
    }
}
