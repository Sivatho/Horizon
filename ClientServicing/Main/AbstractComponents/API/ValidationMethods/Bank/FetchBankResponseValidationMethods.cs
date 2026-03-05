using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.Bank;
using ClientServicing.Main.Models.Bank;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.Bank
{
    public class FetchBankResponseValidationMethods : AbstractValidationMethods, IFetchBankResponseValidationMethods
    {
        public void ValidateFetchBanksRequestData_When_IsNotNullAndNotEmpty(FetchBanksRequest fetchBanksRequest)
        {
            Assert.That(fetchBanksRequest, Is.Not.Null, "Validated: FetchBanksRequest Should Not Be Null");
        }
        public void ValidateFetchBanksRequestData_When_IsNotNullAndNotEmpty_And_GreaterOrEqualZero_And_NotEqualToDefaultDateTime(FetchBanksRequest fetchBanksRequest)
        {
            Assert.Multiple(() =>
            {
                Assert.That(fetchBanksRequest.bankID,               Is.Not.Null,                        "Validated: BankID Should Not Be Null");
                Assert.That(fetchBanksRequest.bankID,               Is.GreaterThanOrEqualTo(0),         "Validated: BankID Should Be Greater Than Or Equal to 0");
                Assert.That(fetchBanksRequest.bankName,             Is.Not.Null.And.Not.Empty,          "Validated: BankName Should Not be Null Or Empty");
                Assert.That(fetchBanksRequest.dispSeq,              Is.GreaterThanOrEqualTo(0),         "Validated: DispSeq Should Be Greater Than Or Equal to 0");
                Assert.That(fetchBanksRequest.lastChanged,          Is.Not.Null,                        "Validated: LastChanged Should Not Be Null");
                Assert.That(fetchBanksRequest.lastChanged!.Value,   Is.Not.EqualTo(default(DateTime)),  "Validated: LastChanged Should Not Be Default DateTime");
            });
            DocumentTemplate.DisplayBody("FetchBanksRequest: When_IsNotNullAndNotEmpty_And_GreaterOrEqualZero_And_NotEqualToDefaultDateTime: As Expected");
        }
        public void ValidatFetchBanksResponseData_When_IsNotNullAndNotEmpty_And_GreaterOrEqualZero_And_NotEqualToDefaultDateTime(FetchBanksResponse fetchBanksResponse)
        {
            Assert.Multiple(() =>
            {
                Assert.That(fetchBanksResponse.responseMessage, Is.Not.Null,                "Fetch Banks Response: Response Message should not be null");
                Assert.That(fetchBanksResponse.data,            Is.Not.Null.And.Not.Empty,  "Data should not be null or empty");

                // Validate all items are well-formed
               Assert.That(
                    fetchBanksResponse.data,
                    Has.All.Matches<FetchBanksRequest>(b =>
                        b.bankID.HasValue && b.bankID.Value >= 0
                        && b.bankName != null && b.bankName != string.Empty
                        && b.bankShortName == null || b.bankShortName != string.Empty
                        && b.dispSeq.HasValue && b.dispSeq.Value >= 0
                        && b.isActive.HasValue && (b.isActive.Value == true || b.isActive.Value == false)
                        && b.lastChanged.HasValue && b.lastChanged.Value != default(DateTime)                                             
                        && b.userID != null && b.userID != string.Empty                        
                    ),
                    "Each item must have bankID >= 0 " +                    
                    "And bankName != Null Or bankName != String.Empty" +
                    "And bankShortName != Null Or bankShortName != String.Empty" +                    
                    "And dispSeq >= 0 " +
                    "And isActive == true Or  isActive == false" +              
                    "And lastChanged != default" +
                    "And userID != Null Or userID != String.Empty" 
                );

            });
            DocumentTemplate.DisplayBody("FetchBanksResponse: IsNotNull And NotEmpty And GreaterOrEqualZero And NotEqualToDefaultDateTime : As Expected");
        }
        public FetchBanksResponse PopulateFetchBanksResponse(RestResponse response)
        {
            using JsonDocument doc = JsonDocument.Parse(response.Content);

            FetchBanksResponse fetchBanksResponse = new FetchBanksResponse
            {
                responseMessage = new ExecutionOutcome(),
                data = new List<FetchBanksRequest>()
            };

            foreach (var property in doc.RootElement.EnumerateObject())
            {
                switch (property.Name.ToLower())
                {
                    case "succeeded":
                        fetchBanksResponse.responseMessage.succeeded = property.Value.GetBoolean();
                        break;
                    case "message":
                        fetchBanksResponse.responseMessage.message = property.Value.GetString();
                        break;
                    case "errors":
                        fetchBanksResponse.responseMessage.errors = property.Value.GetString();
                        break;
                    case "data":
                        foreach (var item in property.Value.EnumerateArray())
                        {
                            var bank = new FetchBanksRequest
                            {
                                bankID = item.GetProperty("bankID").GetInt32(),
                                bankName = item.GetProperty("bankName").GetString(),
                                bankShortName = item.GetProperty("bankShortName").GetString(),
                                dispSeq = item.GetProperty("dispSeq").GetInt32(),
                                isActive = item.GetProperty("isActive").GetBoolean(),
                                lastChanged = item.GetProperty("lastChanged").GetDateTime(),
                                userID = item.GetProperty("userID").GetString()
                            };
                            fetchBanksResponse.data.Add(bank);
                        }
                        break;
                    default:
                        TestContext.Out.WriteLine($"Unkown property: {property.Name}");
                        break;
                }
            }
            return fetchBanksResponse;
        }
        public override void ValidateResponseFieldParametersIsValid(RestResponse restResponse)
        {
            throw new NotImplementedException();
        }
        public override void ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(RestResponse restResponse)
        {
            var rules = new List<JsonValidationRule> {
                new JsonValidationRule {
                    PropertyName = "succeeded",
                    AllowedKinds = new[] {
                        JsonValueKind.True, JsonValueKind.False
                    }
                },
                new JsonValidationRule {
                    PropertyName = "message",
                    AllowedKinds = new[] {
                        JsonValueKind.String, JsonValueKind.Null
                    }
                },
                new JsonValidationRule {
                    PropertyName = "errors",
                    AllowedKinds = new[] {
                        JsonValueKind.String, JsonValueKind.Null
                    }
                },
                new JsonValidationRule {
                    PropertyName = "data",
                    AllowedKinds = new[] { JsonValueKind.Array },
                    NestedRules = new Dictionary<string, JsonValueKind[]>
                    {
                        { "bankID", new[] { JsonValueKind.Number } },
                        { "bankName", new[] { JsonValueKind.String } },
                        { "bankShortName", new[] { JsonValueKind.String, JsonValueKind.Null } },
                        { "dispSeq", new[] { JsonValueKind.Number } },
                        { "isActive", new[] { JsonValueKind.True, JsonValueKind.False } },
                        { "lastChanged", new[] { JsonValueKind.String } }, // DateTime as string
                        { "userID", new[] { JsonValueKind.String } }
                    }
                }
            };

            using var doc = JsonDocument.Parse(restResponse.Content);
            JsonValidationRule.ValidateJson(doc.RootElement, rules);
            TestContext.Out.WriteLine("Response: contents and data types are valid.");
        }       
    }
}
