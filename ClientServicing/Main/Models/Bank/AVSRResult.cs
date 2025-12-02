using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.Bank
{
    public class AVSRResult
    {
        public string qLinkAvsrCheckId { get; set; }
        public string? errorCode { get; set; }
        public string? errorDescription { get; set; }
        public string? sessionId { get; set; }
        public string? accountStatusId { get; set; }
        public bool wasCachedResultUsed { get; set; }
        public bool wasBankAccountFound { get; set; }
        public bool isBankAccountOpen { get; set; }
        public bool doesBankAccountTypeMatch { get; set; }
        public bool doesInitialsMatch { get; set; }
        public bool doesIdentityNumberMatch { get; set; }
        public bool doesNameMatch { get; set; }
        public bool doesAcceptsDebits { get; set; }
        public bool doesAcceptsCredits { get; set; }
        public bool didTimeout { get; set; }
        public bool accountLengthMatch { get; set; }
        public bool missingParameter { get; set; }
        public bool doesPhoneMatch { get; set; }
        public bool doesEmailMatch { get; set; }
        public bool hasBankAccountBeenOpenForMoreThan3Months { get; set; }
        public DateTime responseDate { get; set; }
        public bool wasTestPerformed { get; set; }
        public bool isValid { get; set; }
        public string? message { get; set; }
        public bool isForcedSuccessResponse { get; set; }
    }
}
