using System.Net;
using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.Policy;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.Policy;
using ClientServicing.Main.Resources.Helper;

namespace ClientServicing.Test.Tests.API.TDD.Policy
{
    [TestFixture]
    public class StoreOTPAPITests : StoreOTPValidationMethods
    {
        PolicyAPIClient policyAPIClient = new();
        UtilitiesHelper utilitiesHelper = new();

        [Test, Category("Positive")]
        public async Task Given_StoreOTPRequestPayloadIsValid_When_StoreOTPAsync_Then_ValidateStoreOTPResponseIsOk_And_PropertyNameIsValid_And_DataTypesIsValid_And_IsNotNullOrEmpty_And_SchemaIsValid()
        {
            // Arrange
            var request = JsonSerializer.Deserialize<StoreOTPRequest>
                (utilitiesHelper.ReadTestDataJson("Policy/Data", "StoreOTPRequestPayloadIsValid.json"));
            ValidateStoreOTPRequestDataIsNotNullOrEmpty(request);

            //Act
            var response = await policyAPIClient.StoreOTPAsync(request);
            var storeOTPResponse = populateStoreOTPResponse(response);

            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(response);
            ValidateStoreOTPResponseDataIsNotNullOrEmpty(storeOTPResponse);
            ValidateResponseSchemaIsValid(response, "Policy/Schema", "StoreOTPResponseSchema.json");
        }

        [Test, Category("Negative")]
        public async Task Given_StoreOTPRequestPayload_And_OTPExistsForPolicyAndManager_When_StoreOTPAsync_Then_ValidateStoreOTPResponseData_ResponseStatusInternalServerError_And_MessageAnActivateOTPArlreadyExist() {
            // Arrange
            var request = JsonSerializer.Deserialize<StoreOTPRequest>
                (utilitiesHelper.ReadTestDataJson("Policy/Data", "StoreOTPRequestPayloadIsValid.json"));
            ValidateStoreOTPRequestDataIsNotNullOrEmpty(request);

            //Act
            var response = await policyAPIClient.StoreOTPAsync(request);

            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.InternalServerError);
            ValidateErrorrMessage(response, "\"An active OTP already exists for this Policy and Manager.\"");
        }
    }
}
