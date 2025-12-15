using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.BeneficiaryDetails;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.Bank;
using ClientServicing.Main.Models.BeneficiaryDetails;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Resources.Helper;
using RestSharp;
{
    
}

namespace ClientServicing.Test.Tests.API.TDD.BeneficiaryDetails
{
    [TestFixture]
    public class PolicyBeneficiaryDetailsAPITest : PolicyBeneficiaryDetailsValidationMethods
    {
        BeneficiaryDetailsAPIClient beneficiaryDetailsAPIClient = new();
        UtilitiesHelper utilitiesHelper = new();

        [Test]
        public async Task Given_PolicyBeneficiaryDetailsRequest_When_PolicyBeneficiaryDetailsAsync_Then_ExpectedResult()
        {
            // Arrange            
            PolicyBeneficiaryDetailsRequest beneficiaryDetailsRequest = JsonSerializer.Deserialize<PolicyBeneficiaryDetailsRequest>(utilitiesHelper.ReadTestDataJson("BeneficiaryDetails/Data", "PolicyBeneficiaryDetailsPayloadIsValid.json"));

            // Act
            var response = await beneficiaryDetailsAPIClient.PolicyBeneficiaryDetailsAsync(beneficiaryDetailsRequest);
            var policyBeneficiaryDetailsResponse = populatePolicyBeneficiaryDetailsResponse(response);

            // Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCodeOK(response);
            ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(response);
            ValidateResponseIsNotNullOrEmpty(policyBeneficiaryDetailsResponse);
            ValidateResponseSchemaIsValid(response, "BeneficiaryDetails/Schema", "BeneficiaryDetailsResponseSchema.json");
        
        }
        private PolicyBeneficiaryDetailsResponse populatePolicyBeneficiaryDetailsResponse(RestResponse restResponse)
        {
            using JsonDocument doc = JsonDocument.Parse(restResponse.Content);

            PolicyBeneficiaryDetailsResponse policyBeneficiaryDetailsResponse = new PolicyBeneficiaryDetailsResponse
            {
                executionOutcome = new ExecutionOutcome(),
                data = new BeneficiaryDetailsData()
            };
            foreach (var property in doc.RootElement.EnumerateObject()) {
                switch (property.Name) {
                    case "succeeded":
                        policyBeneficiaryDetailsResponse.executionOutcome.succeeded = (bool)utilitiesHelper.ReadBooleanNullable(property.Value);
                        break;
                    case "message":
                        policyBeneficiaryDetailsResponse.executionOutcome.message = utilitiesHelper.ReadStringNullable(property.Value);
                        break;
                    case "errors":
                        policyBeneficiaryDetailsResponse.executionOutcome.errors = utilitiesHelper.ReadStringNullable(property.Value);
                        break;
                    case "data":
                        var dataElement = property.Value;
                        switch(dataElement.ValueKind) {
                            case JsonValueKind.Object:
                                foreach (var dataProperty in dataElement.EnumerateObject()) {
                                    switch (dataProperty.Name) {
                                        case "totalAllocated":
                                            policyBeneficiaryDetailsResponse.data.totalAllocated = (int)utilitiesHelper.ReadInt32Nullable(dataProperty.Value);
                                            break;
                                        case "policyNo":
                                            policyBeneficiaryDetailsResponse.data.policyNo = (int)utilitiesHelper.ReadInt32Nullable(dataProperty.Value);
                                            break;
                                        case "beneficiaryDetailsItems":
                                            var items = new List<BeneficiaryDetailsItems>();
                                            foreach (var item in dataProperty.Value.EnumerateArray()) {
                                                var beneficiaryItem = new BeneficiaryDetailsItems();
                                                foreach (var itemProperty in item.EnumerateObject()) {
                                                    switch (itemProperty.Name) {
                                                        case "rownumber":
                                                            beneficiaryItem.RowNumber = utilitiesHelper.ReadInt32Nullable(itemProperty.Value);
                                                            break;
                                                        case "policyNo":
                                                            beneficiaryItem.PolicyNo = utilitiesHelper.ReadInt32Nullable(itemProperty.Value);
                                                            break;
                                                        case "firstName":
                                                            beneficiaryItem.FirstName = utilitiesHelper.ReadStringNullable(itemProperty.Value);
                                                            break;
                                                        case "surname":
                                                            beneficiaryItem.Surname = utilitiesHelper.ReadStringNullable(itemProperty.Value);
                                                            break;
                                                        case "titleCd":
                                                            beneficiaryItem.TitleCd = utilitiesHelper.ReadInt32Nullable(itemProperty.Value);
                                                            break;
                                                        case "titleDescr":
                                                            beneficiaryItem.TitleDescr = utilitiesHelper.ReadStringNullable(itemProperty.Value);
                                                            break;
                                                        case "statusCd":
                                                            beneficiaryItem.StatusCd = utilitiesHelper.ReadInt32Nullable(itemProperty.Value);
                                                            break;
                                                        case "statusDescr":
                                                            beneficiaryItem.Status = utilitiesHelper.ReadStringNullable(itemProperty.Value);
                                                            break;
                                                        case "entityRelationId":
                                                            beneficiaryItem.EntityRelationId = utilitiesHelper.ReadInt32Nullable(itemProperty.Value);
                                                            break;
                                                        case "relationCd":
                                                            beneficiaryItem.RelationCd = utilitiesHelper.ReadInt32Nullable(itemProperty.Value);
                                                            break;
                                                        case "relationDescr":
                                                            beneficiaryItem.RelationDescr = utilitiesHelper.ReadStringNullable(itemProperty.Value);
                                                            break;
                                                        case "legalReferenceNumber":
                                                            beneficiaryItem.LegalReferenceNumber = utilitiesHelper.ReadStringNullable(itemProperty.Value);
                                                            break;
                                                        case "legalReferenceNumberMasked":
                                                            beneficiaryItem.LegalReferenceNumberMasked = utilitiesHelper.ReadStringNullable(itemProperty.Value);
                                                            break;
                                                        case "legalReferenceNumberTypeCd":
                                                            beneficiaryItem.LegalReferenceNumberTypeCd = utilitiesHelper.ReadInt32Nullable(itemProperty.Value);
                                                            break;
                                                        case "percAllocation":
                                                            beneficiaryItem.PercAllocation = utilitiesHelper.ReadInt32Nullable(itemProperty.Value);
                                                            break;
                                                        case "dateOfBirth":
                                                            beneficiaryItem.DateOfBirth = utilitiesHelper.ReadDateTimeNullable(itemProperty.Value);
                                                            break;
                                                        case "status":
                                                            beneficiaryItem.Status = utilitiesHelper.ReadStringNullable(itemProperty.Value);
                                                            break;
                                                        case "physicalAddress":
                                                            beneficiaryItem.PhysicalAddress = utilitiesHelper.ReadStringNullable(itemProperty.Value);
                                                            break;
                                                        case "addressLine2":
                                                            beneficiaryItem.AddressLine2 = utilitiesHelper.ReadStringNullable(itemProperty.Value);
                                                            break;
                                                        case "suburb":
                                                            beneficiaryItem.Suburb = utilitiesHelper.ReadStringNullable(itemProperty.Value);
                                                            break;
                                                        case "addressCity":
                                                            beneficiaryItem.AddressCity = utilitiesHelper.ReadStringNullable(itemProperty.Value);
                                                            break;
                                                        case "addressPostCode":
                                                            beneficiaryItem.AddressPostCode = utilitiesHelper.ReadStringNullable(itemProperty.Value);
                                                            break;
                                                        case "cellNumber":
                                                            beneficiaryItem.CellNumber = utilitiesHelper.ReadStringNullable(itemProperty.Value);
                                                            break;
                                                        case "cellNumberMasked":
                                                            beneficiaryItem.CellNumberMasked = utilitiesHelper.ReadStringNullable(itemProperty.Value);
                                                            break;
                                                        case "homeNumber":
                                                            beneficiaryItem.HomeNumber = utilitiesHelper.ReadStringNullable(itemProperty.Value);
                                                            break;
                                                        case "workNumber":
                                                            beneficiaryItem.WorkNumber = utilitiesHelper.ReadStringNullable(itemProperty.Value);
                                                            break;
                                                        case "emailAddress":
                                                            beneficiaryItem.EmailAddress = utilitiesHelper.ReadStringNullable(itemProperty.Value);
                                                            break;
                                                        case "fullname":
                                                            beneficiaryItem.FullName = utilitiesHelper.ReadStringNullable(itemProperty.Value);
                                                            break;
                                                        case "totalPercentageAvailable":
                                                            beneficiaryItem.TotalPercentageAvailable = utilitiesHelper.ReadInt32Nullable(itemProperty.Value);
                                                            break;
                                                        case "role":
                                                            beneficiaryItem.Role = utilitiesHelper.ReadStringNullable(itemProperty.Value);
                                                            break;
                                                        case "genderCd":
                                                            beneficiaryItem.GenderCd = utilitiesHelper.ReadInt32Nullable(itemProperty.Value);
                                                            break;
                                                        case "auditToken":
                                                            beneficiaryItem.AuditToken = utilitiesHelper.ReadStringNullable(itemProperty.Value);
                                                            break;
                                                        case "bankAccNo":
                                                            beneficiaryItem.BankAccNo = utilitiesHelper.ReadStringNullable(itemProperty.Value);
                                                            break;
                                                    }
                                                }
                                                items.Add(beneficiaryItem);
                                            }
                                            policyBeneficiaryDetailsResponse.data.beneficiaryDetailsItems = items;
                                            break;
                                    }
                                }
                                break;
                        }
                        break;
                    default:
                        TestContext.Out.WriteLine($"Unknown property: {property.Name}");
                        break;
                }
            }
            return policyBeneficiaryDetailsResponse;
        }
    }
}
