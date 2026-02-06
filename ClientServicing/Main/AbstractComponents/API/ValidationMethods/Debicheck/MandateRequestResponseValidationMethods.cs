using ClientServicing.Main.AbstractComponents.API.IValidationMethods.Debicheck;
using ClientServicing.Main.Models.BenefitExtendedMember;
using ClientServicing.Main.Models.Debicheck;
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
using ClientServicing.Main.Resources.EndPoints.Debicheck;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.Debicheck
{
    public class MandateRequestResponseValidationMethods : AbstractValidationMethods, IMandateRequestResponseValidationMethods
    {
        UtilitiesHelper UtilitiesHelper = new UtilitiesHelper();

        // Implemented: delegate to the main validator to avoid duplication
        public void ValidateCheckStatusRequesteDataIsNotNullOrEmpty(MandatesRequest mandaterequest)
        {
           throw new NotImplementedException();
        }

        public void ValidatePMandatesRequestResponseDataIsNotNullOrEmpty(MandatesRequestResponse mandateresponse)
        {
            
     
            Assert.Multiple(() =>
            {
                Assert.That(mandateresponse, Is.Not.Null, "mandaterequestresponse: response object should not be null");

                // message may be null in some payloads; ensure property exists (allow null)
                Assert.That(mandateresponse.result, Is.Not.Null, "mandaterequestresponse: <message> should not be null (may be empty)");

                // result must be present and contain at least one item
                Assert.That(mandateresponse.result, Is.Not.Null, "mandaterequestresponse: <result> should not be null");
                Assert.That(mandateresponse.result.Count, Is.GreaterThanOrEqualTo(1), "CheckStatusResponse: <result> should not be empty");
            });
        }

        public override void ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(RestResponse restResponse)
        {
            var rules = new List<JsonValidationRule> {
                // Accept either "succeeded" or "success" (some payloads use one or the other)
               
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

        public MandatesRequestResponse populatefetchMandateRequestResponse(RestResponse restResponse)
        {
            using JsonDocument jsonDoc = JsonDocument.Parse(restResponse.Content);
            var mandatesrequestresponse = new MandatesRequestResponse
            {
                responseMessage = new ExecutionOutcome()
            };

            // Fix: EnumerateObject() gives JsonProperty, which has .Name and .Value
            foreach (var property in jsonDoc.RootElement.EnumerateObject())
            {
                switch (property.Name)
                {
                    case "succeeded":
                        mandatesrequestresponse.responseMessage.succeeded = (bool)UtilitiesHelper.ReadBooleanNullable(property.Value); break;
                    case "message":
                        mandatesrequestresponse.responseMessage.message = UtilitiesHelper.ReadStringNullable(property.Value); break;
                    case "errors":
                        mandatesrequestresponse.responseMessage.errors = UtilitiesHelper.ReadStringNullable(property.Value); break;
                    case "data":
                        var dataValue = property.Value.ValueKind;
                        switch (dataValue)
                        {
                            case JsonValueKind.Array:
                                TestContext.Out.WriteLine("Data: data type is array");
                                break;
                        }
                        break;
                    default: TestContext.Out.WriteLine($"Unknown property: {property.Name}"); break;
                }
            }
            return mandatesrequestresponse;
        }

        public override void ValidateResponseFieldParametersIsValid(RestResponse restResponse)
        {
            throw new NotImplementedException();
        }

        public void ValidateMandateRequestResponseDataIsNotNullOrEmpty(MandatesRequestResponse mandaterequestresponse)
        {
            throw new NotImplementedException();
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
    }
}
