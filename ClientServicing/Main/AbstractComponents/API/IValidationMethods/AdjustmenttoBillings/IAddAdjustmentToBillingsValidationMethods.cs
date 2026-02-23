using ClientServicing.Main.Models.AddAdjustementToBillings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.AdjustmenttoBillings
{
    public interface IAddAdjustmentToBillingsValidationMethods
    {

        public void ValidateAddAdjustmentToBillingsResponseDataIsNotNullOrEmpty(AddAdjustementToBillingsRequest addadjustementtobillings);
	}
}
