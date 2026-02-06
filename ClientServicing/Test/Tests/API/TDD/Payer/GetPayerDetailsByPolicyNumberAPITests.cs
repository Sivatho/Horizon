using System.Net;
using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.JsonValidation;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.Payer;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Resources.Helper;

namespace ClientServicing.Test.Tests.API.TDD.Payer
{
    public class GetPayerDetailsByPolicyNumberAPITests : GetPayerDetailsByPolicyNumberValidationMethods
    {
        PayerAPIClient payerAPIClient = new();
        UtilitiesHelper utilitiesHelper = new();

        [Test]
        public async Task Given_GetPayerDetailsByPolicyNumberRequest_When_GetPayerDetailsByPolicyNumberAsync_Then() {
            //Arrange
            var getPayerDetailsByPolicyNumberRequest = JsonSerializer.Deserialize<PolicyNoAndEffectiveDate>(
                utilitiesHelper.ReadTestDataJson("Payer/Data", "GetPayerDetailsByPolicyNumberPayloadRequestIsValid.json"));
            getPayerDetailsByPolicyNumberRequest.auditToken = Guid.NewGuid().ToString();
            ValidateGetPayerDetailsByPolicyNumberRequestIsNotNUllOrEmpt_And_IsNotLessThanZero_And_IsNotEqualToDefaultDateTime(getPayerDetailsByPolicyNumberRequest);

            //Act
            var response = await payerAPIClient.GetPayerDetailsByPolicyNumberAsync(getPayerDetailsByPolicyNumberRequest);
            var getPayerDetailsByPolicyNumberResponse = populateGetPayerDetailsByPolicyNumberResponse(response);
            var schema = ResponseSchemasEnvelope.PayerDetailsEnvelopeSchema;
            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponseHeadersAreValid(response);
            ValidateResponsePropertyNameIsValidAndDataTypesIsValid(response, schema);
            ValidateResponseShouldMatchSchema(response, schema);
            ValidateGetPayerDetailsByPolicyNumberResponseIsNotNUllOrEmpt_And_IsNotLessThanZero_And_IsNotEqualToDefaultDateTime(getPayerDetailsByPolicyNumberResponse);
        }
    }
}
