using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Models.RemovalFromBillings;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.RemovalFromBillings
{
    public interface IRemovalFromBillingsValidationMethods
    {
        public void ValidateRemovalFromBillingsRequestIsNotNullOrEmpty_And_IntegerIsNotLessThanZero_And_DateTimeIsNotEqualToDefault(RemovalFromBillingsRequest removalFromBillingsRequest);
        public void ValidateRemovalFromBillingsHistoryRespondeIsNotNullOrEmpty_And_IntegerIsNotLessThanZero_And_DateTimeIsNotEqualToDefault(RemovalFromBillingsHistoryResponse removalFromBillingsHistoryResponse);
        public void ValidateRemovalFromBillingsResponseIsNotNullOrEmpty(ExecutionOutcomeAndDataBooleanResponse removalFromBillingsResponse);
    }
}
