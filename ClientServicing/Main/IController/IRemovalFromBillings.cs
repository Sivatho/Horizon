using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace ClientServicing.Main.IController
{
    public interface IRemovalFromBillings
    {
        Task<RestResponse> RemovalFromBillingsHistoryAsync<T>(T payload) where T : class;
        Task<RestResponse> CancelRemovalFromBillingsAsync<T>(T payload) where T : class;
        Task<RestResponse> RemoveFromBillingsAsync<T>(T payload) where T : class;
    }
}
