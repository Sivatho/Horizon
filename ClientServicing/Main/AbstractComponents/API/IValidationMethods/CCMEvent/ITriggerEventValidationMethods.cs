using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Models.CCMEvent;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.CCMEvent
{
    public interface ITriggerEventValidationMethods
    {
        void ValidateTriggerEventRequestPayload(TriggerEventRequest triggerEventRequest);
        void ValidateTriggerEventResponsePayload(TriggerEventResponse triggerEventResponse);
    }
}
