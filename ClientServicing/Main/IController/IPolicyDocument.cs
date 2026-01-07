using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace ClientServicing.Main.IController
{
    public interface IPolicyDocument
    {
        public Task<RestResponse> CheckPolicyDocumentExistAsync<T>(T payload) where T : class;
        public Task<RestResponse> RetrievePolicyDocumentsAsync<T>(T payload) where T : class;
        public Task<RestResponse> RetrievePolicyDocumentDetailsAsync<T>(T payload) where T : class;
        public Task<RestResponse> UpsertPolicyDocumentAsync<T>(T payload) where T : class;
    }
}
