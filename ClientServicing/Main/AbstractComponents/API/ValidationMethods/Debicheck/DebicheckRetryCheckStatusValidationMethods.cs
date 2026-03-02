using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.Debicheck;
using ClientServicing.Main.Models.Debicheck;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.Debicheck
{
    public class DebicheckRetryCheckStatusValidationMethods : AbstractValidationMethods, IDebicheckRetryCheckStatus
    {
        UtilitiesHelper utilitiesHelper = new();
        public void ValidateDebicheckRetryCheckStatusRequestIsNotNullOrEmpty(DebicheckRetryCheckStatusRequest debicheckRetryCheckStatusRequest)
        {
            Assert.That(debicheckRetryCheckStatusRequest, Is.Not.Null, "DebicheckRetryCheckStatusRequest Should Not Be Null");
            Assert.Multiple(() =>
            {
                Assert.That(debicheckRetryCheckStatusRequest.policyNumber,          Is.Not.Null.Or.Not.Empty,                   "Response: PolicyNumber Should Not Be Null Or Not Empty");
                Assert.That(debicheckRetryCheckStatusRequest.identityNumber,        Is.Not.Null.Or.Not.Empty,                   "Response: IdentityNumber Should Not Be Null Or Not Empty");
                Assert.That(debicheckRetryCheckStatusRequest.cellPhoneNumber,       Is.Not.Null.Or.Not.Empty,                   "Response: CellPhoneNumber Should Not Be Null Or Not Empty");
                Assert.That(debicheckRetryCheckStatusRequest.accountNumber,         Is.Not.Null.Or.Not.Empty,                   "Response: AccountNumber Should Not Be Null Or Not Empty");
                Assert.That(debicheckRetryCheckStatusRequest.branchCode,            Is.Not.Null.Or.Not.Empty,                   "Response: BranchCode Should Not Be Null Or Not Empty");
                Assert.That(debicheckRetryCheckStatusRequest.accountType,           Is.Not.Null.Or.Not.Empty,                   "Response: AccountType Should Not Be Null Or Not Empty");
                Assert.That(debicheckRetryCheckStatusRequest.bankName,              Is.Not.Null.Or.Not.Empty,                   "Response: BankName Should Not Be Null or Empty");
                Assert.That(debicheckRetryCheckStatusRequest.surnameOrCompanyName,  Is.Not.Null.Or.Not.Empty,                   "Response: SurnameOrCompanyName Should Not Be Null Or Not Empty");
                Assert.That(debicheckRetryCheckStatusRequest.initials,              Is.Not.Null.Or.Not.Empty,                   "Response: Initials Should Not Be Null or Empty");
                Assert.That(debicheckRetryCheckStatusRequest.amount,                Is.Not.Null.Or.Not.Empty.Or.LessThan(0),    "Response: Amount Should Not Be Null Or Not Empty Or Less Than 0");
                Assert.That(debicheckRetryCheckStatusRequest.bypassD3Check,         Is.True.Or.False,                           "Response: BypassD3Check Should Be True or False");
                Assert.That(debicheckRetryCheckStatusRequest.sourceSystemId,        Is.Not.LessThan(0),                         "Response: SourceSystemId Should Not Be Less Than 0");
            });
            DocumentTemplate.DisplayBody("Validated: DebicheckRetryCheckStatusRequest Data Has Valid Properties Values");

        }

        public void ValidateDebicheckRetryCheckStatusResponseIsNotNullOrEmpty(DebicheckRetryCheckStatusResponse debicheckRetryCheckStatusResponse)
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(debicheckRetryCheckStatusResponse.policyNumber,             Is.Not.Null.Or.Not.Empty,           "Response: PolicyNumber Should Not Be Null Or Not Empty");
                Assert.That(debicheckRetryCheckStatusResponse.debiCheckStatus,          Is.Not.Null.Or.Not.Empty,           "Response: DebiCheckStatus Should Not Be Null Or Not Empty");
                Assert.That(debicheckRetryCheckStatusResponse.retryCount,               Is.Not.LessThan(0),                 "Response: RetryCount Should Not Be Null Or Not Empty");
                Assert.That(debicheckRetryCheckStatusResponse.retryAllowed,             Is.True.Or.False,                   "Response: RetryAllowed Should Not Be Null Or Not Empty");
                Assert.That(debicheckRetryCheckStatusResponse.latestMandateCreatedAt,   Is.Not.EqualTo(default(DateTime)),  "Response: LatestMandateCreatedAt Should Not Be Null Or Not Empty");
            }
            DocumentTemplate.DisplayBody("Validated: DebicheckRetryCheckStatusResponse Data Has Valid Properties Values");

        }

        public DebicheckRetryCheckStatusResponse PopulateDebicheckRetryCheckStatusResponse(RestResponse restResponse) {
            using JsonDocument jsonDocument = JsonDocument.Parse(restResponse.Content!);
            JsonElement root = jsonDocument.RootElement;
            DebicheckRetryCheckStatusResponse debicheckRetryCheckStatusResponse = new();

            foreach (var property in root.EnumerateObject()) {
                switch (property.Name) {
                    case "policyNumber":            debicheckRetryCheckStatusResponse.policyNumber =            utilitiesHelper.ReadStringNullable(property.Value)!; break;
                    case "debiCheckStatus":         debicheckRetryCheckStatusResponse.debiCheckStatus =         utilitiesHelper.ReadStringNullable(property.Value)!; break;
                    case "retryCount":              debicheckRetryCheckStatusResponse.retryCount =              (int)utilitiesHelper.ReadInt32Nullable(property.Value)!; break;
                    case "retryAllowed":            debicheckRetryCheckStatusResponse.retryAllowed =            (bool)utilitiesHelper.ReadBooleanNullable(property.Value)!; break;
                    case "latestMandateCreatedAt":  debicheckRetryCheckStatusResponse.latestMandateCreatedAt =  (DateTime)utilitiesHelper.ReadDateTimeNullable(property.Value)!; break;
                    default: TestContext.Out.WriteLine($"Unknown property: {property.Name}"); break;
                }
            }
            return debicheckRetryCheckStatusResponse;
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
