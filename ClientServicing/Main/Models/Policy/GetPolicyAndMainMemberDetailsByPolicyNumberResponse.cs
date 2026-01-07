using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Models.General;

namespace ClientServicing.Main.Models.Policy
{
    public class GetPolicyAndMainMemberDetailsByPolicyNumberResponse
    {
        public ExecutionOutcome executionOutcome { get; set; }
        public PolicyAndMainMemberDetails data { get; set; }
    }
    public class PolicyAndMainMemberDetails {
        public int policy_NO { get; set; }
        public int entityNo { get; set; }
        public string legacy_Pol_No { get; set; }
        public double annualIncrease { get; set; }
        public DateTime dateOfCommencement { get; set; }
        public DateTime? reInstatedDate { get; set; }
        public DateTime? lapsedDate { get;set; }
        public string venue { get; set; }
        public string salesPerson { get; set; }
        public int campaignCode { get; set; }
        public int policyFee { get; set; }
        public DateTime captureDate { get; set; }
        public int preferedCommunicationMethod { get; set; }
        public string masterContract { get; set; }
        public string title { get; set; }
        public int titleID { get; set; }
        public string firstname { get; set; }
        public string surname { get; set; }
        public string legalRefNo { get; set; }
        public int legalNumberType { get; set; }
        public DateTime dateOfBirth { get; set; }
        public int? preferredTelTypeCd { get; set; }
        public string? faxNumber { get; set; }
        public string homeNumber { get; set; }
        public string? emailAddress { get; set; }
        public string cellNumber { get; set; }
        public string? workNumber { get; set; }
        public string? alternateNumber {  get; set; }
        public string? whatsappNumber { get; set; }
        public string physicalAddress1 { get; set; }
        public string? physicalAddress2 { get; set; }
        public string physicalSuburb { get; set; }
        public string physicalTown { get; set; }
        public string physicalPostalCode { get; set; }
        public string postalAddress1 { get; set; }
        public string? postalAddress2 { get; set; }
        public string postalSuburb { get; set; }
        public string postalTown { get; set; }
        public string postalPostalCode { get; set; }
        public int genderCD { get; set; }
        public int smokerCd { get; set; }
        public string smokerDescr { get; set; }
        public DateTime? lastBillingDate { get; set; }
        public DateTime? lastPaidDate { get; set; }
        public DateTime nextBillingDate { get; set; }
        public double policyPremiumAmount { get; set; }
        public int premiumCount { get; set; }
        public string paymentFrequency { get; set; }
    }
}
