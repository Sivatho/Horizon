using ClientServicing.Main.Models.General;
using ClientServicing.Main.Models.Policy;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.Policy
{
    public interface IFetchPolicyStatusValidationMethods
    {
        public void ValidateFetchPolicyStatusRequestDataIsNotNullOrEmpty(PolicyNoRequest fetchPolicyStatusRequest);
        public void ValidateFetchPolicyStatusResponseDataIsNotNullOrEmpty(FetchPolicyStatusResponse fetchPolicyStatusResponse);
    }
}
