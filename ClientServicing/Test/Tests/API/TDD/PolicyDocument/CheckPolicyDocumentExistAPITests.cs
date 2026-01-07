using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Models.Policy;
using ClientServicing.Main.Models.PolicyDocument;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Test.Tests.API.TDD.PolicyDocument
{
    [TestFixture]
    public class CheckPolicyDocumentExistAPITests
    {
        PolicyDocumentAPIClient policyDocumentAPIClient = new PolicyDocumentAPIClient();
        UtilitiesHelper utilitiesHelper = new UtilitiesHelper();

        [Test]
        [Ignore("Test data needs to be updated output data is not as expected: Validations need to be added")]
        public async Task Given_CheckPolicyDocumentExistRequestPayloadIsValid_When_CheckPolicyDocumentExistAsync_Then_() {
            //Arrange
            var request = JsonSerializer.Deserialize<CheckPolicyDocumentExistRequest>
                (utilitiesHelper.ReadTestDataJson("PolicyDocument/Data", "GetPolicyDocumentRequestPayloadIsValid.json"));
            //Act 
            var response = await policyDocumentAPIClient.CheckPolicyDocumentExistAsync(request);
            var checkPolicyDocumentExistResponse = populateCheckPolicyDocumentExistResponse(response);
            //Assert
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
