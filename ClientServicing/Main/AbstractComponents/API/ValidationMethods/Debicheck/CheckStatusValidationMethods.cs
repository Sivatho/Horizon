using System.Collections.Generic;
using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.Debicheck;
using ClientServicing.Main.Models.Debicheck;
using ClientServicing.Main.Models.Email;
using ClientServicing.Main.Resources.Helper;
using javax.xml.crypto;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.Debicheck
{
    public class CheckStatusValidationMethods : AbstractValidationMethods, ICheckStatusValidationMethods
    {
        UtilitiesHelper utilitiesHelper = new UtilitiesHelper();

        public void ValidateCheckStatusRequestDataIsNotNullOrEmpty(CheckStatusRequestData checkStatusRequestData)
        {
            Assert.Multiple(() =>
            {
                Assert.That(checkStatusRequestData, Is.Not.Null, "CheckStatusRequest Should Not Be Null");
                foreach (DebicheckRetryCheckStatusRequest checkStatusRequest in checkStatusRequestData.listOdCheckStatusRequest!)
                {
                    Assert.That(checkStatusRequest.identityNumber,          Is.Not.Null.And.Not.Empty,  "Response: IdentityNumber Should Not Be Null Or Empty");
                    Assert.That(checkStatusRequest.cellPhoneNumber,         Is.Not.Null.And.Not.Empty,  "Response: CellPhoneNumber Should Not Be Null Or Empty");
                    Assert.That(checkStatusRequest.accountNumber,           Is.Not.Null.And.Not.Empty,  "Response: AccountNumber Should Not Be Null Or Empty");
                    Assert.That(checkStatusRequest.branchCode,              Is.Not.Null.And.Not.Empty,  "Response: BranchCode Should Not Be Null Or Empty");
                    Assert.That(checkStatusRequest.accountType,             Is.Not.Null.And.Not.Empty,  "Response: AccountType Should Not Be Null Or Empty");
                    Assert.That(checkStatusRequest.bankName,                Is.Not.Null.And.Not.Empty,  "Response: BankName Should Not Be Null Or Empty");
                    Assert.That(checkStatusRequest.surnameOrCompanyName,    Is.Not.Null.And.Not.Empty,  "Response: SurnameOrCompanyName Should Not Be Null Or Empty");
                    Assert.That(checkStatusRequest.initials,                Is.Not.Null.And.Not.Empty,  "Response: Initials Should Not Be Null Or Empty");
                    Assert.That(checkStatusRequest.amount,                  Is.Not.LessThan(0),         "Response: Amount Should Not Be Less Than 0");
                    Assert.That(checkStatusRequest.bypassD3Check,           Is.True.Or.False,           "Response: BypassD3Check Should True Or False");
                    Assert.That(checkStatusRequest.sourceSystemId,          Is.Not.LessThan(0),         "Response: SourceSystemId Should Not Be Less Than 0");
                }
            });
            DocumentTemplate.DisplayBody("Validated: CheckStatusRequest Data Has Valid Properties and Values");
        }

        public void ValidateCheckStatusResponseDataIsNotNullOrEmpty(CheckStatusResponse checkStatusResponse)
        {
            Assert.Multiple(() => {
                Assert.That(checkStatusResponse,            Is.Not.Null,                "CheckStatusResponse Should Not Be Null");
                Assert.That(checkStatusResponse.success,    Is.True.Or.False,           "Response: Success Should Be True Or False");
                Assert.That(checkStatusResponse.message,    Is.Null.And.Not.Empty,      "Response: Message Should Be Null Or Not Empty");
                Assert.That(checkStatusResponse.result,     Is.Not.Null.And.Not.Empty,  "Response: Result Should Not Be Null Or Empty");
                var resultList = new List<CheckStatusResponseResult>();
                foreach (var result in checkStatusResponse.result!) { 

                }
            });
            DocumentTemplate.DisplayBody("Validated: CheckStatusResponse Data Has Valid Properties and Values");
        }
        public CheckStatusResponse PopulateCheckStatusResponse(RestResponse restResponse)
        {
            using JsonDocument jsonDocument = JsonDocument.Parse(restResponse.Content);
            JsonElement root = jsonDocument.RootElement;

            var checkStatusResponse = new CheckStatusResponse
            {
                result = new List<CheckStatusResponseResult>()
            };

            foreach (var property in root.EnumerateObject())
            {
                switch (property.Name)
                {
                    case "success": checkStatusResponse.success = (bool)utilitiesHelper.ReadBooleanNullable(property.Value)!; break;
                    case "message": checkStatusResponse.message = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "result":
                        var resultList = new List<CheckStatusResponseResult>();

                        foreach (var resultItem in property.Value.EnumerateArray())
                        {
                            var checkStatusResponseResult = new CheckStatusResponseResult
                            {
                                data = new List<CheckStatusResponseData>()
                            };
                            foreach (var resultProperty in resultItem.EnumerateObject())
                            {
                                switch (resultProperty.Name)
                                {
                                    case "success": checkStatusResponseResult.success = (bool)utilitiesHelper.ReadBooleanNullable(resultProperty.Value)!; break;
                                    case "message": checkStatusResponseResult.message = utilitiesHelper.ReadStringNullable(resultProperty.Value); break;
                                    case "data":
                                        var dataList = new List<CheckStatusResponseData>();
                                        foreach (var dataItem in resultProperty.Value.EnumerateArray())
                                        {
                                            var checkStatusResponseData = new CheckStatusResponseData();
                                            foreach (var dataProperty in dataItem.EnumerateObject())
                                            {
                                                switch (dataProperty.Name)
                                                {
                                                    case "amount": checkStatusResponseData.amount =                                 (int)utilitiesHelper.ReadInt32Nullable(dataProperty.Value)!; break;
                                                    case "ifaBusinessFeeIncluded": checkStatusResponseData.ifaBusinessFeeIncluded = (bool)utilitiesHelper.ReadBooleanNullable(dataProperty.Value)!; break;
                                                    case "success": checkStatusResponseData.success =                               (bool)utilitiesHelper.ReadBooleanNullable(dataProperty.Value)!; break;
                                                    case "message": checkStatusResponseData.message =                               utilitiesHelper.ReadStringNullable(dataProperty.Value); break;
                                                    case "status": checkStatusResponseData.status =                                 utilitiesHelper.ReadStringNullable(dataProperty.Value); break;
                                                    case "payerIdentityNumber": checkStatusResponseData.payerIdentityNumber =       utilitiesHelper.ReadStringNullable(dataProperty.Value); break;
                                                    case "payerMobileTelephoneNumber": checkStatusResponseData.payerMobileTelephoneNumber = utilitiesHelper.ReadStringNullable(dataProperty.Value); break;
                                                    case "policyNumber": checkStatusResponseData.policyNumber =                     utilitiesHelper.ReadStringNullable(dataProperty.Value); break;
                                                    case "createdAt": checkStatusResponseData.createdAt =                           utilitiesHelper.ReadDateTimeNullable(dataProperty.Value)!.Value; break;
                                                    case "mandateType": checkStatusResponseData.mandateType =                       utilitiesHelper.ReadStringNullable(dataProperty.Value); break;
                                                    default: TestContext.Out.WriteLine($"Unknown property: {dataProperty.Name}"); break;
                                                }
                                            }
                                            dataList.Add(checkStatusResponseData);
                                        }
                                        checkStatusResponseResult.data = dataList;
                                        break;
                                    default: TestContext.Out.WriteLine($"Unknown property: {resultProperty.Name}"); break;
                                }
                            }
                            resultList.Add(checkStatusResponseResult);
                        }
                        checkStatusResponse.result = resultList;
                        break;                        
                    default: TestContext.Out.WriteLine($"Unknown property: {property.Name}"); break;
                }
            }
            return checkStatusResponse;
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
