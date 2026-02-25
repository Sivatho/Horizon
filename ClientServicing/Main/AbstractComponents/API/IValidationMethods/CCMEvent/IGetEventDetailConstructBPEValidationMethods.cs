using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Models.CCMEvent;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.CCMEvent
{
    internal interface IGetEventDetailConstructBPEValidationMethods
    {
        void ValidateGetEventDetailConstructBPERequestPayload(GetEventDetailConstructBPERequest getEventDetailConstructBPERequest);
        void ValidateGetEventDetailConstructBPEResponsePayload(GetEventDetailConstructBPEResponse getEventDetailConstructBPEResponse);
    }
}
