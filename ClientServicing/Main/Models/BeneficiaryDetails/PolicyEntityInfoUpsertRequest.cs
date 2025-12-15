using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.BeneficiaryDetails
{
    public class PolicyEntityInfoUpsertRequest
    {
        public int entityNo { get; set; }
        public int policyNo { get; set; }
        public int quoteId { get; set; }
        public int roleCd { get; set; }
        public int entityRelationId { get; set; }
        public int relationEntityNo { get; set; }
        public int relationCd { get; set; }
        public int statusCd { get; set; }
        public int titleCD { get; set; }
        public string initials { get; set; }
        public string firstname { get; set; }
        public string surname { get; set; }
        public int legalRefNoType { get; set; }
        public string legalRefNo { get; set; }
        public DateTime dateOfBirth { get; set; }
        public int preferredTelTypeCd { get; set; }
        public string emailAddress { get; set; }
        public string cellNumber { get; set; }
        public string faxNumber { get; set; }
        public string homeNumber { get; set; }
        public string workNumber { get; set; }
        public string phAddressLine1 { get; set; }
        public string phAddressLine2 { get; set; }
        public string phSuburb { get; set; }
        public string phCity { get; set; }
        public string phPostalCode { get; set; }
        public string poAddressLine1 { get; set; }
        public string poAddressLine2 { get; set; }
        public string poSuburb { get; set; }
        public string poCity { get; set; }
        public string poPostalCode { get; set; }
        public int userID { get; set; }
        public int genderCD { get; set; }
        public int smokerCD { get; set; }
        public int reasonCD { get; set; }
        public int isStudent { get; set; }
        public int isMentallyDisabled { get; set; }
        public int isPhysicallyDisabled { get; set; }
        public int isMarried { get; set; }
        public int percentageAlloc { get; set; }
        public DateTime effectiveDate { get; set; }
        public string auditToken { get; set; }
    }
}
