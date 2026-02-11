using RestSharp;

namespace ClientServicing.Main.IController
{
    public interface IPayer
    {
        public Task<RestResponse> GetPayerDetailsByPolicyNumberAsync<T>(T payload) where T : class;
        public Task<RestResponse> UpsertBankingAndPayerAsync<T>(T payload) where T : class;
    }
}
