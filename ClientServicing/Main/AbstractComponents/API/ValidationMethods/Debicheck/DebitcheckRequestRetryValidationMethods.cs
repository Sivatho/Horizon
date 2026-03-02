using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.Debicheck;
using ClientServicing.Main.Models.Debicheck;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.Debicheck
{
    public class DebitcheckRequestRetryValidationMethods : AbstractValidationMethods, IDebitcheckRequestRetry
    {
        public void ValidateDebicheckRetryCheckStatusRequestIsNotNullOrEmpty(DebicheckRetryCheckStatusRequest debicheckRetryCheckStatusRequest)
        {
            Assert.That(debicheckRetryCheckStatusRequest, Is.Not.Null, "DebicheckRetryCheckStatusRequest Should Not Be Null");
            Assert.Multiple(() =>
            {
                Assert.That(debicheckRetryCheckStatusRequest.identityNumber,        Is.Not.Null.And.Not.Empty,  "Response: IdentityNumber Should Not Be Null Or Empty");
                Assert.That(debicheckRetryCheckStatusRequest.cellPhoneNumber,       Is.Not.Null.And.Not.Empty,  "Response: CellPhoneNumber Should Not Be Null Or Empty");
                Assert.That(debicheckRetryCheckStatusRequest.accountNumber,         Is.Not.Null.And.Not.Empty,  "Response: AccountNumber Should Not Be Null Or Empty");
                Assert.That(debicheckRetryCheckStatusRequest.branchCode,            Is.Not.Null.And.Not.Empty,  "Response: BranchCode Should Not Be Null Or Empty");
                Assert.That(debicheckRetryCheckStatusRequest.accountType,           Is.Not.Null.And.Not.Empty,  "Response: AccountType Should Not Be Null Or Empty");
                Assert.That(debicheckRetryCheckStatusRequest.bankName,              Is.Not.Null.And.Not.Empty,  "Response: BankName Should Not Be Null Or Empty");
                Assert.That(debicheckRetryCheckStatusRequest.surnameOrCompanyName,  Is.Not.Null.And.Not.Empty,  "Response: SurnameOrCompanyName Should Not Be Null Or Empty");
                Assert.That(debicheckRetryCheckStatusRequest.initials,              Is.Not.Null.And.Not.Empty,  "Response: Initials Should Not Be Null Or Empty");
                Assert.That(debicheckRetryCheckStatusRequest.amount,                Is.Not.LessThan(0),         "Response: Amount Should Not Be Less Than 0");
                Assert.That(debicheckRetryCheckStatusRequest.bypassD3Check,         Is.True.Or.False,           "Response: BypassD3Check Should True Or False");
                Assert.That(debicheckRetryCheckStatusRequest.sourceSystemId,        Is.Not.LessThan(0),         "Response: SourceSystemId Should Not Be Less Than 0");
            });
            DocumentTemplate.DisplayBody("Validated: CheckStatusRequest Data Has Valid Properties and Values");
        }

        public void ValidateDebicheckRetryCheckStatusResponseIsNotNullOrEmpty(DebicheckRetryCheckStatusResponseData debicheckRetryCheckStatusResponse)
        {
            Assert.Multiple(() =>
            {
                Assert.That(debicheckRetryCheckStatusResponse,                      Is.Not.Null,                    "DebicheckRetryCheckStatusResponse Should Not Be Null");
                Assert.That(debicheckRetryCheckStatusResponse.success,              Is.True.Or.False,               "Response: Success Should Be True Or False");
                Assert.That(debicheckRetryCheckStatusResponse.didError,             Is.True.Or.False,               "Response: DidError Should Be True Or False");
                Assert.That(debicheckRetryCheckStatusResponse.message,              Is.Not.Null.And.Not.Empty,      "Response: Message Should Not Be Null Or Empty");
                Assert.That(debicheckRetryCheckStatusResponse.data,                 Is.Not.Null,                    "Response: Data Should Not Be Null");
                Assert.That(debicheckRetryCheckStatusResponse.data.mandateType,     Is.Not.Null.And.Not.Empty,      "Response: Data: RetryCheckStatus Should Not Be Null Or Empty");
                Assert.That(debicheckRetryCheckStatusResponse.data.statusReason,    Is.Null.Or.TypeOf<string>(),    "Response: Data: RetryCheckStatus Should  Be Null Or Type Of String");

            });
            DocumentTemplate.DisplayBody("Validated: DebicheckRetryCheckStatusResponseData Data Has Valid Properties and Values");
        }
        public DebicheckRetryCheckStatusResponseData PopulateDebicheckRetryCheckStatusResponseData(RestResponse restResponse) {
            using JsonDocument jsonDocument = JsonDocument.Parse(restResponse.Content);
            JsonElement root = jsonDocument.RootElement;

            var debicheckRetryCheckStatusResponseData = new DebicheckRetryCheckStatusResponseData()
            {
                data = new DebitCheckRetryStatusResponseDataModel()
            };
            foreach(var property in root.EnumerateObject())
            {
                switch(property.Name)
                {
                    case "success":
                        debicheckRetryCheckStatusResponseData.success = property.Value.GetBoolean();
                        break;
                    case "didError":
                        debicheckRetryCheckStatusResponseData.didError = property.Value.GetBoolean();
                        break;
                    case "message":
                        debicheckRetryCheckStatusResponseData.message = property.Value.GetString();
                        break;
                    case "data":
                        foreach(var dataProperty in property.Value.EnumerateObject())
                        {
                            switch(dataProperty.Name)
                            {
                                case "mandateType":
                                    debicheckRetryCheckStatusResponseData.data.mandateType = dataProperty.Value.GetString();
                                    break;
                                case "statusReason":
                                    if(dataProperty.Value.ValueKind != JsonValueKind.Null)
                                    {
                                        debicheckRetryCheckStatusResponseData.data.statusReason = dataProperty.Value.GetString();
                                    }
                                    break;
                            }
                        }
                        break;
                }
            }
            return debicheckRetryCheckStatusResponseData;
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
