using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Models.SendPayAtNumber;
using ClientServicing.Main.Resources.Helper;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.SendPayAtNumber
{
    public interface ISendTextMessageValidationMethods
    {
        public void ValidateSendTextMessageRequestIsNotNullOrEmpty(SendTextMessageRequest sendTextMessageRequest);
    }
}
