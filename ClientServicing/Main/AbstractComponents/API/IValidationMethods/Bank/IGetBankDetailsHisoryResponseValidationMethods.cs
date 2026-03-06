using ClientServicing.Main.Models.Bank;
using ClientServicing.Main.Models.General;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.Response
{
    public interface IGetBankDetailsHisoryResponseValidationMethods
    {
        public void ValidateGetBankDetailsHisoryResponseRequestIsNotNullOrEmy(PolicyNoRequest getBankDetailsHisoryResponseRequest);
        public void ValidateBankDetailHistoryResponseDataIsNotNullOrEmptyAndTrueOrFalseAndDateIsNotEqualToDefaultAndCountGreaterThanZero(GetBankDetailHistoryResponse getBankDetailHistoryResponse);
        public void ValidateBankDetailHistoryResponseDataIsNotNullOrEmptyAndTrueOrFalseAndDateIsNotEqualToDefaultAndCountLessThanOrEqualToZero(GetBankDetailHistoryResponse getBankDetailHistoryResponse);
    }
}
