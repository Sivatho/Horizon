using RestSharp;

namespace ClientServicing.Main.IController
{
    public interface ICCMEvent
    {
        Task<RestResponse> TriggerEventAsync<T>(T payload) where T : class;
        Task<RestResponse> GetEventDetailConstructBPEAsync<T>(T payload) where T : class;
    }
}
