using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.BeneficiaryDetails;
using ClientServicing.Main.Models.BeneficiaryDetails;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.BeneficiaryDetails
{
    public class GetInsuredWithBenefitValidationMethods : AbstractValidationMethods, IGetInsuredWithBenefitValidationMethods
    {
        UtilitiesHelper utilitiesHelper = new();
        public void ValidateGetInsuredWithBenefitRequestIsNotNullOrEmpty(GetInsuredWithBenefitRequest getInsuredWithBenefitRequest)
        {
            Assert.Multiple(() => {
                Assert.That(getInsuredWithBenefitRequest,                       Is.Not.Null,                    "Validated: GetInsuredWithBenefitRequest Should Not Be Null");
                Assert.That(getInsuredWithBenefitRequest.policyNo,              Is.Not.LessThanOrEqualTo(0),    "Validated: GetInsuredWithBenefitRequest.PolicyNo Should Not Be Null");
                Assert.That(getInsuredWithBenefitRequest.insuredlifeEntityNo,   Is.Not.LessThan(0),             "Validated: GetInsuredWithBenefitRequest.InsuredlifeEntityNo Should Not Be Null");
            });
                DocumentTemplate.DisplayBody("Validated: GetInsuredWithBenefitRequest Is Not Null, Is Not Less Than or Equal To 0");
        }
        public void ValidateGetInsuredWithBenefitDataIsNotNull_And_IsTrueOrFalse_And_TypeOfString_And_IsNotLessThanOrEqualTo0(GetInsuredWithBenefitResponse getInsuredWithBenefit)
        {
            Assert.Multiple(() =>
            {
                Assert.That(getInsuredWithBenefit.executionOutcome,             Is.Not.Null,                    "Validated: GetInsuredWithBenefit.ExecutionOutcome Should Not Be Null");
                Assert.That(getInsuredWithBenefit.executionOutcome.succeeded,   Is.True.Or.False,               "Validated: GetInsuredWithBenefit.ExecutionOutcome.Succeeded Should Is True False");
                Assert.That(getInsuredWithBenefit.executionOutcome.message,     Is.Null.Or.TypeOf<string>(),    "Validated: GetInsuredWithBenefit.ExecutionOutcome.Message Is Null Or Type Of String");
                Assert.That(getInsuredWithBenefit.executionOutcome.errors,      Is.Null.Or.TypeOf<string>(),    "Validated: GetInsuredWithBenefit.ExecutionOutcome.Errors Is Null Or Type Of String");

                Assert.That(getInsuredWithBenefit.data,     Is.Not.Null, "Valided: GetInsuredWithBenefit.Data Should Not Be Null");
                foreach (var data in getInsuredWithBenefit.data) {
                    Assert.That(data.benefitID,             Is.Not.LessThanOrEqualTo(0),    "Validated: GetInsuredWithBenefit.Data.BenefitID Is Not Less Than Or Equal To 0");
                    Assert.That(data.benefitCover,          Is.Not.LessThan(0),             "Validated: GetInsuredWithBenefit.Data.BenefitCover Is Not Less Than 0");
                }
            });
            DocumentTemplate.DisplayBody("Validated: GetInsuredWithBenefit: Is Not Null, Is True Or False, Is Null Or Type Of String , Is Not Less Than Or Equal To 0");
        }
        public GetInsuredWithBenefitResponse PopulateGetInsuredWithBenefitResponseData(RestResponse restResponse)
        {
            using JsonDocument jsonDoc = JsonDocument.Parse(restResponse.Content!);
            GetInsuredWithBenefitResponse getInsuredWithBenefitResponse = new GetInsuredWithBenefitResponse
            {
                executionOutcome = new ExecutionOutcome()
            };
            foreach (var property in jsonDoc.RootElement.EnumerateObject())
            {
                switch (property.Name)
                {
                    case "succeeded":
                        getInsuredWithBenefitResponse.executionOutcome.succeeded = (bool)utilitiesHelper.ReadBooleanNullable(property.Value); break;
                    case "message":
                        getInsuredWithBenefitResponse.executionOutcome.message = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "errors":
                        getInsuredWithBenefitResponse.executionOutcome.errors = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "data":
                        var dataElement = property.Value;
                        var items = new List<BenefitData>();
                        switch (dataElement.ValueKind)
                        {
                            case JsonValueKind.Array:
                                foreach (var dataProperty in dataElement.EnumerateArray())
                                {
                                    var benefitData = new BenefitData();
                                    foreach (var item in dataProperty.EnumerateObject())
                                    {
                                        switch (item.Name)
                                        {
                                            case "benefitID":
                                                benefitData.benefitID = (int)utilitiesHelper.ReadInt32Nullable(item.Value); break;
                                            case "benefitCover":
                                                benefitData.benefitCover = (double)utilitiesHelper.ReadInt32Nullable(item.Value); break;
                                        }
                                    }
                                    items.Add(benefitData);
                                }
                                break;
                        }
                        getInsuredWithBenefitResponse.data = items;
                        break;
                    default:
                        TestContext.Out.WriteLine($"Unknown property: {property.Name}");
                        break;
                }
            }
            return getInsuredWithBenefitResponse;
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
