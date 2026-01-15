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
        public void ValidateFetchBanksRequestDataIsNotNullOrEmptyOrLessThanZero(FetchBanksRequest fetchBanksRequest)
        {
            throw new NotImplementedException();
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

        public void ValidatFetchBanksResponseDataIsNotNullOrEmpty(FetchBanksResponse fetchBanksResponse)
        {
            Assert.Multiple(() =>
            {
                Assert.That(fetchBanksResponse.responseMessage, Is.Not.Null, "Fetch Banks Response: Response Message should not be null");

                Assert.That(fetchBanksResponse.data, Is.Not.Null.And.Not.Empty, "Data should not be null or empty");

                // Validate all items are well-formed
                Assert.That(
                    fetchBanksResponse.data,
                    Has.All.Matches<FetchBanksRequest>(b =>
                        b.bankID.HasValue && b.bankID.Value >= 0 &&
                        b.lastChanged.HasValue && b.lastChanged.Value != default(DateTime)
                    ),
                    "Each item must have bankID >= 0 and lastChanged != default"
                );

            });
            TestContext.Out.WriteLine("Response: Message and Data are not null or empty as expected.");
        }

        public FetchBanksResponse populateFetchBanksResponse(RestResponse response)
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

    }
}
