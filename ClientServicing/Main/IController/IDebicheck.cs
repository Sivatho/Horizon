using RestSharp;

namespace ClientServicing.Main.IController
{
    public interface IDebicheck
    {
        Task<RestResponse> CheckStatusAsync<T>(T payload) where T : class;
        Task<RestResponse> MandatesRequestAsync<T>(T payload) where T : class;
        Task<RestResponse> DetermineMandateTypeAsync<T>(T payload) where T : class;
        Task<RestResponse> DebicheckRetryCheckStatusAsync<T>(T payload) where T : class;
        Task<RestResponse> DebicheckRequestRetryAsync<T>(T payload) where T : class;
    }
}