using RestSharp;

namespace AppCore.Main.IController
{
    public interface IQuoting
    {
        Task<RestResponse> CreateNewBusinessQuoteAsync<T>(T payload) where T : class;
        Task<RestResponse> GetQuoteRulesetAsync<T>(T payload) where T : class;
    }
}