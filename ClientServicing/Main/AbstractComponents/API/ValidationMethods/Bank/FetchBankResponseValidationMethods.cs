using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.Bank;
using ClientServicing.Main.Models.Bank;
using ClientServicing.Main.Models.General;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.Bank
{
    public class FetchBankResponseValidationMethods : AbstractValidationMethods, IFetchBankResponseValidationMethods
    {
        public void ValidateFetchBankDataIsNotNullOrEmpty(FetchBanksResponse fetchBanksResponse)
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

        public void ValidateResponseIsNotNullOrEmpty(FetchBanksResponse fetchBanksResponse)
        {
            Assert.Multiple(() =>
            {
                Assert.That(fetchBanksResponse.responseMessage, Is.Not.Null, "Fetch Banks Response: Response Message should not be null");
                Assert.That(fetchBanksResponse.data,            Is.Not.Null, "FetchBanks Response: Data should not be null");
            });
            TestContext.Out.WriteLine("Response: Message and Data are not null or empty as expected.");
        }

        public void ValidateResponseIsNullOrWhiteSpace(FetchBanksResponse fetchBanksResponse)
        {
            throw new NotImplementedException();
        }
    }
}
