using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.Policy
{
    public class EntityInfoUpsertRequest
    {
        public required int policyNo { get; set; }
        public required int entityNo { get; set; }
        public required int titleCD { get; set; }
        public required string legalRefNumber { get; set; }
        public required int legalRefNoType { get; set; }
        public required int genderCD { get; set; }
        public required DateTime dob { get; set; }
        public required DateTime effectiveDate { get; set; }
        public string initials { get; set; }
        public string firstname { get; set; }
        public string surname { get; set; }   
        public string emailAddress { get; set; }
        public int prefferedComType { get; set; }
        public string cellNumber { get; set; }
        public string faxNumber { get; set; }
        public string homeNumber { get; set; }
        public string workNumber { get; set; }
        public string alternateNumber { get; set; }
        public string whatsappNumber { get; set; }
        public string physicalAddressLine1 { get; set; }
        public string physicalAddressLine2 { get; set; }
        public string physicalSuburb { get; set; }
        public string physicalCity { get; set; }
        public string physicalPostalCode { get; set; }
        public string postalAddressLine1 { get; set; }
        public string postalAddressLine2 { get; set; }
        public string postalSuburb { get; set; }
        public string postalCity { get; set; }
        public string postalPostalCode { get; set; }
        public string userID { get; set; }        
        public int smokerCD { get; set; }        
        public int citizenshipCD { get; set; }
        public string auditToken { get; set; }
    }
}
