using ClientServicing.Main.AbstractComponents.API.IValidationMethods.Debicheck;
using ClientServicing.Main.Models.BenefitExtendedMember;
using ClientServicing.Main.Models.Debicheck;
using ClientServicing.Main.Models.Email;
using ClientServicing.Main.Resources.Helper;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.Debicheck
{
    public class CheckStatusResponseValidationMethods :AbstractValidationMethods, ICheckStatusResponseValidationMethods
    {
        UtilitiesHelper UtilitiesHelper = new UtilitiesHelper();

        // Implemented: delegate to the main validator to avoid duplication
        public void ValidateCheckStatusResponseDataIsNotNullOrEmpty(CheckStatusResponse checkStatusResponse)
        {
            ValidateCheckStatusResponsePayload(checkStatusResponse);
        }

        public void ValidatePolicyBenefitExtendedMemberResponseDataIsNotNullOrEmpty(CheckStatusRequest checkstatusrequest)
        {
            throw new NotImplementedException();
        }

        // Main validator for CheckStatusResponse payloads
        public void ValidatePolicyBenefitExtendedMemberResponseDataIsNotNullOrEmpty(CheckStatusResponse checkstatusresponse)
        {
            ValidateCheckStatusResponsePayload(checkstatusresponse);
        }

        private void ValidateCheckStatusResponsePayload(CheckStatusResponse checkstatusresponse)
        {
            Assert.Multiple(() =>
            {
                Assert.That(checkstatusresponse, Is.Not.Null, "CheckStatusResponse: response object should not be null");

                // message may be null in some payloads; ensure property exists (allow null)
                Assert.That(checkstatusresponse.message, Is.Not.Null, "CheckStatusResponse: <message> should not be null (may be empty)");

                // result must be present and contain at least one item
                Assert.That(checkstatusresponse.result, Is.Not.Null, "CheckStatusResponse: <result> should not be null");
                Assert.That(checkstatusresponse.result.Count, Is.GreaterThanOrEqualTo(1), "CheckStatusResponse: <result> should not be empty");
            });
        }

        public override void ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(RestResponse restResponse)
        {
            var rules = new List<JsonValidationRule> {
                // Accept either "succeeded" or "success" (some payloads use one or the other)
                new JsonValidationRule {
                    PropertyName = "succeeded",
                    AllowedKinds = new[] {
                        JsonValueKind.True, JsonValueKind.False
                    }
                },
                new JsonValidationRule {
                    PropertyName = "success",
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
                // Accept both "error" and "errors" variants
                new JsonValidationRule {
                    PropertyName = "error",
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
                    PropertyName = "result",
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

        public CheckStatusResponse populatefetchCheckStatusResponse(RestResponse restResponse)
        {
            if (restResponse == null)
                throw new ArgumentNullException(nameof(restResponse));

            var content = restResponse.Content;
            if (string.IsNullOrWhiteSpace(content))
            {
                return new CheckStatusResponse
                {
                    succeeded = false,
                    message = "Empty response",
                    result = new List<CheckStatusRequest>()
                };
            }

            using JsonDocument JsonDoc = JsonDocument.Parse(content);
            var response = new CheckStatusResponse
            {
                result = new List<CheckStatusRequest>()
            };

            foreach (var property in JsonDoc.RootElement.EnumerateObject())
            {
                switch (property.Name)
                {
                    case "succeeded":
                    case "success":
                        response.succeeded = UtilitiesHelper.ReadBooleanNullable(property.Value) ?? false;
                        break;
                    case "message":
                        response.message = UtilitiesHelper.ReadStringNullable(property.Value) ?? string.Empty;
                        break;
                    case "result":
                    case "data":
                        // Support either an array of items or a single object representing one item.
                        if (property.Value.ValueKind == JsonValueKind.Array)
                        {
                            foreach (var item in property.Value.EnumerateArray())
                            {
                                if (item.ValueKind == JsonValueKind.Object)
                                {
                                    var req = MapJsonElementToCheckStatusRequest(item);
                                    response.result.Add(req);
                                }
                            }
                        }
                        else if (property.Value.ValueKind == JsonValueKind.Object)
                        {
                            var obj = property.Value;

                            // The object may contain an inner "data" array (per sample)
                            if (obj.TryGetProperty("data", out var innerData) && innerData.ValueKind == JsonValueKind.Array)
                            {
                                foreach (var innerItem in innerData.EnumerateArray())
                                {
                                    if (innerItem.ValueKind == JsonValueKind.Object)
                                    {
                                        var req = MapJsonElementToCheckStatusRequest(innerItem);
                                        response.result.Add(req);
                                    }
                                }
                            }
                            else
                            {
                                // Treat the object itself as a single data item
                                var req = MapJsonElementToCheckStatusRequest(obj);
                                response.result.Add(req);
                            }
                        }
                        else if (property.Value.ValueKind == JsonValueKind.Null)
                        {
                            TestContext.Out.WriteLine("Result property is null.");
                        }
                        else
                        {
                            TestContext.Out.WriteLine($"Unexpected result ValueKind: {property.Value.ValueKind}");
                        }
                        break;
                    default:
                        TestContext.Out.WriteLine($"Unknown property: {property.Name}");
                        break;
                }
            }
            return response;
        }

        // Map a JSON object representing a data item into CheckStatusRequest
        private CheckStatusRequest MapJsonElementToCheckStatusRequest(JsonElement item)
        {
            var req = new CheckStatusRequest();

            foreach (var p in item.EnumerateObject())
            {
                switch (p.Name)
                {
                    case "policyNumber":
                 
                        var polStr = UtilitiesHelper.ReadStringNullable(p.Value);
                        if (!string.IsNullOrEmpty(polStr))
                            req.policyNumber = polStr;
                        else if (UtilitiesHelper.ReadInt32Nullable(p.Value) is int pi)
                            req.policyNumber = pi.ToString();
                        break;

                    case "identityNumber":
               
                        req.identityNumber = UtilitiesHelper.ReadStringNullable(p.Value) ?? string.Empty;
                        break;

                    case "cellPhoneNumber":
                        req.cellPhoneNumber = UtilitiesHelper.ReadStringNullable(p.Value) ?? string.Empty;
                        break;

                    case "accountNumber":
                        req.accountNumber = UtilitiesHelper.ReadStringNullable(p.Value) ?? string.Empty;
                        break;

                    case "branchCode":
                        req.branchCode = UtilitiesHelper.ReadStringNullable(p.Value) ?? string.Empty;
                        break;

                    case "accountType":
                        req.accountType = UtilitiesHelper.ReadStringNullable(p.Value) ?? string.Empty;
                        break;

                    case "bankName":
                        req.bankName = UtilitiesHelper.ReadStringNullable(p.Value) ?? string.Empty;
                        break;

                    case "surnameOrCompanyName":
                        req.surnameOrCompanyName = UtilitiesHelper.ReadStringNullable(p.Value) ?? string.Empty;
                        break;

                    case "initials":
                        req.initials = UtilitiesHelper.ReadStringNullable(p.Value) ?? string.Empty;
                        break;

                    case "amount":
                        req.amount = UtilitiesHelper.ReadInt32Nullable(p.Value) ?? default;
                        break;

                    case "bypassD3Check":
                        req.bypassD3Check = UtilitiesHelper.ReadBooleanNullable(p.Value) ?? false;
                        break;

                    case "sourceSystemId":
                        req.sourceSystemId = UtilitiesHelper.ReadInt32Nullable(p.Value) ?? default;
                        break;

                    default:
                        TestContext.Out.WriteLine($"Unknown data property (ignored): {p.Name}");
                        break;
                }
            }

            return req;
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
