using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.BeneficiaryDetails;
using ClientServicing.Main.Models.BeneficiaryDetails;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.BeneficiaryDetails
{
    public class PolicyEntityInfoUpsertResponseValidationMethods : AbstractValidationMethods, IPolicyEntityInfoUpsertValidationMethods
    {
        UtilitiesHelper utilitiesHelper = new();
        public void ValidatePolicyEntityInfoUpsertResponseDataIsNotNullOrEmpty(PolicyEntityInfoUpsertResponse policyEntityInfoUpsertResponse)
        {
            Assert.Multiple(() =>
            {
                Assert.That(policyEntityInfoUpsertResponse.data, Is.Not.False.Or.Not.Empty, "Data property is False or Empty.");
                Assert.That(policyEntityInfoUpsertResponse.executionOutcome, Is.Not.Null.Or.Empty, "ExecutionOutcome property is null.");
            });
        }
        public PolicyEntityInfoUpsertResponse PopulatePolicyEntityInfoUpsert(RestResponse restResponse)
        {
            using JsonDocument doc = JsonDocument.Parse(restResponse.Content);

            PolicyEntityInfoUpsertResponse policyEntityInfoUpsertResponse = new PolicyEntityInfoUpsertResponse
            {
                executionOutcome = new ExecutionOutcome()
            };
            foreach (JsonProperty property in doc.RootElement.EnumerateObject())
            {
                switch (property.Name)
                {
                    case "succeeded":
                        policyEntityInfoUpsertResponse.executionOutcome.succeeded = (bool)utilitiesHelper.ReadBooleanNullable(property.Value);
                        break;
                    case "message":
                        policyEntityInfoUpsertResponse.executionOutcome.message = utilitiesHelper.ReadStringNullable(property.Value);
                        break;
                    case "errors":
                        policyEntityInfoUpsertResponse.executionOutcome.errors = utilitiesHelper.ReadStringNullable(property.Value);
                        break;
                    case "data":
                        policyEntityInfoUpsertResponse.data = (bool)utilitiesHelper.ReadBooleanNullable(property.Value);
                        break;
                    default:
                        TestContext.Out.WriteLine($"Unknown property: {property.Name}");
                        break;
                }
            }
            return policyEntityInfoUpsertResponse;
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
