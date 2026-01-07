using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.BeneficiaryDetails;
using ClientServicing.Main.Models.BeneficiaryDetails;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.BeneficiaryDetails
{
    public class GetInsuredWithBenefitValidationMethods : AbstractValidationMethods, IGetInsuredWithBenefitValidationMethods
    {
        public void ValidateGetInsuredWithBenefitDataIsNotNullOrEmpty(GetInsuredWithBenefitResponse getInsuredWithBenefit)
        {
            Assert.Multiple(() =>
            {
                Assert.That(getInsuredWithBenefit.executionOutcome, Is.Not.Null, "GetInsuredWithBenefit Response: executionOutcome should not be null");
                Assert.That(getInsuredWithBenefit.data, Is.Not.Null, "GetInsuredWithBenefit Response: data should not be null");
            });
            TestContext.WriteLine("Validated: Response is not null or Empty.");
        }

        public override void ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(RestResponse restResponse)
        {
                var rules = new List<JsonValidationRule>
                {
                    new JsonValidationRule
                    {
                        PropertyName = "succeeded",
                        AllowedKinds = new[] {
                            JsonValueKind.True,
                            JsonValueKind.False
                        }
                    },
                    new JsonValidationRule
                    {
                        PropertyName = "message",
                        AllowedKinds = new[] {
                            JsonValueKind.String,
                            JsonValueKind.Null
                        }
                    },
                    new JsonValidationRule
                    {
                        PropertyName = "errors",
                        AllowedKinds = new[] {
                            JsonValueKind.String,
                            JsonValueKind.Null
                        }
                    },
                    new JsonValidationRule
                    {
                        PropertyName = "data",
                        AllowedKinds = new[] { JsonValueKind.Array },
                        ArrayItemRules = new Dictionary<string,JsonValueKind[]>
                        {
                            { "benefitID",      new[] { JsonValueKind.Number} },
                            { "benefitCover",   new[] { JsonValueKind.Number} }
                        }
                    }
                };
            using var jsonDoc = JsonDocument.Parse(restResponse.Content);
            JsonValidationRule.ValidateJson(jsonDoc.RootElement, rules);
            TestContext.Out.WriteLine("Validated: Response Property Names are valid and Data Types are valid.");
        }
    }
}
