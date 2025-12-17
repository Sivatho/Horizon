using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.BeneficiaryDetails;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.BeneficiaryDetails;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Test.Tests.API.TDD.BeneficiaryDetails
{
    public class PolicyEntityInfoUpsertAPITest : PolicyEntityInfoUpsertResponseValidationMethods
    {
        BeneficiaryDetailsAPIClient beneficiaryDetailsAPIClient = new();
        UtilitiesHelper utilitiesHelper = new();

        [Test]
        public async Task Given_PolicyEntityInfoUpsertRequest_When_PolicyEntityInfoUpsertAsync_Then_ValidateFetchBankResponseIsOk_And_PropertyNameIsValid_And_DataTypesIsValid_And_IsNotNullOrEmpty_And_SchemaIsValid()
        {
            // Arrange
            PolicyEntityInfoUpsertRequest policyEntityInfoUpsert = JsonSerializer.Deserialize<PolicyEntityInfoUpsertRequest>(utilitiesHelper.ReadTestDataJson("BeneficiaryDetails/Data", "PolicyEntityInfoUpsertRequestPayloadIsValid.json"));
            policyEntityInfoUpsert.dateOfBirth = DateTime.Now.AddYears(-25);
            policyEntityInfoUpsert.effectiveDate = DateTime.Now.AddDays(30);

            // Act
            var response = await beneficiaryDetailsAPIClient.PolicyEntityInfoUpsertAsync(policyEntityInfoUpsert);
            var policyBeneficiaryDetailsResponse = populatePolicyEntityInfoUpsert(response);

            // Assert
            
            ValidationAssertionHeading();
            ValidateResponseStatusCodeOK(response);
            ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(response);
            ValidateResponseIsNotNullOrEmpty(policyBeneficiaryDetailsResponse);
            ValidateResponseSchemaIsValid(response, "BeneficiaryDetails/Schema", "PolicyEntityDetailInfoUpertResponseSchema.json");
        }

        private PolicyEntityInfoUpsertResponse populatePolicyEntityInfoUpsert(RestResponse restResponse)
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
    }
}
