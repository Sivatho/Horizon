using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.BeneficiaryDetails;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.BeneficiaryDetails;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Test.Tests.API.TDD.BeneficiaryDetails
{
    public class UpdatePolicyBeneficiaryCacheAPITest: UpdatePolicyBeneficiaryCacheValidationMethods
    {
        BeneficiaryDetailsAPIClient beneficiaryDetailsAPIClient = new();
        UtilitiesHelper utilitiesHelper = new();

        [Test]
        public async Task Given_BeneficiaryDetailsData_When_UpdatePolicyBeneficiaryCacheAsync_Then_ValidateFetchBankResponseIsOk_And_PropertyNameIsValid_And_DataTypesIsValid_And_IsNotNullOrEmpty_And_SchemaIsValid()
        {
            //Arrange
            var json = utilitiesHelper.ReadTestDataJson("BeneficiaryDetails/Data", "UpdatePolicyBeneficiaryCacheRequestPayloadIsValid.json");
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            BeneficiaryDetailsData beneficiaryDetailsData = JsonSerializer.Deserialize<BeneficiaryDetailsData>(json, options);

            //Act
            var response = await beneficiaryDetailsAPIClient.UpdatePolicyBeneficiaryCacheAsync(beneficiaryDetailsData);
            var updatePolicyBenefitCacheResponse = PopulateExecutionOutcome(response);

            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCodeOK(response);
            ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(response);
            ValidateResponseSchemaIsValid(response, "BeneficiaryDetails/Schema", "UpdatePolicyBenefitCacheResponseSchema.json");
        }
        private UpdatePolicyBenefitciaryResponse PopulateExecutionOutcome(RestResponse restResponse)
        {
            var updatePolicyBenefitCacheResponse = new UpdatePolicyBenefitciaryResponse();
            using JsonDocument doc = JsonDocument.Parse(restResponse.Content);

            foreach (var property in doc.RootElement.EnumerateObject())
            {
                switch (property.Name)
                {
                    case "succeeded":
                        updatePolicyBenefitCacheResponse.succeeded = (bool)utilitiesHelper.ReadBooleanNullable(property.Value); break;
                    case "message":
                        updatePolicyBenefitCacheResponse.message = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "error":
                        updatePolicyBenefitCacheResponse.errors = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "data":
                        updatePolicyBenefitCacheResponse.data = (bool)utilitiesHelper.ReadBooleanNullable(property.Value); break;
                    default:
                        TestContext.Out.WriteLine($"Unknown property: {property.Name}");
                        break;
                }
            }
            return updatePolicyBenefitCacheResponse;
        }
    }
}
 