using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ClientServicing.Main.IController
{
    public interface IBenefitExtendedMember
    {
        Task<RestResponse> policyBenefitExtendedMemberAsync<T>(T policyNo) where T : class;
        Task<RestResponse> UpdateBenefitExtendedMemberAsync<T>(T payload) where T : class;
      
    }
}

