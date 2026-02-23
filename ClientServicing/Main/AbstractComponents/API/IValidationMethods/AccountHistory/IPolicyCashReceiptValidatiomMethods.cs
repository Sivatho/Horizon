using ClientServicing.Main.Models.AccountHistory;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.AccountHistory
{
    public interface IPolicyCashReceiptValidatiomMethods
    {
        void ValidatePolicyCashReceiptRequestPayload(PolicyAccountHistoryRequest accountHistoryRequest);
        void ValidatePolicyCashReceiptResponsePayload(PolicyCashReceiptResponse policyCashReceiptResponse);        
    }
}
