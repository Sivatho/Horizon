using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.Policy;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Models.Policy;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.Policy
{
    public class GetPolicyProductLineValidationMethods : AbstractValidationMethods, IGetPolicyProductLineValidationMethods
    {
        UtilitiesHelper utilitiesHelper = new();
        public void ValidateGetPolicyProductLineRequestDataIsNotNullOrEmpty_NotLessThanZero(PolicyNoAndLegacyPolicyNumberRequest getPolicyProductLineRequest)
        {
            using (Assert.EnterMultipleScope()) {
                Assert.That(getPolicyProductLineRequest.policyNo, Is.Not.LessThan(0), "PolicyNo Should Not Be Less Than Zero");
                Assert.That(getPolicyProductLineRequest.legacyPolicyNumber, Is.Not.Null.Or.Empty, "LegacyPolicyNumber Should Not Be Null or Empty");
            }
            DocumentTemplate.DisplayBody("Validated: GetPolicyProductLineRequest Data Is Not Null Or Empty And Integer Is Not Less Than Zero");
        }

        public void ValidateGetPolicyProductLineResponseDataIsNotNullOrEmpty_NotLessThanZero(GetPolicyProductLineResponse getPolicyProductLineResponse)
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(getPolicyProductLineResponse.executionOutcome,              Is.Not.Null.Or.Empty,   "ExecutionOutcome Should Not Be Null or Empty");
                Assert.That(getPolicyProductLineResponse.data,                          Is.Not.Null.Or.Empty,   "Data Property Should Not Be Null or Empty");
                Assert.That(getPolicyProductLineResponse.data.policyNo,                 Is.Not.LessThan(0),     "PolicyNo Should Not Be Less Than Zero");
                Assert.That(getPolicyProductLineResponse.data.productLineCD,            Is.Not.LessThan(0),     "ProductLineCD Should Not Be Less Than Zero");
                Assert.That(getPolicyProductLineResponse.data.productLineDescription,   Is.Not.Null.Or.Empty,   "ProductLineDescription Should Not Be Null or Empty");
            }
            DocumentTemplate.DisplayBody("Validated: GetPolicyProductLineResponse Data Is Not Null Or Empty And Integer Is Not Less Than Zero");
        }
        public GetPolicyProductLineResponse populateGetPolicyProductLineResponse(RestResponse response)
        {
            using JsonDocument jsonDoc = JsonDocument.Parse(response.Content);
            GetPolicyProductLineResponse getPolicyProductLineResponse = new()
            {
                executionOutcome = new ExecutionOutcome(),
                data = new GetPolicyProductLine()
            };

            foreach (var property in jsonDoc.RootElement.EnumerateObject())
            {
                switch (property.Name)
                {
                    case "succeeded": getPolicyProductLineResponse.executionOutcome.succeeded = (bool)utilitiesHelper.ReadBooleanNullable(property.Value); break;
                    case "message": getPolicyProductLineResponse.executionOutcome.message = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "errors": getPolicyProductLineResponse.executionOutcome.errors = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "data":
                        foreach (var dataProperty in property.Value.EnumerateObject())
                        {
                            switch (dataProperty.Name)
                            {
                                case "policyNo": getPolicyProductLineResponse.data.policyNo = (int)utilitiesHelper.ReadInt32Nullable(dataProperty.Value); break;
                                case "productLineCD": getPolicyProductLineResponse.data.productLineCD = (int)utilitiesHelper.ReadInt32Nullable(dataProperty.Value); break;
                                case "productLineDescription": getPolicyProductLineResponse.data.productLineDescription = utilitiesHelper.ReadStringNullable(dataProperty.Value); break;
                                default: DocumentTemplate.DisplayFieldAndValue("Unknown Data Propertey", dataProperty.Name); break;
                            }
                        }
                        break;
                    default: DocumentTemplate.DisplayFieldAndValue("Unknown Propertey", property.Name); break;
                }
            }
            return getPolicyProductLineResponse;
        }

        public override void ValidateResponseFieldParametersIsValid(RestResponse restResponse)
        {
            throw new NotImplementedException();
        }

        public override void ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(RestResponse restResponse)
        {
            throw new NotImplementedException();
        }
    }
}
