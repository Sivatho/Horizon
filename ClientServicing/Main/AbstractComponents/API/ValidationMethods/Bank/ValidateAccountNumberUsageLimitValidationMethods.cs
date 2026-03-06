using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.Bank;
using ClientServicing.Main.Models.Bank;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.Bank
{
    public class ValidateAccountNumberUsageLimitValidationMethods : AbstractValidationMethods, IValidateAccountNumberUsageLimitValidationMethods
    {
        UtilitiesHelper utilitiesHelper = new();
        public void ValidateValidateAccountNumberUsageLimitResponseDataIsNotNullOrEmpty(ValidateAccountNumberUsageLimitResponse validateAccountNumberUsageLimitResponse)
        {
            Assert.Multiple(() =>
            {
                Assert.That(validateAccountNumberUsageLimitResponse.success,        Is.True.Or.False.And.Not.Null,                  "ValidateAccountNumberUsageLimit Response: success can be true or false but not null.");
                Assert.That(validateAccountNumberUsageLimitResponse.message,        Is.Not.Null,                                    "ValidateAccountNumberUsageLimit Response: message should not be null");
                Assert.That(validateAccountNumberUsageLimitResponse.totalPolicies,  Is.Not.LessThan(0).And.Not.Null.Or.Not.Empty,   "ValidateAccountNumberUsageLimit Response: totalPolicies should not be less than 0.");
                Assert.That(validateAccountNumberUsageLimitResponse.limitExceeded,  Is.True.Or.False.And.Not.Null,                  "ValidateAccountNumberUsageLimit Response: limitExceeded can be true or false but not null."
                );
            });
            TestContext.Out.WriteLine("Response: ValidateAccountNumberUsageLimitResponse is not null or empty.");
        }
        public ValidateAccountNumberUsageLimitResponse PopulateValidateAccountNumberUsageLimitResponse(RestResponse response)
        {
            using JsonDocument document = JsonDocument.Parse(response.Content!);
            ValidateAccountNumberUsageLimitResponse validateAccountNumberUsageLimitResponse = new();
            foreach (var property in document.RootElement.EnumerateObject())
            {
                switch (property.Name.ToLower())
                {
                    case "succeeded":       validateAccountNumberUsageLimitResponse.success =       (bool)utilitiesHelper.ReadBooleanNullable(property.Value)!; break;
                    case "message":         validateAccountNumberUsageLimitResponse.message =       utilitiesHelper.ReadStringNullable(property.Value)!; break;
                    case "totalPolicies":   validateAccountNumberUsageLimitResponse.totalPolicies = (int)utilitiesHelper.ReadInt32Nullable(property.Value)!; break;
                    case "limitExceeded":   validateAccountNumberUsageLimitResponse.limitExceeded = (bool)utilitiesHelper.ReadBooleanNullable(property.Value)!; break;
                }
            }
            return validateAccountNumberUsageLimitResponse;
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
