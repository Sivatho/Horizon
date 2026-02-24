using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Models.BeneficiaryDetails;
using ClientServicing.Main.Models.CancelPolicy;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.CancelPolicy
{
    internal interface IUpdateCancelDetailsValidationMethods
    {
        void ValidateUpdateCancelDetailsRequestIsNotNullOrEmpty(UpdateCancelPolicyDetailsRequest updateCancelPolicyDetailsRequest);
        void ValidateUpdateCancelDetailsResponseIsNotNullOrEmpty(PolicyEntityInfoUpsertResponse updateCancelPolicyDetailsResponse);
    }
}
