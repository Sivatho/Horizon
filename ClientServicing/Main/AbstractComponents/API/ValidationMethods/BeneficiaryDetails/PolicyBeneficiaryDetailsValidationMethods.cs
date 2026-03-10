using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.BeneficiaryDetails;
using ClientServicing.Main.Models.BeneficiaryDetails;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.BeneficiaryDetails
{
    public class PolicyBeneficiaryDetailsValidationMethods : AbstractValidationMethods, IPolicyBeneficiaryDetailsValidationMethods
    {
        UtilitiesHelper utilitiesHelper = new UtilitiesHelper();
        public void ValidatePolicyBeneficiaryDetailsRequestIsNotNullOrEmpty_And_GreaterThanOrEqualToZeroOrTypeOfString_And_IsNullOrTypeOfString(PolicyBeneficiaryDetailsRequest policyBeneficiaryDetailsRequest)
        {
            Assert.Multiple(() => { 
                Assert.That(policyBeneficiaryDetailsRequest,                    Is.Not.Null.Or.Not.Empty,                       "Request is Null or Empty.");
                Assert.That(policyBeneficiaryDetailsRequest.policyNo,           Is.GreaterThanOrEqualTo(0).Or.TypeOf<string>(), "Validated: PolicyNo Is Null Or Greater Than Or Equal To 0.");
                Assert.That(policyBeneficiaryDetailsRequest.legacyPolicyNumber, Is.Null.Or.TypeOf<string>(),                    "Validated: LegacyPolicyNumber Is Null Or Type Of String.");
                Assert.That(policyBeneficiaryDetailsRequest.auditToken,         Is.Null.Or.TypeOf<string>(),                    "Validated: AuditToken Is Null Or Type Of String.");
            });
            DocumentTemplate.DisplayBody("Validated: PolicyBeneficiaryDetailsRequest: Is Not Null Or Empty, Greater Than Or Equal To 0 Or Type Of String, Is Null Or Type Of String");
        }
        public void ValidatePolicyBeneficiaryDetailsResponseIsNotNullOrEmpty_And_IsTrueOrFalse_And_IsNullOrTypeOfString_And_IntergerIsNotLessThan0(PolicyBeneficiaryDetailsResponse policyBeneficiaryDetailsResponse)
        {
            Assert.Multiple(() =>
            {
                Assert.That(policyBeneficiaryDetailsResponse,                               Is.Not.Null.Or.Not.Empty,       "Response is Null or Empty.");
                
                Assert.That(policyBeneficiaryDetailsResponse.executionOutcome,              Is.Not.Null.Or.Not.Empty,       "Validated: Execution.Outcome Is Null Or Empty.");
                Assert.That(policyBeneficiaryDetailsResponse.executionOutcome.succeeded,    Is.True.Or.False,               "Validated: Execution.Outcome.Succeeded Is True Or False.");
                Assert.That(policyBeneficiaryDetailsResponse.executionOutcome.message,      Is.Null.Or.TypeOf<string>() ,   "Validated: Execution.Outcome.Message Is Null Or Type Of String.");
                Assert.That(policyBeneficiaryDetailsResponse.executionOutcome.errors,       Is.Null.Or.TypeOf<string>() ,   "Validated: Execution.Outcome.Errors Is Null Or Type Of String.");

                Assert.That(policyBeneficiaryDetailsResponse.data,                          Is.Not.Null.Or.Not.Empty,       "Validated: Data Is Null oO Empty.");
                Assert.That(policyBeneficiaryDetailsResponse.data.totalAllocated,           Is.Not.LessThan(0),             "Validated: Data.Total Allocated Is Not Less Than 0.");
                Assert.That(policyBeneficiaryDetailsResponse.data.policyNo,                 Is.Not.LessThan(0),             "Validated: Data.PolicyNo Is Not Less Than 0.");
                Assert.That(policyBeneficiaryDetailsResponse.data.auditToken,               Is.Null.Or.TypeOf<string>(),    "Validated: Data.AuditToken Is Null Or Type Of String.");

                Assert.That(policyBeneficiaryDetailsResponse.data.beneficiaryDetailsItems,  Is.Not.Null.Or.Not.Empty, "BeneficiaryDetailsItems property is Null or Empty.");
                Assert.That(policyBeneficiaryDetailsResponse.data.beneficiaryDetailsItems,  Is.InstanceOf<List<BeneficiaryDetailsItems>>(), "BeneficiaryDetailsItems property is not of type List<BeneficiaryDetailsItems>.");
                Assert.That(policyBeneficiaryDetailsResponse.data.beneficiaryDetailsItems.Count, Is.GreaterThanOrEqualTo(0), "BeneficiaryDetailsItems list count is less than 0.");
                Assert.That(policyBeneficiaryDetailsResponse.data.beneficiaryDetailsItems,  Has.All.Matches<BeneficiaryDetailsItems>(item =>
                    item.RowNumber >= 0 &&
                    item.PolicyNo >= 0 &&
                    (item.FirstName == null                     || item.FirstName is string) &&
                    (item.Surname == null                       || item.Surname is string) &&
                    (item.TitleCd == null                       || item.TitleCd is int) &&
                    (item.TitleDescr == null                    || item.TitleDescr is string) &&
                    (item.StatusCd == null                      || item.StatusCd is int) &&
                    (item.Status == null                        || item.Status is string) &&
                    (item.EntityRelationId == null              || item.EntityRelationId is int) &&
                    (item.RelationCd == null                    || item.RelationCd is int) &&
                    (item.RelationDescr == null                 || item.RelationDescr is string) &&
                    (item.LegalReferenceNumber == null          || item.LegalReferenceNumber is string) &&
                    (item.LegalReferenceNumberMasked == null    || item.LegalReferenceNumberMasked is string) &&
                    (item.LegalReferenceNumberTypeCd == null    || item.LegalReferenceNumberTypeCd is int) &&
                    (item.PercAllocation == null                || item.PercAllocation is double) &&
                    (item.DateOfBirth != null                   || item.DateOfBirth is DateTime) &&
                    (item.Status == null                        || item.Status is string) &&
                    (item.PhysicalAddress == null               || item.PhysicalAddress is string) &&
                    (item.AddressLine2 == null                  || item.AddressLine2 is string) &&
                    (item.Suburb == null                        || item.Suburb is string) &&
                    (item.AddressCity == null                   || item.AddressCity is string) &&
                    (item.AddressPostCode == null               || item.AddressPostCode is string) &&
                    (item.CellNumber == null                    || item.CellNumber is string) &&
                    (item.CellNumberMasked == null              || item.CellNumberMasked is string) &&
                    (item.HomeNumber == null                    || item.HomeNumber is string) &&
                    (item.WorkNumber == null                    || item.WorkNumber is string) &&
                    (item.EmailAddress == null                  || item.EmailAddress is string) &&
                    (item.FullName == null                      || item.FullName is string) &&
                    (item.TotalPercentageAvailable == null      || item.TotalPercentageAvailable is int) &&
                    (item.Role == null                          || item.Role is string) &&
                    (item.GenderCd == null                      || item.GenderCd is int) &&
                    (item.AuditToken == null                    || item.AuditToken is string) &&
                    (item.BankAccNo == null                     || item.BankAccNo is string)
                    ), "One or more items in BeneficiaryDetailsItems list do not match the expected structure and data types.");
            });
            DocumentTemplate.DisplayBody("Validated: PolicyBeneficiaryDetailsResponse: Is Not Null Or Empty, Is True Or False, Is Null Or Type Of String, Interger Is Not Less Than 0");
        }
        public PolicyBeneficiaryDetailsResponse PopulatePolicyBeneficiaryDetailsResponse(RestResponse restResponse)
        {
            using JsonDocument doc = JsonDocument.Parse(restResponse.Content);

            PolicyBeneficiaryDetailsResponse policyBeneficiaryDetailsResponse = new PolicyBeneficiaryDetailsResponse
            {
                executionOutcome = new ExecutionOutcome(),
                data = new BeneficiaryDetailsData()
            };
            foreach (var property in doc.RootElement.EnumerateObject())
            {
                switch (property.Name)
                {
                    case "succeeded":
                        policyBeneficiaryDetailsResponse.executionOutcome.succeeded = (bool)utilitiesHelper.ReadBooleanNullable(property.Value)!;
                        break;
                    case "message":
                        policyBeneficiaryDetailsResponse.executionOutcome.message = utilitiesHelper.ReadStringNullable(property.Value);
                        break;
                    case "errors":
                        policyBeneficiaryDetailsResponse.executionOutcome.errors = utilitiesHelper.ReadStringNullable(property.Value);
                        break;
                    case "data":
                        var dataElement = property.Value;
                        switch (dataElement.ValueKind)
                        {
                            case JsonValueKind.Object:
                                foreach (var dataProperty in dataElement.EnumerateObject())
                                {
                                    switch (dataProperty.Name)
                                    {
                                        case "totalAllocated":
                                            policyBeneficiaryDetailsResponse.data.totalAllocated = (int)utilitiesHelper.ReadInt32Nullable(dataProperty.Value)!;
                                            break;
                                        case "policyNo":
                                            policyBeneficiaryDetailsResponse.data.policyNo = (int)utilitiesHelper.ReadInt32Nullable(dataProperty.Value)!;
                                            break;
                                        case "beneficiaryDetailsItems":
                                            var items = new List<BeneficiaryDetailsItems>();
                                            foreach (var item in dataProperty.Value.EnumerateArray())
                                            {
                                                var beneficiaryItem = new BeneficiaryDetailsItems();
                                                foreach (var itemProperty in item.EnumerateObject())
                                                {
                                                    switch (itemProperty.Name)
                                                    {
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
                                                            beneficiaryItem.AuditToken = utilitiesHelper.ReadStringNullable(itemProperty.Value)!;
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
        public override void ValidateResponseFieldParametersIsValid(RestResponse restResponse)
        {
            throw new NotImplementedException();
        }
        public override void ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(RestResponse restResponse)
        {
            throw new NotImplementedException();
        }

       
    }
}
