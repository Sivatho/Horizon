using ClientServicing.Main.Models.Debicheck;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.Debicheck
{
    public interface IDebicheckRetryCheckStatus
    {
        public void ValidateDebicheckRetryCheckStatusRequestIsNotNullOrEmpty(DebicheckRetryCheckStatusRequest debicheckRetryCheckStatusRequest);
        public void ValidateDebicheckRetryCheckStatusResponseIsNotNullOrEmpty(DebicheckRetryCheckStatusResponse debicheckRetryCheckStatusResponse);
    }
}
