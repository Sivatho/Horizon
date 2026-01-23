using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.Policy;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Models.Policy;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.Policy
{
    public class FetchPolicyStatusValidationMethods : AbstractValidationMethods, IFetchPolicyStatusValidationMethods
    { UtilitiesHelper utilitiesHelper = new();    
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
                        JsonValueKind.False, JsonValueKind.True
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
                    AllowedKinds = new[] { JsonValueKind.Object },
                    NestedRules = new Dictionary<string, JsonValueKind[]>
                    {
                        { "legacyPolicyNo", new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "policyNo",       new[] { JsonValueKind.Null, JsonValueKind.Number } },
                        { "status",         new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "statusCD",       new[] { JsonValueKind.Null, JsonValueKind.Number } },
                        { "statusDate",     new[] { JsonValueKind.Null, JsonValueKind.String } }                       
                    }
                }
            };
            using var doc = JsonDocument.Parse(restResponse.Content);
            JsonValidationRule.ValidateJson(doc.RootElement, rules);
            TestContext.Out.WriteLine("Validated: Response Contects and data types are valid");
        }
        public void ValidateFetchPolicyStatusRequestDataIsNotNullOrEmpty(PolicyNoRequest fetchPolicyStatusRequest)
        {
            Assert.Multiple(() => {
                Assert.That(fetchPolicyStatusRequest.policyNo, Is.GreaterThanOrEqualTo(0), "FetchPolicyStatusRequest: policyNo should be greater or equal to zero");
            });
        }
        public void ValidateFetchPolicyStatusResponseDataIsNotNullOrEmpty(FetchPolicyStatusResponse fetchPolicyStatusResponse)
        {
            Assert.Multiple(() => {
                Assert.That(fetchPolicyStatusResponse.executionOutcome,     Is.Not.Null,                        "FetchPolicyStatusResponse: executionOutcome should not be null");
                Assert.That(fetchPolicyStatusResponse.data,                 Is.Not.Null,                        "FetchPolicyStatusResponse: data should not be null");
                Assert.That(fetchPolicyStatusResponse.data.legacyPolicyNo,  Is.Not.Null,                        "FetchPolicyStatusResponse: data.legacyPolicyNo should not be null");
                Assert.That(fetchPolicyStatusResponse.data.policyNo,        Is.GreaterThanOrEqualTo(0),         "FetchPolicyStatusResponse: data.policyNo should be greater or equal to zero");
                Assert.That(fetchPolicyStatusResponse.data.status,          Is.Not.Null.And.Not.Empty,          "FetchPolicyStatusResponse: data.status should not be null or empty");
                Assert.That(fetchPolicyStatusResponse.data.statusCD,        Is.GreaterThanOrEqualTo(0),         "FetchPolicyStatusResponse: data.statusCD should be greater or equal to zero");
                Assert.That(fetchPolicyStatusResponse.data.statusDate,      Is.Not.EqualTo(default(DateTime)),  "FetchPolicyStatusResponse: data.statusDate should not be default (DateTime.MinValue)"); 
            });
        }
        public FetchPolicyStatusResponse populateFetchPolicyStatusResponse(RestResponse restResponse)
        {
            using JsonDocument jsDoc = JsonDocument.Parse(restResponse.Content);
            var fetchPolicyStatusResponse = new FetchPolicyStatusResponse
            {
                executionOutcome = new ExecutionOutcome(),
                data = new PolicyStatus()
            };
            foreach (var property in jsDoc.RootElement.EnumerateObject())
            {
                switch (property.Name)
                {
                    case "succeeded": fetchPolicyStatusResponse.executionOutcome.succeeded  = (bool)utilitiesHelper.ReadBooleanNullable(property.Value); break;
                    case "message": fetchPolicyStatusResponse.executionOutcome.message      = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "errors": fetchPolicyStatusResponse.executionOutcome.errors        = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "data":
                        foreach (var item in property.Value.EnumerateObject())
                        {
                            switch (item.Name)
                            {
                                case "legacyPolicyNo": fetchPolicyStatusResponse.data.legacyPolicyNo    = utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "policyNo": fetchPolicyStatusResponse.data.policyNo                = (int)utilitiesHelper.ReadInt32Nullable(item.Value); break;
                                case "status": fetchPolicyStatusResponse.data.status                    = utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "statusCD": fetchPolicyStatusResponse.data.statusCD                = (int)utilitiesHelper.ReadInt32Nullable(item.Value); break;
                                case "statusDate": fetchPolicyStatusResponse.data.statusDate            = (DateTime)utilitiesHelper.ReadDateTimeNullable(item.Value); break;
                                default: TestContext.Out.WriteLine($"Unknown property in data: {item.Name}"); break;
                            }
                        }
                        break;
                    default: TestContext.Out.WriteLine($"Unknown property: {property.Name}"); break;
                }               
            }
            return fetchPolicyStatusResponse;
        }            
    }
}