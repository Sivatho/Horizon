using ClientServicing.Main.AbstractComponents.API.IValidationMethods.AdjustmenttoBillings;

using ClientServicing.Main.Models.AddAdjustementToBillings;

using ClientServicing.Main.Resources.Helper;
using RestSharp;


namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.AdjustmentToBillings
{
    public class AddAdjustmentToBillingsValidationMethod : AbstractValidationMethods, IAddAdjustmentToBillingsValidationMethods
    {
        UtilitiesHelper utilitiesHelper = new UtilitiesHelper();

        public void ValidateBillingsAdjustmentInformationRequestPayload(AddAdjustementToBillingsRequest addAdjustementToBillingsRequest)
        {
            Assert.Multiple(() =>
            {
                Assert.That(addAdjustementToBillingsRequest, Is.Not.Null, "AddAdjustementToBillings Request Payload should not be null");
                Assert.That(addAdjustementToBillingsRequest.billingsAdjustmentInformation, Is.Not.Null, "AddAdjustementToBillings Request Payload should not be null");
                Assert.That(addAdjustementToBillingsRequest.billingsAdjustmentInformation.policyNo, Is.Not.LessThan(0), "Request: PolicyNo Should Not Be Less Than 0");
                Assert.That(addAdjustementToBillingsRequest.billingsAdjustmentInformation.effectiveDate, Is.Not.EqualTo(default(DateTime)), "Request: EffectiveDate Should Not Be Empty");
                Assert.That(addAdjustementToBillingsRequest.billingsAdjustmentInformation.adjustmentDateFrom, Is.Not.EqualTo(default(DateTime)), "Request: AdjustmentDateFrom Should Not Be Empty");
                Assert.That(addAdjustementToBillingsRequest.billingsAdjustmentInformation.adjustmentAmount, Is.Not.NaN, "Request: AdjustmentAmount Should Not Be NaN");
                Assert.That(addAdjustementToBillingsRequest.billingsAdjustmentInformation.totalAdjAmount, Is.Not.NaN, "Request: TotalAdjAmount Should Not Be NaN");
                Assert.That(addAdjustementToBillingsRequest.billingsAdjustmentInformation.adjustedMonthCnt, Is.Not.LessThan(0), "Request: AdjustedMonthCnt Should Not Be Less Than 0");
                Assert.That(addAdjustementToBillingsRequest.billingsAdjustmentInformation.adjustmentEndDate, Is.Not.EqualTo(default(DateTime)), "Request: AdjustmentEndDate Should Not Be Empty");
                Assert.That(addAdjustementToBillingsRequest.billingsAdjustmentInformation.comment, Is.Not.Null.Or.Empty, "Request: Comment Should Not Be Null or Empty");
                Assert.That(addAdjustementToBillingsRequest.billingsAdjustmentInformation.actionID, Is.Not.Null.Or.Empty, "Request: ActionID Should Not Be Null or Empty");
                
                Assert.That(addAdjustementToBillingsRequest.billingAdjustmentPeriods, Is.Not.Null, "AddAdjustementToBillings Request Payload should not be null");
               foreach(var billingAdjustmentPeriod in addAdjustementToBillingsRequest.billingAdjustmentPeriods)
                {
                    Assert.That(billingAdjustmentPeriod.policyNo, Is.Not.LessThan(0), "Request: BillingAdjustmentPeriods.PolicyNo Should Not Be Less Than 0");
                    Assert.That(billingAdjustmentPeriod.legacyPolNo, Is.Null.Or.TypeOf<string>(), "Request: BillingAdjustmentPeriods.LegacyPolNo Should Be Null Or Type Of String");
                    Assert.That(billingAdjustmentPeriod.referenceNO, Is.Not.Null.Or.Empty, "Request: BillingAdjustmentPeriods.ReferenceNO Should Not Be Null or Empty");
                    Assert.That(billingAdjustmentPeriod.billingPeriod, Is.Not.LessThan(0), "Request: BillingAdjustmentPeriods.BillingPeriod Should Not Be Less Than 0");
                    Assert.That(billingAdjustmentPeriod.raisedDate, Is.Not.EqualTo(default(DateTime)), "Request: BillingAdjustmentPeriods.RaisedDate Should Not Be Empty");
                    Assert.That(billingAdjustmentPeriod.mandateType, Is.Not.Null.Or.Empty, "Request: BillingAdjustmentPeriods.MandateType Should Not Be Null or Empty");
                    Assert.That(billingAdjustmentPeriod.paymentType, Is.Not.Null.Or.Empty, "Request: BillingAdjustmentPeriods.PaymentType Should Not Be Null or Empty");
                    Assert.That(billingAdjustmentPeriod.premiumAmount, Is.Not.NaN, "Request: BillingAdjustmentPeriods.PremiumAmount Should Not Be NaN");
                    Assert.That(billingAdjustmentPeriod.amountPaid, Is.Not.NaN, "Request: BillingAdjustmentPeriods.AmountPaid Should Not Be NaN");
                    Assert.That(billingAdjustmentPeriod.effectiveDate, Is.Not.EqualTo(default(DateTime)), "Request: BillingAdjustmentPeriods.EffectiveDate Should Not Be Empty");
                }
            });
            DocumentTemplate.DisplayBody("Validated: AddAdjustementToBillings Request Payload Data is in Correct Format and Not Null or Empty.");
        }
        public void ValidateAddAdjustmenttpBillingsisNotBeNullAndTypeOfBoolean (AddAdjustementToBillingsResponse response)
        {
            Assert.Multiple(() =>
            {
                Assert.That(response, Is.Not.Null, "AddAdjustementToBillings Response Should Not Be Null");
                Assert.That(response.Result, Is.TypeOf<bool>(), "AddAdjustementToBillings Response Result Should Be of Type Boolean");
            });
            DocumentTemplate.DisplayBody("Validated: AddAdjustementToBillings Response Data is Not Null or Empty.");
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
