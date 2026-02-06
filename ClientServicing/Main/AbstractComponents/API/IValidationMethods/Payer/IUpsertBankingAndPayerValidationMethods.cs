using ClientServicing.Main.Models.Payer;
using ClientServicing.Main.Models.Policy;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.Payer
{
    public interface IUpsertBankingAndPayerValidationMethods
    {
        public void ValidateUpsertBankingAndPayerRequestIsNotNUllOrEmpty(UpsertBankingAndPayerRequest upsertBankingAndPayerRequest);
        public void ValidateUpsertBankingAndPayerResponseIsNotNUllOrEmpt(InsertPolicyNoteResponse upsertBankingAndPayerRequest);
    }
}
