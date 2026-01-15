using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.BenefitExtendedMember
{

    public class UpdateBenefitExtendedMemberRequest
    {
        public int? benefit { get; set; }  // or add JsonConverter to convert string to int
        public int? benefitTypeCd { get; set; }
        public string? benefitDescription { get; set; }
        public DateTime? doc { get; set; }
        public int? premium { get; set; }
        public string? sumAssured { get; set; }
        public string? titleDescr { get; set; }
        public int? entityNo { get; set; }
        public int policyNo { get; set; }  
        public  string firstName { get; set; }
        public string surname { get; set; }
        public int? genderCd { get; set; }
        public string? genderDescription { get; set; }
        public string? cellNumber { get; set; }
        public int relationCd { get; set; }
        public int? rolecd { get; set; }
        public string? relationDescr { get; set; }
        public int legalRefNoTypeCD { get; set; }
        public string? legalRefNoType { get; set; }
        public string idNumber { get; set; }
        public int? citizenshipCd { get; set; }
        public string? citizenshipDescr { get; set; }
        public int? coveredAmount { get; set; }
        public int? premiumAmount { get; set; }
        public string? waitingPeriod { get; set; }
        public DateTime dob { get; set; }
        public int? statusCd { get; set; }
        public DateTime? ofd { get; set; }
        public int? isCovered { get; set; }
        public int? isStudent { get; set; }
        public int? isPhysicallyDisabled { get; set; }
        public int? isMentallyDisabled { get; set; }
        public int? isMarried { get; set; }
        public int? isFree { get; set; }
        public string? status { get; set; }
        public string? fullName { get; set; }
        public string? idNumberMasked { get; set; }
        public int? titleCD { get; set; }
        public string username { get; set; }
        public string? auditToken { get; set; }

    }
}