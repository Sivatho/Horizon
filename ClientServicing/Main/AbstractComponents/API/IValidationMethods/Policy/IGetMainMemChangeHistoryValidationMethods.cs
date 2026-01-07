using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Models.BeneficiaryDetails;
using ClientServicing.Main.Models.Policy;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.Policy
{
    public interface IGetMainMemChangeHistoryValidationMethods
    {
        public void ValidateGetMainMemChangeHistoryRequestDataIsNotNullOrEmpty(PolicyBeneficiaryDetailsRequest getMainMemChangeHistoryRequest);
        public void ValidateGetMainMemChangeHistoryResponseDataIsNotNullOrEmpty(GetMainMemChangeHistoryResponse getMainMemChangeHistoryResponse);
    }
}
