using ClientServicing.Main.Models.Debicheck;
using ClientServicing.Main.Models.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.Debicheck
{
    public interface ICheckStatusResponseValidationMethods
    {
        public void ValidateCheckStatusResponseDataIsNotNullOrEmpty(CheckStatusResponse checkStatusResponse);
    }
}
