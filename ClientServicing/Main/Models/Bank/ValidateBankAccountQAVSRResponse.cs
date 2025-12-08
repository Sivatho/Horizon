using ClientServicing.Main.Models.General;

namespace ClientServicing.Main.Models.Bank
{
    public class ValidateBankAccountQAVSRResponse
    {
        public bool isValid { get; set; }
        public bool shouldUpdateAccountType { get; set; }
        public bool overrideIsValid { get; set; }
        public string message { get; set; }
        public int fraudsterFailure { get; set; }
        public TestResult softyCompResult { get; set; }
        public TestResult fraudsterResult { get; set; }
        public TestResult d3BlackListResult { get; set; }
        public AVSRResult avsrResult { get; set; }
        public CorrectBankAccountType correctBankAccountType { get; set; }
        public string correctBankName { get; set; }
    }
}