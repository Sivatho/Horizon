using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace ClientServicing.Main.IController
{
    public interface IGSD
    {
        Task<RestResponse>EmployeeEnquiryAsync<T>(T payload) where T : class;
        Task<RestResponse> AffordabilityEnquiryAsync<T>(T payload) where T : class; 
    }
}
