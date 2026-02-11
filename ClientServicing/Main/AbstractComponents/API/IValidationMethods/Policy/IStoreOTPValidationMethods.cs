using ClientServicing.Main.Models.Policy;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.Policy
{
    public interface IStoreOTPValidationMethods
    {
        public void ValidateStoreOTPRequestDataIsNotNullOrEmpty(StoreOTPRequest storeOTPRequest);
        public void ValidateStoreOTPResponseDataIsNotNullOrEmpty(StoreOTPResponse storeOTPResponse);
    }
}
