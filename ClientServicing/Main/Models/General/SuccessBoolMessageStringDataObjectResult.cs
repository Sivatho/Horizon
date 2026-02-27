using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.General
{
    public class SuccessBoolMessageStringDataObjectResult
    {
        public bool success { get; set; }
        public string message { get; set; }
        public MandateTypeStringStatusReasonObjectData data { get; set; }
    }

}
