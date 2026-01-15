using System.Net;
using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.PolicyDocument;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Models.PolicyDocument;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Test.Tests.API.TDD.PolicyDocument
{
    [TestFixture]
    public class CheckPolicyDocumentExistAPITests : CheckPolicyDocumentExistValidationMethods
    {
        PolicyDocumentAPIClient policyDocumentAPIClient = new PolicyDocumentAPIClient();
        UtilitiesHelper utilitiesHelper = new UtilitiesHelper();

        [Test]
        //[Ignore("Test data needs to be updated output data is not as expected")]
        public async Task Given_CheckPolicyDocumentExistRequestPayloadIsValid_And_ValidateCheckPolicyDocumentExistRequestIsNotNullOrEmptyOrLessThanZero_When_CheckPolicyDocumentExistAsync_Then_ValidateResponseStatusCodeOK_And_ResponsePropertyNameIsValid_And_DataTypesIsValid_And_PolicyDocumentExistResponseIsNotNullOrEmpty() {
            //Arrange
            var request = JsonSerializer.Deserialize<CheckPolicyDocumentExistRequest>
                (utilitiesHelper.ReadTestDataJson("PolicyDocument/Data", "GetPolicyDocumentRequestPayloadIsValid.json"));
            ValidateObjectRequestDataIsNotNullOrEmptyOrLessThanZero(request);
            //Act 
            var response = await policyDocumentAPIClient.CheckPolicyDocumentExistAsync(request);
            var checkPolicyDocumentExistResponse = populateCheckPolicyDocumentExistResponse(response);
            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponseHeadersAreValid(response);
            ValidateResponseFieldParametersIsValid(response);
            ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(response);
            ValidateResponseSchemaIsValid(response, "PolicyDocument/Schema", "CheckPolicyDocumentExistResponseShema.json");
            ValidateCheckPolicyDocumentExistResponseIsNotNullOrEmpty(checkPolicyDocumentExistResponse);            
        }
        private CheckPolicyDocumentExistResponse populateCheckPolicyDocumentExistResponse(RestResponse response)
        {
            using JsonDocument jsonDoc = JsonDocument.Parse(response.Content);
            var checkPolicyDocumentExistResponse = new CheckPolicyDocumentExistResponse
            {
                executionOutcome = new ExecutionOutcome()
            };
            foreach (var property in jsonDoc.RootElement.EnumerateObject())
            {
                switch (property.Name)
                {
                    case "succeeded":
                        checkPolicyDocumentExistResponse.executionOutcome.succeeded = (bool)utilitiesHelper.ReadBooleanNullable(property.Value); break;
                    case "message":
                        checkPolicyDocumentExistResponse.executionOutcome.message = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "errors":
                        checkPolicyDocumentExistResponse.executionOutcome.errors = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "data":
                        checkPolicyDocumentExistResponse.data = (bool)utilitiesHelper.ReadBooleanNullable(property.Value); break;
                        break;
                    default: TestContext.Out.WriteLine($"Unknown property: {property.Name}"); break;
                }
            }
            return checkPolicyDocumentExistResponse;
        }
    }
}
