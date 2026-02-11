using System.Net;
using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.Policy;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.Policy;
using ClientServicing.Main.Resources.Helper;

namespace ClientServicing.Test.Tests.API.TDD.Policy
{
    [TestFixture]
    public class VerifyAndUpdateOTPAPITests: StoreOTPValidationMethods
    {
        PolicyAPIClient policyAPIClient = new();
        UtilitiesHelper utilitiesHelper = new();
        [Test]
        public async Task Given_VerifyAndUpdateOTPRequestPayloadIsValid_When_VerifyAndUpdateOTPAsync_Then_ValidateStoreOTPResponseIsOk_And_PropertyNameIsValid_And_DataTypesIsValid_And_IsNotNullOrEmpty_And_SchemaIsValid()
        {
            //Arrange
            var request = JsonSerializer.Deserialize<StoreOTPRequest>
               (utilitiesHelper.ReadTestDataJson("Policy/Data", "StoreOTPRequestPayloadIsValid.json"));
            ValidateStoreOTPRequestDataIsNotNullOrEmpty(request);

            //Act
            var response = await policyAPIClient.VerifyAndUpdateOTPAsync(request);
            var verifyAndUpdateOTPResponse = populateStoreOTPResponse(response);

            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(response);
            ValidateStoreOTPResponseDataIsNotNullOrEmpty(verifyAndUpdateOTPResponse);
            ValidateResponseSchemaIsValid(response, "Policy/Schema", "StoreOTPResponseSchema.json");
        }
    }
}
