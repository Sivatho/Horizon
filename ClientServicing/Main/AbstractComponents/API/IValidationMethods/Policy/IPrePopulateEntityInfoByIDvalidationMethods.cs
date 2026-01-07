using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Models.Policy;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.Policy
{
    public interface IPrePopulateEntityInfoByIDvalidationMethods
    {
        public void ValidatePrePopulateEntityInfoByIDRequestDataIsNotNullOrEmpty(CheckHasProductRequest prePopulateEntityInfoByIDRequest);
        public void ValidatePrePopulateEntityInfoByIDResponseDataIsNotNullOrEmpty(PrePopulateEntityInfoByIDResponse prePopulateEntityInfoByIDResponse);
    }
}
