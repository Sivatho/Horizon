using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.PolicyDocument;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Test.Tests.API.TDD.PolicyDocument
{
    [TestFixture]
    public class RetrievePolicyDocumentsAPITests
    {
        PolicyDocumentAPIClient policyDocumentAPIClient = new PolicyDocumentAPIClient();
        UtilitiesHelper utilitiesHelper = new UtilitiesHelper();

        [Test]
        //[Ignore("Test data needs to be updated output data is not as expected: Validations need to be added")]
        public async Task Given_RetrievePolicyDocumentDetailsRequestPayloadIsValid_When_RetrievePolicyDocumentsAsync_Then_()
        {
            //Arrange 
            var request = JsonSerializer.Deserialize<CheckPolicyDocumentExistRequest>(
                utilitiesHelper.ReadTestDataJson("PolicyDocument/Data", "RetrievePolicyDocumentDetailsRequestPayloadIsValid.json"));
            //Act
            var response = await policyDocumentAPIClient.RetrievePolicyDocumentsAsync(request);
            var retrievePolicyDocumentDetailsResponse = populateRetrievePolicyDocumentDetailsResponse(response);
            //Assert
        }
        public RetrievePolicyDocumentDetailsResponse populateRetrievePolicyDocumentDetailsResponse(RestResponse response)
        {
            using JsonDocument jsonDoc = JsonDocument.Parse(response.Content);
            var retrievePolicyDocumentDetailsResponse = new RetrievePolicyDocumentDetailsResponse
            {
                executionOutcome = new Main.Models.General.ExecutionOutcome(),
                data = new CheckPolicyDocumentExistRequest(),
                fileDetails = new RetrievePolicyDocumentFileDetails()
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
                        var dataElement = property.Value;
                        switch (dataElement.ValueKind)
                        {
                            case JsonValueKind.Object:
                                foreach (var dataProperty in dataElement.EnumerateObject())
                                {
                                    switch (dataProperty.Name)
                                    {
                                        case "sourceSystem":        retrievePolicyDocumentDetailsResponse.data.sourceSystem =       utilitiesHelper.ReadStringNullable(dataProperty.Value); break;
                                        case "policyDocumentNo":    retrievePolicyDocumentDetailsResponse.data.policyDocumentNo =   utilitiesHelper.ReadInt32Nullable(dataProperty.Value); break;
                                        case "policyNo":            retrievePolicyDocumentDetailsResponse.data.policyNo =           utilitiesHelper.ReadInt32Nullable(dataProperty.Value); break;
                                        case "documentId":          retrievePolicyDocumentDetailsResponse.data.documentId =         utilitiesHelper.ReadInt32Nullable(dataProperty.Value); break;
                                        case "documentTypeCD":      retrievePolicyDocumentDetailsResponse.documentTypeCD =          utilitiesHelper.ReadInt32Nullable(dataProperty.Value); break;
                                        case "processCd":           retrievePolicyDocumentDetailsResponse.data.processCd =          utilitiesHelper.ReadInt32Nullable(dataProperty.Value); break;
                                        case "statusId":            retrievePolicyDocumentDetailsResponse.data.statusId =           utilitiesHelper.ReadInt32Nullable(dataProperty.Value); break;
                                        case "statusDate":          retrievePolicyDocumentDetailsResponse.data.statusDate =         utilitiesHelper.ReadDateTimeNullable(dataProperty.Value); break;
                                        case "effFrom":             retrievePolicyDocumentDetailsResponse.data.effFrom =            utilitiesHelper.ReadDateTimeNullable(dataProperty.Value); break;
                                        case "effTo":               retrievePolicyDocumentDetailsResponse.data.effTo =              utilitiesHelper.ReadDateTimeNullable(dataProperty.Value); break;
                                        case "audCreateUser":       retrievePolicyDocumentDetailsResponse.data.audCreateUser =      utilitiesHelper.ReadStringNullable(dataProperty.Value); break;
                                        case "audCreateDate":       retrievePolicyDocumentDetailsResponse.data.audCreateDate =      utilitiesHelper.ReadDateTimeNullable(dataProperty.Value); break;
                                        case "audModUser":          retrievePolicyDocumentDetailsResponse.data.audModUser =         utilitiesHelper.ReadStringNullable(dataProperty.Value); break;
                                        case "audModDate":          retrievePolicyDocumentDetailsResponse.data.audModDate =         utilitiesHelper.ReadDateTimeNullable(dataProperty.Value); break;
                                        case "fileDetails":
                                            var fileDetailsElement = dataProperty.Value;
                                            switch (fileDetailsElement.ValueKind)
                                            {
                                                case JsonValueKind.Object:
                                                    foreach (var fileDetailsProperty in fileDetailsElement.EnumerateObject())
                                                    {
                                                        switch (fileDetailsProperty.Name)
                                                        {
                                                            case "fileId":              retrievePolicyDocumentDetailsResponse.fileDetails.fileId =              utilitiesHelper.ReadStringNullable(fileDetailsProperty.Value); break;
                                                            case "referenceId":         retrievePolicyDocumentDetailsResponse.fileDetails.referenceId =         utilitiesHelper.ReadStringNullable(fileDetailsProperty.Value); break;
                                                            case "fileName":            retrievePolicyDocumentDetailsResponse.fileDetails.fileName =            utilitiesHelper.ReadStringNullable(fileDetailsProperty.Value); break;
                                                            case "fileCreatedDate":     retrievePolicyDocumentDetailsResponse.fileDetails.fileCreatedDate =     utilitiesHelper.ReadDateTimeNullable(fileDetailsProperty.Value); break;
                                                            case "base64FileContents":  retrievePolicyDocumentDetailsResponse.fileDetails.base64FileContents =  utilitiesHelper.ReadStringNullable(fileDetailsProperty.Value); break;
                                                            case "fileExtension":       retrievePolicyDocumentDetailsResponse.fileDetails.fileExtension =       utilitiesHelper.ReadStringNullable(fileDetailsProperty.Value); break;
                                                            case "tags":
                                                                var tagsElement = fileDetailsProperty.Value;
                                                                switch (tagsElement.ValueKind)
                                                                {
                                                                    case JsonValueKind.Array:
                                                                        var tagsList = new List<string>();
                                                                        foreach (var tag in tagsElement.EnumerateArray())
                                                                        {
                                                                            tagsList.Add(utilitiesHelper.ReadStringNullable(tag));
                                                                        }
                                                                        retrievePolicyDocumentDetailsResponse.fileDetails.tags = tagsList.ToArray();
                                                                        break;
                                                                }
                                                                break;
                                                                case "properties":
                                                                    var propertiesElement = fileDetailsProperty.Value;
                                                                    switch (propertiesElement.ValueKind)
                                                                    {
                                                                        case JsonValueKind.Array:
                                                                            var propertiesList = new List<Dictionary<string, string>>();
                                                                            foreach (var propertyItem in propertiesElement.EnumerateArray())
                                                                            {
                                                                                var propertyDict = new Dictionary<string, string>();
                                                                                foreach (var prop in propertyItem.EnumerateObject())
                                                                                {
                                                                                    propertyDict[prop.Name] = utilitiesHelper.ReadStringNullable(prop.Value);
                                                                                }
                                                                                propertiesList.Add(propertyDict);
                                                                            }
                                                                            retrievePolicyDocumentDetailsResponse.fileDetails.properties = propertiesList.ToArray();
                                                                            break;
                                                                    }
                                                                    break;
                                                                    default: TestContext.Out.WriteLine($"Unknown property: {fileDetailsProperty.Name}"); break;
                                                        }
                                                    }
                                                    break;
                                                default: TestContext.Out.WriteLine($"Unknown property: {dataProperty.Name}"); break;
                                            }
                                            break;
                                    }
                                }
                                break;
                        }
                        break;
                }
            }
            return retrievePolicyDocumentDetailsResponse;
        }
    }
}
