using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Models.General;

namespace ClientServicing.Main.Models.Bank
{
    public class ValidateBankAccountResponse
    {
        public bool isValid { get; set; }
        public bool shouldUpdateAccountType { get; set; }
        public bool overrideIsValid { get; set; }
        public string message { get; set; }
        public int fraudsterFailure { get; set; }
        public string? correctBankName { get; set; }
        public TestResult softyCompResult { get; set; }
        public TestResult fraudsterResult { get; set; }
        public TestResult d3BlackListResult { get; set; }
        public AVSRResult avsrResult { get; set; }
    }
}
