using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.JsonValidation;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.RemovalFromBillings;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.RemovalFromBillings;
using ClientServicing.Main.Resources.Helper;

namespace ClientServicing.Test.Tests.API.TDD.RemovalFromBillings
{
    [TestFixture]
    public class RemovalFromBillingsHistory : RemovalFromBillingsValidationMethods
    {
        RemovalFromBillingsAPIClient removalFromBillingsAPIClient = new();
        UtilitiesHelper utilitiesHelper = new();

        [Test]
        public async Task Given_RemovalFromBillingsRequestPayloadIsValid_When_RemovalFromBillingsHistoryAsync_Then_ValidateRespondeStatusIsOK_AND_ResponseDataIsNotNullOrEmpty_And_IsNotLessThanZero_And_IsNotEqualToDefaultDateTime() {
            //Arrange
            var request = JsonSerializer.Deserialize<RemovalFromBillingsRequest>(utilitiesHelper.ReadTestDataJson("RemovalFromBillings/Data", "RemovalFromBillingsRequestPayloadIsValid.json"));
            ValidateRemovalFromBillingsRequestIsNotNullOrEmpty_And_IntegerIsNotLessThanZero_And_DateTimeIsNotEqualToDefault(request);
            //Act
            var response = await removalFromBillingsAPIClient.RemovalFromBillingsHistoryAsync(request);
            var removalFromBillingsHistoryResponse = populateRemovalFromBillingsHistoryResponse(response);
            var schema = ResponseSchemasEnvelope.RemovalFromBillingHistory;

            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponseHeadersAreValid(response);
            ValidateResponsePropertyNameIsValidAndDataTypesIsValid(response, schema);
            ValidateResponseShouldMatchSchema(response, schema);
            ValidateRemovalFromBillingsRespondeIsNotNullOrEmpty_And_IntegerIsNotLessThanZero_And_DateTimeIsNotEqualToDefault(removalFromBillingsHistoryResponse);

        }
    }
}
