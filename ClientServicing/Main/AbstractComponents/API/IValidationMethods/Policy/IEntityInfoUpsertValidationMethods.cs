using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Models.BeneficiaryDetails;
using ClientServicing.Main.Models.Policy;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.Policy
{
    public interface IEntityInfoUpsertValidationMethods
    {
        public void ValidateEntityInfoUpsertRequestDataIsNotNullOrEmpty(EntityInfoUpsertRequest entityInfoUpsertRequest);
        public void ValidateEntityInfoUpsertResponseDataIsNotNullOrEmpty(PolicyEntityInfoUpsertResponse entityInfoUpsertResponse);
    }
}
