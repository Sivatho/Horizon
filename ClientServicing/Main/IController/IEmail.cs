using RestSharp;

namespace ClientServicing.Main.IController
{
    public interface IEmail
    {
        Task<RestResponse> SendInternalEmailsAsync<T>(T payload) where T : class;
    }
}
