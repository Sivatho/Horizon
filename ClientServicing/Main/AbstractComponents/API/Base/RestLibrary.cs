using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.Base
{

    public class RestLibrary : IRestLibrary, IDisposable
    {
        private readonly UtilitiesHelper _utilitiesHelper = new UtilitiesHelper();

        // Expose a fully initialized client
        public RestClient RestClient { get; }

        public RestLibrary()
        {
            var options = new RestClientOptions
            {
                BaseUrl = new Uri(_utilitiesHelper.GetApiBaseUrl()),
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };

            RestClient = new RestClient(options);
        }

        public void Dispose()
        {
            RestClient?.Dispose();
        }
    }

}
