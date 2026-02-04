using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.Policy;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.JsonValidation;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Models.Policy;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.Policy
{
    public class GetUnmentPremiumValidationMethods : AbstractValidationMethods, IGetUnmentPremiumValidationMethods
    {
        UtilitiesHelper utilitiesHelper = new();
        public GetUnmentPremiumResponse populateGetUnmentPremiumResponse(RestResponse response)
        {
            using JsonDocument jsonDoc = JsonDocument.Parse(response.Content);
            GetUnmentPremiumResponse getUnmentPremiumResponse = new()
            {
                executionOutcome = new ExecutionOutcome(),
                data = new UnmetPremiumData()
            };
            UnmetPremiumData unmetPremiumData = new()
            {
                TotalUnmetPremiumResult = new List<TotalUnmetPremiumResult>(),
                UnmetPremiumSummary = new List<UnmetPremiumSummary>()
            };
            TotalUnmetPremiumResult totalUnmetPremiumResult = new();
            UnmetPremiumSummary unmetPremiumSummary = new();

            foreach (var property in jsonDoc.RootElement.EnumerateObject())
            {
                switch (property.Name)
                {
                    case "succeeded":   getUnmentPremiumResponse.executionOutcome.succeeded =   (bool)utilitiesHelper.ReadBooleanNullable(property.Value); break;
                    case "message":     getUnmentPremiumResponse.executionOutcome.message =     utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "errors":      getUnmentPremiumResponse.executionOutcome.errors =      utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "data":
                        foreach (var dataProperty in property.Value.EnumerateObject())
                        {
                            switch (dataProperty.Name)
                            {
                                case "totalUnmetPremiumResult":
                                    foreach (var itemProperty in dataProperty.Value.EnumerateArray())
                                    {
                                        totalUnmetPremiumResult = new TotalUnmetPremiumResult
                                        {
                                            numberOfMonths =    (int)utilitiesHelper.ReadInt32Nullable(itemProperty.GetProperty("numberOfMonths")),
                                            totalAmountMissed = (double?)utilitiesHelper.ReadInt32Nullable(itemProperty.GetProperty("totalAmountMissed")),
                                            description =       utilitiesHelper.ReadStringNullable(itemProperty.GetProperty("description"))
                                        };
                                    }
                                    unmetPremiumData.TotalUnmetPremiumResult.Add(totalUnmetPremiumResult);
                                    break;
                                case "unmetPremiumSummary":
                                    foreach (var itemProperty in dataProperty.Value.EnumerateArray())
                                    {
                                        unmetPremiumSummary = new UnmetPremiumSummary
                                        {
                                            policyNo =      (int)utilitiesHelper.ReadInt32Nullable(itemProperty.GetProperty("policyNo")),
                                            legacy_Pol_No = utilitiesHelper.ReadStringNullable(itemProperty.GetProperty("legacy_Pol_No")),
                                            month =         utilitiesHelper.ReadStringNullable(itemProperty.GetProperty("month")),
                                            paymentDate =   (DateTime)utilitiesHelper.ReadDateTimeNullable(itemProperty.GetProperty("paymentDate")),
                                            trackingDays =  (int)utilitiesHelper.ReadInt32Nullable(itemProperty.GetProperty("trackingDays")),
                                            paymentType =   utilitiesHelper.ReadStringNullable(itemProperty.GetProperty("paymentType")),
                                            description =   utilitiesHelper.ReadStringNullable(itemProperty.GetProperty("description")),
                                            premiumAmount = (double)utilitiesHelper.ReadInt32Nullable(itemProperty.GetProperty("premiumAmount")),
                                            amountPaid =    (double)utilitiesHelper.ReadInt32Nullable(itemProperty.GetProperty("amountPaid"))
                                        };
                                    }
                                    unmetPremiumData.UnmetPremiumSummary.Add(unmetPremiumSummary);
                                    break;
                                default: DocumentTemplate.DisplayFieldAndValue("Unknown Data Propertey", dataProperty.Name); break;
                            }
                        }
                        getUnmentPremiumResponse.data = unmetPremiumData;
                        break;
                    default: DocumentTemplate.DisplayFieldAndValue("Unknown Propertey", property.Name); break;
                }
            }
            return getUnmentPremiumResponse;
        }

        public void ValidateGetUnmentPremiumResponseDataIsNotNullOrEmpty_And_IsNotLessThanZero_And_DateTimeIsNotEqualToDefault(GetUnmentPremiumResponse getUnmentPremiumResponse)
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(getUnmentPremiumResponse.executionOutcome,                      Is.Not.Null.Or.Empty,   "Execution Model Properties Must Not Be Null");
                Assert.That(getUnmentPremiumResponse.data,                                  Is.Not.Null.Or.Empty,   "Data Property Should Not Be Null or Empty");
                Assert.That(getUnmentPremiumResponse.data.UnmetPremiumSummary.Count,        Is.GreaterThan(0),      "UnmetPremiumSummary Count Should Be Greater Than Zero");
                Assert.That(getUnmentPremiumResponse.data.TotalUnmetPremiumResult.Count,    Is.GreaterThan(0),      "TotalUnmetPremiumResult Count Should Be Greater Than Zero");
            }
            DocumentTemplate.DisplayBody("Validated: GetUnmentPremiumResponse Data Is Not Null Or Empty And Integer Is Not Less Than Zero And Date Time Is Not Equal To Default");
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
