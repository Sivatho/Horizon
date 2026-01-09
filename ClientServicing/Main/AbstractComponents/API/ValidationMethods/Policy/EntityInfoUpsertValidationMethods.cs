using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.Policy;
using ClientServicing.Main.Models.BeneficiaryDetails;
using ClientServicing.Main.Models.Policy;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.Policy
{
    public class EntityInfoUpsertValidationMethods : AbstractValidationMethods, IEntityInfoUpsertValidationMethods
    {
        public void ValidateEntityInfoUpsertResponseDataIsNotNullOrEmpty(PolicyEntityInfoUpsertResponse entityInfoUpsertResponse)
        {
            Assert.Multiple(() => {
                Assert.That(entityInfoUpsertResponse.executionOutcome, Is.Not.Null.Or.Empty, "EntityInfoUpsertResponse: <executionOutcome> Should not be null or empty");
                Assert.That(entityInfoUpsertResponse.data, Is.TypeOf<bool>(), "EntityInfoUpsertResponse: <data> Should be a boolean value");
            });
            TestContext.Out.WriteLine("Validated: <EntityInfoUpsertResponse> is not be null or empty");
        }
        public void ValidateEntityInfoUpsertRequestDataIsNotNullOrEmpty(EntityInfoUpsertRequest entityInfoUpsertRequest)
        {
            Assert.Multiple(() => {
                Assert.That(entityInfoUpsertRequest.policyNo,       Is.Not.LessThan(0).Or.Empty,    "EntityInfoUpsertRequest: <policyNo> Should not be less than zero or empty");
                Assert.That(entityInfoUpsertRequest.entityNo,       Is.Not.LessThan(0).Or.Empty,    "EntityInfoUpsertRequest: <entityNo> Should not be less than zero or empty");
                Assert.That(entityInfoUpsertRequest.titleCD,        Is.Not.LessThan(0).Or.Empty,    "EntityInfoUpsertRequest: <titleCD> Should not be less than zero or empty");
                Assert.That(entityInfoUpsertRequest.legalRefNoType, Is.Not.LessThan(0).Or.Empty,    "EntityInfoUpsertRequest: <legalRefNoType> Should not be less than zero or empty");
                Assert.That(entityInfoUpsertRequest.genderCD,       Is.Not.LessThan(0).Or.Empty,    "EntityInfoUpsertRequest: <genderCD> Should not be less than zero or empty");
                Assert.That(entityInfoUpsertRequest.legalRefNumber, Is.Not.Null.Or.Empty,           "EntityInfoUpsertRequest: <genderCD> Should not be less than zero or empty");
                Assert.That(entityInfoUpsertRequest.dob,            Is.Not.EqualTo(default(DateTime)),"EntityInfoUpsertRequest: <dob> should not be default (DateTime.MinValue)");
                Assert.That(entityInfoUpsertRequest.dob,            Is.Not.GreaterThan(DateTime.Now.AddYears(-21)).Or.Empty,    "EntityInfoUpsertRequest: <dob> must be on or before (>= 21 years ago)");
                Assert.That(entityInfoUpsertRequest.effectiveDate,  Is.Not.GreaterThan(DateTime.Now.AddMonths(1)).Or.Empty,     "EntityInfoUpsertRequest: <effectiveDate> should not be later than one month from now or empty");
            });
            TestContext.Out.WriteLine("Validated: <EntityInfoUpsertRequest> is not null or empty; integers are not less than 0 or empty; datetime are not default or empty");
        }
        public override void ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(RestResponse restResponse)
        {
            var rules = new List<JsonValidationRule> {
                new JsonValidationRule {
                    PropertyName = "succeeded",
                    AllowedKinds = new[] { JsonValueKind.False, JsonValueKind.True }
                },
                new JsonValidationRule {
                    PropertyName = "message",
                    AllowedKinds = new[] { JsonValueKind.String, JsonValueKind.Null }
                },
                new JsonValidationRule {
                    PropertyName = "errors",
                    AllowedKinds = new[] { JsonValueKind.String, JsonValueKind.Null }
                },
                new JsonValidationRule {
                    PropertyName = "data",
                    AllowedKinds = new[] { JsonValueKind.False, JsonValueKind.True }
                }
            };
            using var doc = JsonDocument.Parse(restResponse.Content);
            JsonValidationRule.ValidateJson(doc.RootElement, rules);
            TestContext.Out.WriteLine("Validated: Response Contects and data types are valid");
        }

        public override void ValidateResponseFieldParametersIsValid(RestResponse restResponse)
        {
            throw new NotImplementedException();
        }
    }
}
