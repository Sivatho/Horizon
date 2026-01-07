using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Models.General;

namespace ClientServicing.Main.Models.Policy
{
    public class PrePopulateEntityInfoByIDResponse
    {
        public ExecutionOutcome executionOutcome { get; set; }
        public EntityInfo data { get; set; }
    }
    public class EntityInfo {
        public int entityNo { get; set; }
        public int titleCd { get; set; }
        public string titleDescr { get; set; }
        public string firstName { get; set; }
        public string surname { get; set; }
        public string legalRefNo { get; set; }
        public int genderCd { get; set; }
        public string genderDescr { get; set; }
        public int citizenshipCd { get; set; }
        public string citizenshipDescr { get; set; }
        public int smokerCd { get; set; }
        public string smokerDescr { get; set; }
        public string cellNumber { get; set; }
        public string homeNumber { get; set; }
        public string faxNumber { get; set; }
        public string workNumber { get; set; }
        public string alternateNumber { get; set; }
        public string whatsappNumber { get; set; }
        public string emailAddress { get; set; }
        public string postalAddressLine1 { get; set; }
        public string postalAddressLine2 { get; set; }
        public string postalAddressSuburb { get; set; }
        public string postalAddressCity { get; set; }
        public string postalAddressCode { get; set; }
        public string physicalAddressLine1 { get; set; }
        public string physicalAddressLine2 { get; set; }
        public string physicalAddressSuburb { get; set; }
        public string physicalAddressCity { get; set; }
        public string physicalAddressCode { get; set; }
    }
}
