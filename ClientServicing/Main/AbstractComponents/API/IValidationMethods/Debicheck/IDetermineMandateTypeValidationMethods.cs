using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Models.Debicheck;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.Debicheck
{
    public interface IDetermineMandateTypeValidationMethods
    {
        void ValidateDetermineMandateTypeRequestDataIsNotNullOrEmpty(DetermineMandateTypeRequestData determineMandateTypeRequestData);
        void ValidateDetermineMandateTypeResponseDataIsNotNullOrEmpty(DetermineMandateTypeResponse determineMandateTypeResponse);
    }
}
