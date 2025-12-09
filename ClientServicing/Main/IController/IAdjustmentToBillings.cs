using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace ClientServicing.Main.IController
{
    public interface IAdjustmentToBillings
    {
        Task<RestResponse> AddAdjustementToBillingsAsync<T>(T payload) where T : class;
        Task<RestResponse> GetOutstandingPolicyPremiumsAsync<T>(T payload) where T : class;
        Task<RestResponse> CancelAdjustmentToBillingsAsync<T>(T payload) where T : class;
        Task<RestResponse> GetAdjustmentToBillingsHistoryAsync<T>(T payload) where T : class;
        Task<RestResponse> GetAdjustedPeriodsAsync<T>(T payload) where T : class;
    }
}