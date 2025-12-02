using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.General
{
    public class TestResult
    {
        public bool wasTestResultOverridden { get; set; }
        public bool wasTestPerformed { get; set; }
        public bool isValid { get; set; }
        public string message { get; set; }
        public int fraudsterFailureType { get; set; }
    } 
}
