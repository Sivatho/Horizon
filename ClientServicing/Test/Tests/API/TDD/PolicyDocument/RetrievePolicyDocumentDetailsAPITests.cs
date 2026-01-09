using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.PolicyDocument;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Models.PolicyDocument;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Test.Tests.API.TDD.PolicyDocument
{
    [TestFixture]
    public class RetrievePolicyDocumentDetailsAPITests : RetrievePolicyDocumentDetailsValidationMethods
    {
        PolicyDocumentAPIClient policyDocumentAPIClient = new PolicyDocumentAPIClient();
        UtilitiesHelper utilitiesHelper = new UtilitiesHelper();

        [Test]
        //[Ignore("Test data needs to be updated output data is not as expected: Validations need to be added")]
        public async Task Given_GetPolicyDocumentRequestPayloadIsValid_When_RetrievePolicyDocumentDetailsAsync_Then_() {
            //Arrange 
            var request = JsonSerializer.Deserialize<CheckPolicyDocumentExistRequest>(
                utilitiesHelper.ReadTestDataJson("PolicyDocument/Data", "RetrievePolicyDocumentDetailsRequestPayloadIsValid.json"));
            ValidateRetrievePolicyDocumentDetailsRequestIsNotNullOrEmptyOrLessThanZero(request);
            //Act
            var response = await policyDocumentAPIClient.RetrievePolicyDocumentDetailsAsync(request);
            var retrievePolicyDocumentDetailsResponse = populateRetrievePolicyDocumentDetailsResponse(response);
            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCodeOK(response);
            ValidateResponseHeadersAreValid(response);
            ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(response);
            ValidateResponseSchemaIsValid(response, "PolicyDocument/Schema", "RetrievePolicyDocumentDetailsResponseSchema.json");
            ValidateRetrievePolicyDocumentDetailsResponseIsNotNullOrEmptyOrLessThanZero(retrievePolicyDocumentDetailsResponse);
        }
        private RetrievePolicyDocumentDetailsResponse populateRetrievePolicyDocumentDetailsResponse(RestResponse response)
        {
            using JsonDocument jsonDoc = JsonDocument.Parse(response.Content);
            var retrievePolicyDocumentDetailsResponse = new RetrievePolicyDocumentDetailsResponse
            {
                executionOutcome = new ExecutionOutcome(),
                checkPolicyDocumentExistRequest = new CheckPolicyDocumentExistRequest()
            };
            foreach (var property in jsonDoc.RootElement.EnumerateObject())
            {
                switch (property.Name)
                {
                    case "succeeded":
                        retrievePolicyDocumentDetailsResponse.executionOutcome.succeeded = (bool)utilitiesHelper.ReadBooleanNullable(property.Value); break;
                    case "message":
                        retrievePolicyDocumentDetailsResponse.executionOutcome.message = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "errors":
                        retrievePolicyDocumentDetailsResponse.executionOutcome.errors = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "data":
                        var items = property.Value;
                        switch (items.ValueKind) {
                            case JsonValueKind.Object:
                                foreach (var dataProperty in items.EnumerateObject())
                                {
                                    switch (dataProperty.Name)
                                    {
                                        case "sourceSystem":
                                            retrievePolicyDocumentDetailsResponse.checkPolicyDocumentExistRequest.sourceSystem =        utilitiesHelper.ReadStringNullable(dataProperty.Value); break;
                                        case "policyDocumentNo":
                                            retrievePolicyDocumentDetailsResponse.checkPolicyDocumentExistRequest.policyDocumentNo =    utilitiesHelper.ReadInt32Nullable(dataProperty.Value); break;
                                        case "policyNo":
                                            retrievePolicyDocumentDetailsResponse.checkPolicyDocumentExistRequest.policyNo =            utilitiesHelper.ReadInt32Nullable(dataProperty.Value); break;
                                        case "documentId":
                                            retrievePolicyDocumentDetailsResponse.checkPolicyDocumentExistRequest.documentId =          utilitiesHelper.ReadInt32Nullable(dataProperty.Value); break;
                                        case "processCd":
                                            retrievePolicyDocumentDetailsResponse.checkPolicyDocumentExistRequest.processCd =           utilitiesHelper.ReadInt32Nullable(dataProperty.Value); break;
                                        case "statusId":
                                            retrievePolicyDocumentDetailsResponse.checkPolicyDocumentExistRequest.statusId =            utilitiesHelper.ReadInt32Nullable(dataProperty.Value); break;
                                        case "statusDate":
                                            retrievePolicyDocumentDetailsResponse.checkPolicyDocumentExistRequest.statusDate =          utilitiesHelper.ReadDateTimeNullable(dataProperty.Value); break;
                                        case "effFrom":
                                            retrievePolicyDocumentDetailsResponse.checkPolicyDocumentExistRequest.effFrom =             utilitiesHelper.ReadDateTimeNullable(dataProperty.Value); break;
                                        case "effTo":
                                            retrievePolicyDocumentDetailsResponse.checkPolicyDocumentExistRequest.effTo =               utilitiesHelper.ReadDateTimeNullable(dataProperty.Value); break;
                                        case "audCreateUser":
                                            retrievePolicyDocumentDetailsResponse.checkPolicyDocumentExistRequest.audCreateUser =       utilitiesHelper.ReadStringNullable(dataProperty.Value); break;
                                        case "audCreateDate":
                                            retrievePolicyDocumentDetailsResponse.checkPolicyDocumentExistRequest.audCreateDate =       utilitiesHelper.ReadDateTimeNullable(dataProperty.Value); break;
                                        case "audModUser":
                                            retrievePolicyDocumentDetailsResponse.checkPolicyDocumentExistRequest.audModUser =          utilitiesHelper.ReadStringNullable(dataProperty.Value); break;
                                        case "audModDate":
                                            retrievePolicyDocumentDetailsResponse.checkPolicyDocumentExistRequest.audModDate =          utilitiesHelper.ReadDateTimeNullable(dataProperty.Value); break;
                                        case "documentTypeCD":
                                            retrievePolicyDocumentDetailsResponse.documentTypeCD =                                      utilitiesHelper.ReadInt32Nullable(dataProperty.Value); break;
                                        case "fileDetails":
                                            retrievePolicyDocumentDetailsResponse.fileDetails =                                         utilitiesHelper.ReadStringNullable(dataProperty.Value); break;
                                        default: TestContext.Out.WriteLine($"Unknown property in data: {dataProperty.Name}"); break;
                                    }
                                }
                                //retrievePolicyDocumentDetailsResponse.checkPolicyDocumentExistRequest.items
                                break;                            
                        }
                        break;
                    default: TestContext.Out.WriteLine($"Unknown property: {property.Name}"); break;
                }
            }
            return retrievePolicyDocumentDetailsResponse;
        }
    }
}
