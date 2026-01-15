using ClientServicing.Main.AbstractComponents.API.IValidationMethods.BenefitExtendedMember;
using ClientServicing.Main.Models.AccountHistory;
using ClientServicing.Main.Models.BeneficiaryDetails;
using ClientServicing.Main.Models.BenefitExtendedMember;
using ClientServicing.Main.Models.Email;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Models.Policy;
using ClientServicing.Main.Resources.Helper;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.BenefitExtendedMember
{
    public class PolicyBenefitExtendedMemberResponseValidationMethods : AbstractValidationMethods, IPolicyBenefitExtendedMemberResponseValidationMethods
    {
        UtilitiesHelper UtilitiesHelper = new UtilitiesHelper();

        public void ValidatePolicyBenefitExtendedMemberResponseDataIsNotNullOrEmpty(policyBenefitExtendedMemberRequest PolicyBenefitExtendedMemberRequest)
        {
            throw new NotImplementedException();
        }


        public void ValidatePolicyBenefitExtendedMemberResponseDataIsNotNullOrEmpty(policyBenefitExtendedMemberResponse PolicyBenefitExtendedMemberResponse)
        {
            Assert.Multiple(() =>
            {
                Assert.That(PolicyBenefitExtendedMemberResponse, Is.Not.Null, "PolicyBenefitExtendedMemberResponse: response object should not be null");

                // message should always be present
                Assert.That(PolicyBenefitExtendedMemberResponse.message, Is.Not.Null.Or.Empty, "PolicyBenefitExtendedMemberResponse: <message> should not be null or empty");

                // error is required only when the response indicates failure.
                if (PolicyBenefitExtendedMemberResponse.succeeded)
                {
                    Assert.That(PolicyBenefitExtendedMemberResponse.error, Is.Null.Or.Empty, "PolicyBenefitExtendedMemberResponse: <error> should be null or empty when succeeded is true");
                }
                else
                {
                    Assert.That(PolicyBenefitExtendedMemberResponse.error, Is.Not.Null.Or.Empty, "PolicyBenefitExtendedMemberResponse: <error> should not be null or empty when succeeded is false");
                }

                Assert.That(PolicyBenefitExtendedMemberResponse.data, Is.Not.Null, "PolicyBenefitExtendedMemberResponse: <data> should not be null");
                Assert.That(PolicyBenefitExtendedMemberResponse.data.Count, Is.GreaterThanOrEqualTo(1), "PolicyBenefitExtendedMemberResponse: <data> should not be empty");
            });
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
                    // Accept array (most common), object (single item), or null
                    AllowedKinds =  new[] { 
                        JsonValueKind.Array, JsonValueKind.Object, JsonValueKind.Null }
                }
            };

            var content = restResponse?.Content;
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("Response content is null or empty", nameof(restResponse));

            using var jsonDoc = JsonDocument.Parse(content);
            JsonValidationRule.ValidateJson(jsonDoc.RootElement, rules);
            TestContext.Out.WriteLine("Validated: Response Property Names are valid and Data Types are valid.");
        }

            public policyBenefitExtendedMemberResponse populatePolicyBenefitExtendedMemberResponse(RestResponse restResponse)
        {
            if (restResponse == null)
                throw new ArgumentNullException(nameof(restResponse));

            var content = restResponse.Content;
            if (string.IsNullOrWhiteSpace(content))
            {
                return new policyBenefitExtendedMemberResponse
                {
                    succeeded = false,
                    message = "Empty response",
                    error = "Empty response",
                    data = new List<policyBenefitExtendedMemberRequest>()
                };
            }

            using JsonDocument JsonDoc = JsonDocument.Parse(content);
            var response = new policyBenefitExtendedMemberResponse
            {
                data = new List<policyBenefitExtendedMemberRequest>()
            };

            foreach (var property in JsonDoc.RootElement.EnumerateObject())
            {
                switch (property.Name)
                {
                    case "succeeded":
                        response.succeeded = UtilitiesHelper.ReadBooleanNullable(property.Value) ?? false;
                        break;
                    case "message":
                        response.message = UtilitiesHelper.ReadStringNullable(property.Value) ?? string.Empty;
                        break;
                    case "error":
                        response.error = UtilitiesHelper.ReadStringNullable(property.Value) ?? string.Empty;
                        break;
                    case "data":
                        // Support either an array of items or a single object representing one item.
                        if (property.Value.ValueKind == JsonValueKind.Array)
                        {
                            foreach (var item in property.Value.EnumerateArray())
                            {
                                var req = new policyBenefitExtendedMemberRequest();
                                foreach (var p in item.EnumerateObject())
                                {
                                    switch (p.Name)
                                    {
                                        case "policyNo":
                                            req.PolicyNo = UtilitiesHelper.ReadInt32Nullable(p.Value) ?? default;
                                            break;
                                        case "effectiveDate":
                                            req.effectiveDate = UtilitiesHelper.ReadDateTimeNullable(p.Value);
                                            break;
                                        default:
                                            TestContext.Out.WriteLine($"Unknown data property: {p.Name}");
                                            break;
                                    }
                                }
                                response.data.Add(req);
                            }
                        }
                        else if (property.Value.ValueKind == JsonValueKind.Object)
                        {
                            // Single item object case - treat as one element in data list
                            var item = property.Value;
                            var req = new policyBenefitExtendedMemberRequest();
                            foreach (var p in item.EnumerateObject())
                            {
                                switch (p.Name)
                                {
                                    case "policyNo":
                                        req.PolicyNo = UtilitiesHelper.ReadInt32Nullable(p.Value) ?? default;
                                        break;
                                    case "effectiveDate":
                                        req.effectiveDate = UtilitiesHelper.ReadDateTimeNullable(p.Value);
                                        break;
                                    default:
                                        TestContext.Out.WriteLine($"Unknown data property: {p.Name}");
                                        break;
                                }
                            }
                            response.data.Add(req);
                        }
                        else if (property.Value.ValueKind == JsonValueKind.Null)
                        {
                            TestContext.Out.WriteLine("Data property is null.");
                        }
                        else
                        {
                            TestContext.Out.WriteLine($"Unexpected data ValueKind: {property.Value.ValueKind}");
                        }
                        break;
                    default:
                        TestContext.Out.WriteLine($"Unknown property: {property.Name}");
                        break;
                }
            }
            return response;
        }

        public override void ValidateResponseFieldParametersIsValid(RestResponse restResponse)
        {
            throw new NotImplementedException();
        }

        public void ValidatePolicyBenefitExtendedMemberResponseDataIsNotNullOrEmpty(SendInternalEmailsResponse sendInternalEmailsResponse)
        {
            throw new NotImplementedException();
        }
    }
}