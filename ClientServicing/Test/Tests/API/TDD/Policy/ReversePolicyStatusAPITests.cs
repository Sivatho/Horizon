using System.Net;
using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.JsonValidation;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.Policy;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.Policy;
using ClientServicing.Main.Resources.Helper;

namespace ClientServicing.Test.Tests.API.TDD.Policy
{
    [TestFixture]
    public class ReversePolicyStatusAPITests : ReversePolicyStatusValidationMethods
    {
        PolicyAPIClient policyAPIClient = new();
        UtilitiesHelper utilitiesHelper = new();

        [Test, Category("Positive")]
        public async Task Given_ReversePolicyStatusRequestPayloadIsValid_When_ReversePolicyStatusAsync_Then_ValidationReversePolicyStatusResponseOK_And_HeadersAreValid_And_ShouldAcceptValidNamesAndTypes_And_ShouldMatchSchema_And_IsNotNullOrEmpty() {
            //Arrange
            var reversePolicyStatusRequest = JsonSerializer.Deserialize<ReversePolicyStatusRequest>(
                utilitiesHelper.ReadTestDataJson("Policy/Data", "ReversePolicyStatusRequestPayloadIsValid.json"));
            ValidationReversePolicyStatusRequestIsNotNullOrEmptyAndIsNotLessThanZero(reversePolicyStatusRequest);

            //Act
            var response = await policyAPIClient.ReversePolicyStatusAsync(reversePolicyStatusRequest);
            var reversePolicyStatusResponse = populateReversePolicyStatusResponse(response);
            var schema = ResponseSchemasEnvelope.DataBooleanSchema;

            //Arrange
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponseHeadersAreValid(response);
            ValidateResponsePropertyNameIsValidAndDataTypesIsValid(response, schema);
            ValidateResponseShouldMatchSchema(response, schema);
            ValidationReversePolicyStatusResponseIsNotNullOrEmpty(reversePolicyStatusResponse);
        }

        [Test, Category("Negative")]
        [Ignore("The response is incorrect as the status returned is incorrect and the response content is not formated correctly and policy number is not validated if it exists or not")]
        public async Task Give_ReversePolicyStatusRequestPayloadIsValid_And_PolicyNoStatusIsInforce_When_ReversePolicyStatusAsync_Then_ValidationReversePolicyStatusRespone() {
            //Arrange
            var reversePolicyStatusRequest = JsonSerializer.Deserialize<ReversePolicyStatusRequest>(
                utilitiesHelper.ReadTestDataJson("Policy/Data", "ReversePolicyStatusRequestPayloadIsValid.json"));
            ValidationReversePolicyStatusRequestIsNotNullOrEmptyAndIsNotLessThanZero(reversePolicyStatusRequest);

            //Act
            var response = await policyAPIClient.ReversePolicyStatusAsync(reversePolicyStatusRequest);
            var schema = ResponseSchemasEnvelope.DataBooleanSchema;

            //Arrange
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.InternalServerError);
            ValidateResponseHeadersAreValid(response);
            //ValidateResponseDataShouldAcceptValidNames_And_Types(response, schema);
            //ValidateResponseShouldMatchSchema(response, schema);
        }
    }
}
