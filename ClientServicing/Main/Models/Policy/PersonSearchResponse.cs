using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Models.General;

namespace ClientServicing.Main.Models.Policy
{
    public class PersonSearchResponse
    {
        public ExecutionOutcome executionOutcome { get; set; }
        public List<PersonSearchDetails> personSearchDetails { get; set; }
    }
    public class PersonSearchDetails {
        public int entityID { get; set; }
        public int? ifaNo { get; set; }
        public int entityNo { get; set; }
        public string entityName { get; set; }
        public string entitySurname { get; set; }
        public DateTime entityDOB { get; set; }
        public string legalRefNo { get; set; }
        public string legalRefNoType { get; set; }
        public int citizenshipCD { get; set; }
        public string alpha3Code { get; set; }
        public string citizenship { get; set; }
        public string emailAddress { get; set; }
        public string cellphoneNumber { get; set; }
        public string physicalAddress1 { get; set; }
        public string legacyPolicyNo { get; set; }
        public int policyNo { get; set; }
        public int roleCd { get; set; }
        public string status { get; set; }
        public int statusCD { get; set; }
        public string planTypeDescr { get; set; }
        public DateTime statusDate { get; set; }
        public DateTime dateOfCommencement { get; set; }
        public int premiumAmt { get; set; }
        public string salesPerson { get; set; }
        public string rewardStatus { get; set; }
        public string debiCheckStatus { get; set; }
        public string agency { get; set; }
        public string payor { get; set; }
        public string payorLegalReferenceNumber { get; set; }
        public string payorCellphoneNumber { get; set; }
        public string? payorEmailAddress { get; set; }
        public string beneficiaryName { get; set; }
        public int paymentTypeCD { get; set; }
        public string inspiratorNo { get; set; }
        public string region { get; set; }
        public int partnerCD { get; set; }
        public string partnerCode { get; set; }
        public int schemeCD { get; set; }
        public string schemeDesc { get; set; }
        public int planCD { get; set; }
        public string planDesc { get; set; }
        public int? channelCD { get; set; }
        public string? channelDesc { get; set; }
        public string entityFullname { get; set; }
    }
}
