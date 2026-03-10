using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.BeneficiaryDetails;
using ClientServicing.Main.Models.BeneficiaryDetails;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.BeneficiaryDetails
{
    public class UpdatePolicyBeneficiaryCacheValidationMethods : AbstractValidationMethods, IUpdatePolicyBeneficiaryCacheValidationMethods
    {
        UtilitiesHelper utilitiesHelper = new();
        public void ValidateUpdatePolicyBenefitciaryCacheResponseDataIsNotNullOrEmpty(UpdatePolicyBenefitciaryResponse updatePolicyBenefitciaryCacheResponse)
        {
            Assert.Multiple(() =>{
                Assert.That(updatePolicyBenefitciaryCacheResponse,              Is.Not.Null.Or.Empty,           "Validated: UpdatePolicyBeneficiaryCacheResponse: Response should not be null or empty");
                Assert.That(updatePolicyBenefitciaryCacheResponse.succeeded,    Is.True.Or.False,               "Validated: UpdatePolicyBeneficiaryCacheResponse.Succeeded property should not be null");
                Assert.That(updatePolicyBenefitciaryCacheResponse.data,         Is.True.Or.False,               "Validated: UpdatePolicyBeneficiaryCacheResponse.Data property should not be null");
                Assert.That(updatePolicyBenefitciaryCacheResponse.message,      Is.Null.Or.TypeOf<string>(),    "Validated: UpdatePolicyBeneficiaryCacheResponse.Message property should not be null or empty");
                Assert.That(updatePolicyBenefitciaryCacheResponse.errors,       Is.Null.Or.TypeOf<string>(),    "Validated: UpdatePolicyBeneficiaryCacheResponse.Errors property should not be null or empty");
            });
            DocumentTemplate.DisplayBody("Validated: UpdatePolicyBeneficiaryCacheResponse: Is Not Null, Is True Or False, Is Null Or Type Of String , Is Not Less Than Or Equal To 0");
        }
        public UpdatePolicyBenefitciaryResponse PopulateUpdatePolicyBenefitciaryResponse(RestResponse restResponse)
        {
            var updatePolicyBenefitCacheResponse = new UpdatePolicyBenefitciaryResponse();
            using JsonDocument doc = JsonDocument.Parse(restResponse.Content!);

            foreach (var property in doc.RootElement.EnumerateObject())
            {
                switch (property.Name)
                {
                    case "succeeded":
                        updatePolicyBenefitCacheResponse.succeeded = (bool)utilitiesHelper.ReadBooleanNullable(property.Value); break;
                    case "message":
                        updatePolicyBenefitCacheResponse.message = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "error":
                        updatePolicyBenefitCacheResponse.errors = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "data":
                        updatePolicyBenefitCacheResponse.data = (bool)utilitiesHelper.ReadBooleanNullable(property.Value); break;
                    default:
                        TestContext.Out.WriteLine($"Unknown property: {property.Name}");
                        break;
                }
            }
            return updatePolicyBenefitCacheResponse;
        }
        public override void ValidateResponseFieldParametersIsValid(RestResponse restResponse)
        {
            throw new NotImplementedException();
        }
        public override void ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(RestResponse restResponse)
        {
            throw new NotImplementedException();
        }
    }
}
