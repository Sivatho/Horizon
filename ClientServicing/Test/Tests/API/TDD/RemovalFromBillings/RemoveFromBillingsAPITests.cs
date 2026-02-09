using System.Net;
using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.JsonValidation;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.RemovalFromBillings;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.RemovalFromBillings;
using ClientServicing.Main.Resources.Helper;

namespace ClientServicing.Test.Tests.API.TDD.RemovalFromBillings
{
    [TestFixture]
    public class RemoveFromBillingsAPITests : RemovalFromBillingsValidationMethods
    {
        [Test]
        public async Task Given_CancelRemovalFromBillingsRequestPayloadIsValid_When_RemoveFromBillingsAsync_Then_ValidateRespondeStatusIsOK_AND_ResponseDataIsNotNullOrEmpty()
        {
            RemovalFromBillingsAPIClient removalFromBillingsAPIClient = new();
            UtilitiesHelper utilitiesHelper = new();

            //Arrange
            var request = JsonSerializer.Deserialize<RemovalFromBillingsRequest>(utilitiesHelper.ReadTestDataJson("RemovalFromBillings/Data", "RemovalFromBillingsRequestPayloadIsValid.json"));
            ValidateRemovalFromBillingsRequestIsNotNullOrEmpty_And_IntegerIsNotLessThanZero_And_DateTimeIsNotEqualToDefault(request);

            //Act
            var response = await removalFromBillingsAPIClient.RemoveFromBillingsAsync(request);
            var cancelRemovalFromBillingsResponse = PopulateRemovalFromBillingsResponse(response);
            var schema = ResponseSchemasEnvelope.DataBooleanSchema;
            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponseHeadersAreValid(response);
            ValidateResponsePropertyNameIsValidAndDataTypesIsValid(response, schema);
            ValidateResponseShouldMatchSchema(response, schema);
            ValidateRemovalFromBillingsResponseIsNotNullOrEmpty(cancelRemovalFromBillingsResponse);
        }
    }
}
