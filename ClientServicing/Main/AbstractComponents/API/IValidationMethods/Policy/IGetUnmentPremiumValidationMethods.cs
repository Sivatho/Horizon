using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Models.Policy;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.Policy
{
    public interface IGetUnmentPremiumValidationMethods
    {
        public GetUnmentPremiumResponse populateGetUnmentPremiumResponse(RestResponse response);
        public void ValidateGetUnmentPremiumResponseDataIsNotNullOrEmpty_And_IsNotLessThanZero_And_DateTimeIsNotEqualToDefault(GetUnmentPremiumResponse getUnmentPremiumResponse);
    }
}
