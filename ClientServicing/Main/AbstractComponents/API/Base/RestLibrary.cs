using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.Base
{
    public class RestLibrary : IRestLibrary
    {        
        protected UtilitiesHelper utilitiesHelper = new UtilitiesHelper();
        public RestLibrary()
        {
            var options = new RestClientOptions()
            {
                BaseUrl = new Uri(utilitiesHelper.GetApiBaseUrl()),
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            RestClient = new RestClient(options);
        }
        public RestClient RestClient { get; }
    }
}
