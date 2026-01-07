using ClientServicing.Main.Models.GSD;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.GSD
{
    public interface IAffordabilityEnquiryValidationMethods
    {
        public void ValidateAffordabilityEnquiryRequestIsNotNullOrEmpty(AffordabilityEnquiryRequest affordabilityEnquiryRequest);
        public void ValidateAffordabilityEnquiryResponseIsNotNullOrEmpty(AffordabilityEnquiryResponse affordabilityEnquiryResponse);
    }
}
