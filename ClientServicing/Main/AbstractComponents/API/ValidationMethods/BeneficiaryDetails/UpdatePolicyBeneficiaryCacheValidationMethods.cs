using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.BeneficiaryDetails;
using ClientServicing.Main.Models.BeneficiaryDetails;
using ClientServicing.Main.Models.General;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.BeneficiaryDetails
{
    public class UpdatePolicyBeneficiaryCacheValidationMethods : AbstractValidationMethods, IUpdatePolicyBeneficiaryCacheValidationMethods
    {
        public void ValidateResponseIsNotNullOrEmpty(UpdatePolicyBenefitciaryCacheResponse updatePolicyBenefitciaryCacheResponse)
        {
           Assert.Multiple(() =>
           {
               Assert.That(updatePolicyBenefitciaryCacheResponse,Is.Not.Null, "UpdatePolicyBeneficiaryCacheResponse is null");
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
                    AllowedKinds = new[] {
                        JsonValueKind.True, JsonValueKind.False, JsonValueKind.Null
                    }
                }
            };
            using var jsonDoc = JsonDocument.Parse(restResponse.Content);
            JsonValidationRule.ValidateJson(jsonDoc.RootElement, rules);
            TestContext.Out.WriteLine("Validated: Response Property Names are valid and Data Types are valid.");
        }

        public void ValidateUpdatePolicyBenefitciaryCacheResponseDataIsNotNullOrEmpty(UpdatePolicyBenefitciaryCacheResponse updatePolicyBenefitciaryCacheResponse)
        {
            throw new NotImplementedException();
        }
    }
}
