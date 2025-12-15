using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace ClientServicing.Main.IController
{
    public interface IBeneficiaryDetails
    {
        public Task<RestResponse> PolicyBeneficiaryDetailsAsync<T>(T payload) where T : class;
        public Task<RestResponse> PolicyEntityInfoUpsertAsync<T>(T payload) where T : class;
        public Task<RestResponse> GetAndCachePolicyBeneficiaryDetailsAsync<T>(T payload) where T : class;
        public Task<RestResponse> UpdatePolicyBeneficiaryCacheAsync<T>(T payload) where T : class;
        public Task<RestResponse> GetCachedBeneficiaryListAsync<T>(T payload) where T : class;
        public Task<RestResponse> SaveUpdatedBeneficiariesAsync<T>(T payload) where T : class;
    }
}