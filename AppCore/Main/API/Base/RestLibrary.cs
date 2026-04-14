using ClientServicing.Main.AbstractComponents.API.Base;
using RestSharp;

namespace AppCore.Main.API.Base
{
    public class RestLibrary : IRestLibrary, IDisposable
    {
        public RestClient RestClient { get; }
        public RestLibrary()
        {
            var options = new RestClientOptions
            {
                BaseUrl = new Uri("https://horizontest.clientele.co.za/horizon.appcore/"),
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}