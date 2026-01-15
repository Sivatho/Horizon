using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Models.BeneficiaryDetails;
using ClientServicing.Main.Models.Email;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.BenefitExtendedMember
{
    public interface IPolicyBenefitExtendedMemberResponseValidationMethods
    {
        public void ValidatePolicyBenefitExtendedMemberResponseDataIsNotNullOrEmpty(SendInternalEmailsResponse sendInternalEmailsResponse);
    }
}
