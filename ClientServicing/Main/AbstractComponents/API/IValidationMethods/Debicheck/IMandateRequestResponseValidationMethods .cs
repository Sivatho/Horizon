using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Models.Debicheck;
using ClientServicing.Main.Models.Email;
using static ClientServicing.Main.Models.Debicheck.MandatesRequest;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.Debicheck
{
    public interface IMandateRequestValidationMethods
    {
        void ValidateMandateRequesDataIsNotNullOrEmpty(MandatesRequestData mandatesRequestData);
        public void ValidateMandateResponseDataIsNotNullOrEmpty(MandatesRequestResponse mandaterequestresponse);
    }
}
