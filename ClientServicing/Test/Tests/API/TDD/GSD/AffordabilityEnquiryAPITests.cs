using System.Net;
using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.GSD;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.GSD;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Test.Tests.API.TDD.GSD
{
    [TestFixture]
    public class AffordabilityEnquiryAPITests : AffordabilityEnquiryValidationMethods
    {
        GSDAPIClient gsdAPIClient = new GSDAPIClient();
        UtilitiesHelper utilitiesHelper = new UtilitiesHelper();

        [Test]
        public async Task Given_AffordabilityEnquiryRequestPayloadIsValid_When_AffordabilityEnquiryAsync_Then()
        {
            //Arrange
            Guid newGuid = Guid.NewGuid();
            AffordabilityEnquiryRequest? affordabilityEnquiryRequest = JsonSerializer.Deserialize
                <AffordabilityEnquiryRequest>
                (utilitiesHelper.ReadTestDataJson("GSD/Data",
                "AffordabilityEnquiryRequestPayloadIsValid.json"));
            affordabilityEnquiryRequest.requestId = newGuid.ToString();

            //Act
            var response = await gsdAPIClient.AffordabilityEnquiryAsync(affordabilityEnquiryRequest);
            var affordabilityEnquiryResponse = populateAffordabilityEnquiryResponse(response);

            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(response);
            ValidateAffordabilityEnquiryResponseIsNotNullOrEmpty(affordabilityEnquiryResponse);
            ValidateResponseSchemaIsValid(response, "GSD/Schema", "AffordabilityEnquiryResponseSchema.json");
        }
        private AffordabilityEnquiryResponse populateAffordabilityEnquiryResponse(RestResponse response) { 
            var affordabilityEnquiryResponse = new AffordabilityEnquiryResponse();
            using JsonDocument jsDoc = JsonDocument.Parse(response.Content);
            foreach (var property in jsDoc.RootElement.EnumerateObject()) {
                switch (property.Name) {
                    case "isValid":
                        affordabilityEnquiryResponse.isValid = (bool)utilitiesHelper.ReadBooleanNullable(property.Value); break;
                    case "errorMessage":
                        affordabilityEnquiryResponse.errorMessage = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "createdTimestamp":
                        affordabilityEnquiryResponse.createdTimestamp = (DateTime)utilitiesHelper.ReadDateTimeNullable(property.Value); break;
                    case "requestId":
                        affordabilityEnquiryResponse.requestId = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "identityNumber":
                        affordabilityEnquiryResponse.identityNumber = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "amount":
                        affordabilityEnquiryResponse.amount = (double)utilitiesHelper.ReadInt32Nullable(property.Value); break;
                    case "initials":
                        affordabilityEnquiryResponse.initials = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "surname":
                        affordabilityEnquiryResponse.surname = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "errorCodeId":
                        affordabilityEnquiryResponse.errorCodeId = (int)utilitiesHelper.ReadInt32Nullable(property.Value); break;
                    case "errorCode":
                        affordabilityEnquiryResponse.errorCode = (int)utilitiesHelper.ReadInt32Nullable(property.Value); break;
                    case "correlationId":
                        affordabilityEnquiryResponse.correlationId = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "employeeNumberHash":
                        affordabilityEnquiryResponse.employeeNumberHash = utilitiesHelper.ReadStringNullable(property.Value); break;
                    default:
                        TestContext.Out.WriteLine($"Unknown property: {property.Name}");
                        break;
                }
            }
            return affordabilityEnquiryResponse;
        }
    }
}
