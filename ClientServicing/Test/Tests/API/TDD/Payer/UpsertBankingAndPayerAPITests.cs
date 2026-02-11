using System.Net;
using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.JsonValidation;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.Payer;
using ClientServicing.Main.Resources.Helper;

namespace ClientServicing.Test.Tests.API.TDD.Payer
{
    public class UpsertBankingAndPayerAPITests : UpsertBankingAndPayerValidationMethods
    {
        PayerAPIClient payerAPIClient = new();
        UtilitiesHelper utilitiesHelper = new();

        [Test]
        public async Task Given_UpsertBankingAndPayerRequestPayloasIsValid_When_UpsertBankingAndPayerAsync_Then_ValidateResponseStatusCodeIsOk_And_HeadersAreValid_And_()
        {
            //Arrange
            var request = JsonSerializer.Deserialize<UpsertBankingAndPayerRequest>(
                utilitiesHelper.ReadTestDataJson("Payer/Data", "UpsertBankingAndPayerRequestPayloasIsValid.json"));
            ValidateUpsertBankingAndPayerRequestIsNotNUllOrEmpty(request);

            //Act
            var response = await payerAPIClient.UpsertBankingAndPayerAsync(request);
            var upsertBankingAndPayerResponse = populateCheckPolicyIfMainMemberOnlyResponse(response);
            var schema = ResponseSchemasEnvelope.DataBooleanSchema;

            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponseHeadersAreValid(response);
            ValidateResponsePropertyNameIsValidAndDataTypesIsValid(response, schema);
            ValidateResponseShouldMatchSchema(response, schema);
            ValidateUpsertBankingAndPayerResponseIsNotNUllOrEmpt(upsertBankingAndPayerResponse);
        }
    }
}
