using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.RemovalFromBillings;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Models.RemovalFromBillings;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.RemovalFromBillings
{
    public class RemovalFromBillingsValidationMethods : AbstractValidationMethods, IRemovalFromBillingsValidationMethods
    {
        UtilitiesHelper utilitiesHelper = new();

        public void ValidateRemovalFromBillingsRequestIsNotNullOrEmpty_And_IntegerIsNotLessThanZero_And_DateTimeIsNotEqualToDefault(RemovalFromBillingsRequest removalFromBillingsRequest)
        {
            using (Assert.EnterMultipleScope()) {
                Assert.That(removalFromBillingsRequest, Is.Not.Null.Or.Empty, "RemovalFromBillingsRequest Should Not Be Null or Empty");
                Assert.That(removalFromBillingsRequest.policyNo,        Is.Not.LessThan(0),                 "PolicyNo Should Not Be Less Than Zero");
                Assert.That(removalFromBillingsRequest.effectiveDate,   Is.Not.EqualTo(default(DateTime)),  "EffectiveDate Should Not Equal To Default Date");
                Assert.That(removalFromBillingsRequest.endDate,         Is.Not.EqualTo(default(DateTime)),  "EndDate Should Not Equal To Default Date");
                Assert.That(removalFromBillingsRequest.comment,         Is.Not.Null.Or.Empty,               "Comment Should Not Be Null Or Empty");
            }
            DocumentTemplate.DisplayBody("RemovalFromBillingsRequest Is Not Null Or Empty And Integer Is Not Less Than Zero And DateTime Is Not Equal To Default");
        }

        public void ValidateRemovalFromBillingsHistoryRespondeIsNotNullOrEmpty_And_IntegerIsNotLessThanZero_And_DateTimeIsNotEqualToDefault(RemovalFromBillingsHistoryResponse removalFromBillingsHistoryResponse)
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(removalFromBillingsHistoryResponse.executionOutcome, Is.Not.Null.Or.Empty, "");
                Assert.That(removalFromBillingsHistoryResponse.data, Is.Not.Null.Or.Empty, "");
                Assert.That(removalFromBillingsHistoryResponse.data.Length, Is.Not.LessThan(0), "");
                for (int i = 0; i < removalFromBillingsHistoryResponse.data.Length; i++) {
                    var item = removalFromBillingsHistoryResponse.data[i];
                    Assert.That(item,               Is.Not.Null,                        $"Data {i} Should Not Be Null");
                    Assert.That(item.removeID,      Is.Not.LessThan(0),                 "RemoveID Should Not Be Less Than Zero");
                    Assert.That(item.policyNo,      Is.Not.LessThan(0),                 "PolicyNo Should Not Be Less Than Zero");
                    Assert.That(item.removeCD,      Is.Not.LessThan(0),                 "RemoveCDShould Not Be Less Than Zero");
                    Assert.That(item.removalDate,   Is.Not.EqualTo(default(DateTime)),  "RemovalDate Should Not Equal To Default Date");
                    Assert.That(item.premiumAmt,    Is.Not.LessThan(0),                 "PremiumAmt Should Not Be Less Than Zero");
                    Assert.That(item.effDate,       Is.Not.EqualTo(default(DateTime)),  "EffDate Should Not Equal To Default Date");
                    Assert.That(item.statusCD,      Is.Not.LessThan(0),                 "StatusCD Should Not Be Less Than Zero");
                    Assert.That(item.s_Desc,        Is.Not.Null.Or.Empty,               "S_Desc Should Not Be Null Or Empty");
                    Assert.That(item.comments,      Is.Not.Null.Or.Empty,               "Comments Should Not Be Null Or Empty");
                    //Assert.That(item.audModUser,    Is.Not.Null.Or.Empty,               "AudModUser Should Not Be Null Or Empty");
                }                
            }
            DocumentTemplate.DisplayBody("Validated: RemovalFromBillingsHistoryResponse: Is Not Null Or Empty And Integer Is Not Less Than Zero And DateTime Is Not Equal To Default");
        }

        public void ValidateRemovalFromBillingsResponseIsNotNullOrEmpty(ExecutionOutcomeAndDataBooleanResponse removalFromBillingsResponse)
        {
            using (Assert.EnterMultipleScope()) {
                Assert.That(removalFromBillingsResponse.executionOutcome,   Is.Not.Null.Or.Empty, "ExecutionOutcome Should Not Be Null Or Empty");
                Assert.That(removalFromBillingsResponse.data,               Is.Not.Null.Or.Empty, "Data Should Not Be Null Or Empty");
            }
            DocumentTemplate.DisplayBody("Validated: RemovalFromBillingsResponse: Is Not Null Or Empty"); 
        }

        public RemovalFromBillingsHistoryResponse populateRemovalFromBillingsHistoryResponse(RestResponse response) {
            using JsonDocument jsDoc = JsonDocument.Parse(response.Content);
            var removalFromBillingsHistoryResponse = new RemovalFromBillingsHistoryResponse
            {
                executionOutcome = new ExecutionOutcome()
            };
            foreach (var property in jsDoc.RootElement.EnumerateObject())
            {
                switch (property.Name)
                {
                    case "succeeded": removalFromBillingsHistoryResponse.executionOutcome.succeeded = (bool)utilitiesHelper.ReadBooleanNullable(property.Value); break;
                    case "message": removalFromBillingsHistoryResponse.executionOutcome.message = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "errors": removalFromBillingsHistoryResponse.executionOutcome.errors = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "data":
                        var dataElement = property.Value;
                        List<RemovalFromBillingHistory> items = new List<RemovalFromBillingHistory>();
                        foreach (var dataProperty in dataElement.EnumerateArray()) {
                            var removalFromBillingHistory = new RemovalFromBillingHistory();
                            foreach (var item in dataProperty.EnumerateObject()) {
                                switch (item.Name) {
                                    case "removeID":    removalFromBillingHistory.removeID =    (int)utilitiesHelper.ReadInt32Nullable(item.Value); break;
                                    case "policyNo":    removalFromBillingHistory.policyNo =    (int)utilitiesHelper.ReadInt32Nullable(item.Value); break;
                                    case "removeCD":    removalFromBillingHistory.removeCD =    (int)utilitiesHelper.ReadInt32Nullable(item.Value); break;
                                    case "removalDate": removalFromBillingHistory.removalDate = (DateTime)utilitiesHelper.ReadDateTimeNullable(item.Value); break;
                                    case "premiumAmt":  removalFromBillingHistory.premiumAmt =  (int)utilitiesHelper.ReadInt32Nullable(item.Value); break;
                                    case "effDate":     removalFromBillingHistory.effDate =     (DateTime)utilitiesHelper.ReadDateTimeNullable(item.Value); break;
                                    case "endDate":     removalFromBillingHistory.endDate =     (DateTime)utilitiesHelper.ReadDateTimeNullable(item.Value); break;
                                    case "months":      removalFromBillingHistory.months =      utilitiesHelper.ReadStringNullable(item.Value); break;
                                    case "statusCD":    removalFromBillingHistory.statusCD =    (int)utilitiesHelper.ReadInt32Nullable(item.Value); break;
                                    case "s_Desc":      removalFromBillingHistory.s_Desc =      utilitiesHelper.ReadStringNullable(item.Value); break;
                                    case "comments":    removalFromBillingHistory.comments =    utilitiesHelper.ReadStringNullable(item.Value); break;
                                    case "audModUser":  removalFromBillingHistory.audModUser =  utilitiesHelper.ReadStringNullable(item.Value); break;
                                    default: DocumentTemplate.DisplayBody($"{item.Name} Property Item Does Not Exist"); break;
                                }
                            }
                            items.Add(removalFromBillingHistory);
                        }
                        removalFromBillingsHistoryResponse.data = items.ToArray();
                        break;
                    default: DocumentTemplate.DisplayBody($"{property.Name} Property Does Not Exist"); break;
                }                
            }
            return removalFromBillingsHistoryResponse;
        }
        public ExecutionOutcomeAndDataBooleanResponse PopulateRemovalFromBillingsResponse(RestResponse response)
        {
            using JsonDocument jsDoc = JsonDocument.Parse(response.Content);
            var removalFromBillingsResponse = new ExecutionOutcomeAndDataBooleanResponse
            {
                executionOutcome = new ExecutionOutcome()
            };
            foreach (var property in jsDoc.RootElement.EnumerateObject())
            {
                switch (property.Name)
                {
                    case "succeeded":   removalFromBillingsResponse.executionOutcome.succeeded =    (bool)utilitiesHelper.ReadBooleanNullable(property.Value); break;
                    case "message":     removalFromBillingsResponse.executionOutcome.message =      utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "errors":      removalFromBillingsResponse.executionOutcome.errors =       utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "data":        removalFromBillingsResponse.data =                          (bool)utilitiesHelper.ReadBooleanNullable(property.Value); break;
                    default: DocumentTemplate.DisplayBody($"{property.Name} Property Does Not Exist"); break;
                }
            }
            return removalFromBillingsResponse;
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
