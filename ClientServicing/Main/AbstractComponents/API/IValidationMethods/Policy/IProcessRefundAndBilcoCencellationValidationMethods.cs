using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Models.Policy;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.Policy
{
    internal interface IProcessRefundAndBilcoCencellationValidationMethods
    {
        public void ValidateProcessRefundAndBilcoCencellationRequestPayload(ProcessRefundAndBilcoCancellationRequest processRefundAndBilcoCencellationRequest);
    }
}
