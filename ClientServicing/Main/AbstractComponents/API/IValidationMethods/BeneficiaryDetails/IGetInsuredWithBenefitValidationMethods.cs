using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Models.BeneficiaryDetails;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.BeneficiaryDetails
{
    public interface IGetInsuredWithBenefitValidationMethods
    {
        //public void ValidateResponseIsNotNullOrEmpty(GetInsuredWithBenefitResponse getInsuredWithBenefitResponse);
        public void ValidateGetInsuredWithBenefitDataIsNotNullOrEmpty(GetInsuredWithBenefitResponse getInsuredWithBenefit);
    }
}
