using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Models.PolicyDocument;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Test.Tests.API.TDD.PolicyDocument
{
    [TestFixture]
    public class UpsertPolicyDocumentAPITests
    {
        PolicyDocumentAPIClient policyDocumentAPIClient = new PolicyDocumentAPIClient();
        UtilitiesHelper utilitiesHelper = new UtilitiesHelper();

        [Test]
        //[Ignore("API call failed with status code: InternalServerError and message: \"Object reference not set to an instance of an object.")]
        public async Task Given_UpsertPolicyDocumentRequestPayloadIsValid_When_UpsertPolicyDocumentAsync_Then_() {
            //Arrange
            var upsertPolicyDocumentRequest = JsonSerializer.Deserialize<UpsertPolicyDocumentRequest>(
                utilitiesHelper.ReadTestDataJson("PolicyDocument/Data", "UpsertPolicyDocumentRequestPayloadIsValid.json"));
            upsertPolicyDocumentRequest.fileDetails.fileId = Guid.NewGuid().ToString();
            upsertPolicyDocumentRequest.fileDetails.referenceId = Guid.NewGuid().ToString();
            //Act
            var response = await policyDocumentAPIClient.UpsertPolicyDocumentAsync(upsertPolicyDocumentRequest);

            //Assert
        }
        /*
        public UpsertPolicyDocumentResponse populateUpsertPolicyDocumentResponse(RestResponse response)
        {
            using JsonDocument jsonDoc = JsonDocument.Parse(response.Content);
            var upsertPolicyDocumentResponse = new UpsertPolicyDocumentResponse
            {
                executionOutcome = new ExecutionOutcome(),
                upsertPolicyDocumentRequest = new UpsertPolicyDocumentRequest()
            };
            foreach (var property in jsonDoc.RootElement.EnumerateObject())
            {
                switch (property.Name)
                {
                    case "succeeded":
                        upsertPolicyDocumentResponse.executionOutcome.succeeded = (bool)utilitiesHelper.ReadBooleanNullable(property.Value); break;
                    case "message":
                        upsertPolicyDocumentResponse.executionOutcome.message = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "errors":
                        upsertPolicyDocumentResponse.executionOutcome.errors = utilitiesHelper.ReadStringNullable(property.Value); break;
                }
            }
            return upsertPolicyDocumentResponse;
        }*/
    }
}
