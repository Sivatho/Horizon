using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Models.Policy;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.Policy
{
    public interface IGetPolicyProductLineValidationMethods
    {
        public void ValidateGetPolicyProductLineRequestDataIsNotNullOrEmpty_NotLessThanZero(PolicyIdentifierRequest GetPolicyProductLineRequest);
        public void ValidateGetPolicyProductLineResponseDataIsNotNullOrEmpty_NotLessThanZero(GetPolicyProductLineResponse getPolicyProductLineResponse);
    }
}
