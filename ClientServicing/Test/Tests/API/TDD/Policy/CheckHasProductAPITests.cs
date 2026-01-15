using System.Net;
using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.Policy;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Models.Policy;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Test.Tests.API.TDD.Policy
{
    [TestFixture]
    public class CheckHasProductAPITests : CheckHasProductValidationMethods
    {
        PolicyAPIClient policyAPIClient = new PolicyAPIClient();
        UtilitiesHelper utilitiesHelper = new UtilitiesHelper();

        [Test]
        public async Task Given_CheckHasProductRequestPayloadIsValid_When_CheckHasProductAsync_Then_ValidateResponseStatusCodeOK_And_PropertyNameIsValid_And_DataTypesIsValid_And_CheckHasProductResponseDataIsNotNullOrEmpty() {
            //Arrange
            var checkHasProductRequest = JsonSerializer.Deserialize<CheckHasProductRequest>
                (utilitiesHelper.ReadTestDataJson("Policy/Data", "CheckHasProductRequestPayloadIsValid.json"));
            
            //Act
            var response = await policyAPIClient.CheckHasProductAsync (checkHasProductRequest);
            var checkHasProductResponse = populateCheckHasProductResponse(response);

            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(response);
            ValidateCheckHasProductResponseDataIsNotNullOrEmpty(checkHasProductResponse);
        }

        private CheckHasProductResponse populateCheckHasProductResponse(RestResponse response)
        {
            using JsonDocument jsDoc = JsonDocument.Parse (response.Content);
            var checkHasProductResponse = new CheckHasProductResponse
            {
                executionOutcome = new ExecutionOutcome()
            };
            foreach (var property in jsDoc.RootElement.EnumerateObject()) {
                switch (property.Name) {
                    case "succeeded":   checkHasProductResponse.executionOutcome.succeeded =  (bool)utilitiesHelper.ReadBooleanNullable(property.Value); break;
                    case "message":     checkHasProductResponse.executionOutcome.message =    utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "errors":      checkHasProductResponse.executionOutcome.errors =     utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "data":        checkHasProductResponse.data =                        (bool)utilitiesHelper.ReadBooleanNullable (property.Value); break;
                    default: TestContext.Out.WriteLine($"Unknown property: {property.Name}"); break;
                }
            }
            return checkHasProductResponse;
        }
    }
}
