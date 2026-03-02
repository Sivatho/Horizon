using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Models.Debicheck;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.Debicheck
{
    public interface IDebitcheckRequestRetry
    {
        void ValidateDebicheckRetryCheckStatusRequestIsNotNullOrEmpty(DebicheckRetryCheckStatusRequest debicheckRetryCheckStatusRequest);
        void ValidateDebicheckRetryCheckStatusResponseIsNotNullOrEmpty(DebicheckRetryCheckStatusResponseData debicheckRetryCheckStatusResponse);
    }
}
