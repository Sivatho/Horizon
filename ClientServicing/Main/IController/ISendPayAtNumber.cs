using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace ClientServicing.Main.IController
{
    public interface ISendPayAtNumber
    {
        Task<RestResponse> SendTextMessageAsync<T>(T payload) where T : class;
    }
}
