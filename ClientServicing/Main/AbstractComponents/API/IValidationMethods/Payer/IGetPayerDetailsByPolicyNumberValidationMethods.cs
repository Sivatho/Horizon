using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Models.Payer;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.Payer
{
    public interface IGetPayerDetailsByPolicyNumberValidationMethods
    {
        public void ValidateGetPayerDetailsByPolicyNumberRequestIsNotNUllOrEmpt_And_IsNotLessThanZero_And_IsNotEqualToDefaultDateTime(PolicyNoAndEffectiveDate getPayerDetailsByPolicyNumberRequest);
        public void ValidateGetPayerDetailsByPolicyNumberResponseIsNotNUllOrEmpt_And_IsNotLessThanZero_And_IsNotEqualToDefaultDateTime(GetPayerDetailsByPolicyNumberResponse getPayerDetailsByPolicyNumberResponse);
    }
}
