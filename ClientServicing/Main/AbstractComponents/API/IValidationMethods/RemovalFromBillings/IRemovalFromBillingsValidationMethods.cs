using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Models.RemovalFromBillings;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.RemovalFromBillings
{
    public interface IRemovalFromBillingsValidationMethods
    {
        public void ValidateRemovalFromBillingsRequestIsNotNullOrEmpty_And_IntegerIsNotLessThanZero_And_DateTimeIsNotEqualToDefault(RemovalFromBillingsRequest removalFromBillingsRequest);
        public void ValidateRemovalFromBillingsRespondeIsNotNullOrEmpty_And_IntegerIsNotLessThanZero_And_DateTimeIsNotEqualToDefault(RemovalFromBillingsHistoryResponse removalFromBillingsHistoryResponse);
    }
}
