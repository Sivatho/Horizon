using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.Bank;
using ClientServicing.Main.Models.Bank;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.Bank
{
    public class FetchBankResponseValidationMethods : AbstractValidationMethods, IFetchBankResponseValidationMethods
    {
        public void ValidateFetchBankDataIsNotNullOrEmpty(FetchBanksResponse fetchBanksResponse)
        {
            throw new NotImplementedException();
        }

        public override void ValidateResponsePropertyNameIsValid_AndDataTypesIsValid(RestResponse restResponse)
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
            TestContext.Out.WriteLine("FetchBanks Response: Response contents and data types are valid.");
        }

        public void ValidateResponseIsNotNullOrEmpty(FetchBanksResponse fetchBanksResponse)
        {
            Assert.Multiple(() =>
            {
                Assert.That(fetchBanksResponse.responseMessage, Is.Not.Null, "Fetch Banks Response: Response Message should not be null");
                Assert.That(fetchBanksResponse.data,            Is.Not.Null, "FetchBanks Response: Data should not be null");
            });
            TestContext.Out.WriteLine("FetchBanks Response: Response Message and Data are not null or empty as expected.");
        }

        public void ValidateResponseIsNullOrWhiteSpace(FetchBanksResponse fetchBanksResponse)
        {
            throw new NotImplementedException();
        }

        public override void ValidateResponseSchemaIsValid(RestResponse restResponse, string folder, string jsonfile)
        {
            UtilitiesHelper utilitiesHelper = new UtilitiesHelper();
            var schemaJson = utilitiesHelper.ReadJson(folder, jsonfile);
            utilitiesHelper.ValidateJsonSchema(restResponse.Content, schemaJson);
            TestContext.Out.WriteLine("ValidateJsonSchema: Response content matches the expected JSON schema and is valid.");
        }
    }
}
