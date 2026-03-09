using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.Bank;
using ClientServicing.Main.Models.Bank;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.Bank
{
    public class ValidateBankAccountQAVSRValidationMethods : AbstractValidationMethods, IValidateBankAccountQAVSRValidationMethods
    {
        UtilitiesHelper utilitiesHelper = new();
        public void ValidateValidateBankAccountQAVSRRequestDataIsNotNullOrEmptyOrLessThanZero(ValidateBankAccountQAVSRRequest validateBankAccountQAVSRRequest)
        {
            Assert.Multiple(() =>
            {
                Assert.That(validateBankAccountQAVSRRequest.payerEntityNo,          Is.Not.LessThan(0),                 "Request: payerEntityNo Should Not Be Less Than 0");
                Assert.That(validateBankAccountQAVSRRequest.policyNumber,           Is.Null.Or.TypeOf<string>(),        "Request: policyNumber Should Not Be Null Or Empty");
                Assert.That(validateBankAccountQAVSRRequest.productCategory,        Is.Null.Or.TypeOf<string>(),        "Request: productCategory Should Not Be Null Or Empty");
                Assert.That(validateBankAccountQAVSRRequest.productCategoryId,      Is.Not.LessThan(0),                 "Request: productCategoryId Should Not Be Less Than 0");
                Assert.That(validateBankAccountQAVSRRequest.title,                  Is.Null.Or.TypeOf<string>(),          "Request: title Should Not Be Null Or Empty");
                Assert.That(validateBankAccountQAVSRRequest.titleCd,                Is.Not.LessThan(0),                 "Request: titleCd Should Not Be Less Than 0");
                Assert.That(validateBankAccountQAVSRRequest.firstName,              Is.Null.Or.TypeOf<string>(),        "Request: firstName Should Not Be Null Or Empty");
                Assert.That(validateBankAccountQAVSRRequest.surname,                Is.Null.Or.TypeOf<string>(),        "Request: surname Should Not Be Null Or Empty");
                Assert.That(validateBankAccountQAVSRRequest.initials,               Is.Null.Or.TypeOf<string>(),        "Request: initials Should Not Be Null Or Empty");
                Assert.That(validateBankAccountQAVSRRequest.emailAddress,           Is.Null.Or.TypeOf<string>(),        "Request: emailAddress Should Not Be Null Or Empty");
                Assert.That(validateBankAccountQAVSRRequest.isAMember,              Is.True.Or.False,                   "Request: isAMember Should Be True Or False");
                Assert.That(validateBankAccountQAVSRRequest.legalRefNo,             Is.Not.Null.And.Not.Empty,          "Request: legalRefNo Should Not Be Null Or Empty");
                Assert.That(validateBankAccountQAVSRRequest.legalRefNoTypeCD,       Is.Not.LessThan(0),                 "Request: legalRefNoTypeCD Should Not Be Less Than 0");
                Assert.That(validateBankAccountQAVSRRequest.dateOfBirth,            Is.Not.EqualTo(default(DateTime)),  "Request: dateOfBirth Should Not Be Equal To Default DateTime");
                Assert.That(validateBankAccountQAVSRRequest.genderCd,               Is.Not.LessThan(0),                 "Request: genderCd Should Not Be Less Than 0");
                Assert.That(validateBankAccountQAVSRRequest.employerName,           Is.Null.Or.TypeOf<string>(),        "Request: employerName Should Not Be Null Or Empty");
                Assert.That(validateBankAccountQAVSRRequest.employeeNumber,         Is.Null.Or.TypeOf<string>(),        "Request: employeeNumber Should Not Be Null Or Empty");
                Assert.That(validateBankAccountQAVSRRequest.employeeDepartmentCd,   Is.Null.Or.TypeOf<string>(),        "Request: employeeDepartmentCd Should Not Be Null Or Empty");
                Assert.That(validateBankAccountQAVSRRequest.employeeDepartment,     Is.Null.Or.TypeOf<string>(),        "Request: employeeDepartment Should Not Be Null Or Empty");
                Assert.That(validateBankAccountQAVSRRequest.authorizationTypeCd,    Is.Not.LessThan(0),                 "Request: authorizationTypeCd Should Not Be Less Than 0");
                Assert.That(validateBankAccountQAVSRRequest.payroll,                Is.Null.Or.TypeOf<string>(),        "Request: payroll Should Not Be Null Or Empty");
                Assert.That(validateBankAccountQAVSRRequest.mandateType,            Is.Null.Or.TypeOf<string>(),        "Request: mandateType Should Not Be Null Or Empty");
                Assert.That(validateBankAccountQAVSRRequest.agentID,                Is.Null.Or.TypeOf<string>(),        "Request: agentID Should Not Be Null Or Empty");
                Assert.That(validateBankAccountQAVSRRequest.isAuthorized,           Is.True.Or.False,                   "Request: isAuthorized Should Be True Or False");
                Assert.That(validateBankAccountQAVSRRequest.homeNumber,             Is.Null.Or.TypeOf<string>(),        "Request: homeNumber Should Not Be Null Or Empty");
                Assert.That(validateBankAccountQAVSRRequest.cellNumber,             Is.Null.Or.TypeOf<string>(),        "Request: cellNumber Should Not Be Null Or Empty");
                Assert.That(validateBankAccountQAVSRRequest.workNumber,             Is.Null.Or.TypeOf<string>(),        "Request: workNumber Should Not Be Null Or Empty");
                Assert.That(validateBankAccountQAVSRRequest.relationCd,             Is.Not.LessThan(0),                 "Request: relationCd Should Not Be Less Than 0");
                Assert.That(validateBankAccountQAVSRRequest.bankId,                 Is.Not.LessThan(0),                 "Request: bankId Should Not Be Less Than 0");
                Assert.That(validateBankAccountQAVSRRequest.bankName,               Is.Null.Or.TypeOf<string>(),        "Request: bankName Should Not Be Null Or Empty");
                Assert.That(validateBankAccountQAVSRRequest.bankShortName,          Is.Null.Or.TypeOf<string>(),        "Request: bankShortName Should Not Be Null Or Empty");
                Assert.That(validateBankAccountQAVSRRequest.branchNo,               Is.Not.LessThan(0),                 "Request: branchNo Should Not Be Less Than 0");
                Assert.That(validateBankAccountQAVSRRequest.branchCode,             Is.Null.Or.TypeOf<string>(),        "Request: branchCode Should Not Be Null Or Empty");
                Assert.That(validateBankAccountQAVSRRequest.bankAccNo,              Is.Null.Or.TypeOf<string>(),        "Request: bankAccNo Should Not Be Null Or Empty");
                Assert.That(validateBankAccountQAVSRRequest.bankAccHolderInitial,   Is.Null.Or.TypeOf<string>(),        "Request: bankAccHolderInitial Should Not Be Null Or Empty");
                Assert.That(validateBankAccountQAVSRRequest.bankAccHolderName,      Is.Null.Or.TypeOf<string>(),        "Request: bankAccHolderName Should Not Be Null Or Empty");
                Assert.That(validateBankAccountQAVSRRequest.bankAccTypeCD,          Is.Not.LessThan(0),                 "Request: bankAccTypeCD Should Not Be Less Than 0");
                Assert.That(validateBankAccountQAVSRRequest.bankAccTypeDesc,        Is.Null.Or.TypeOf<string>(),        "Request: bankAccTypeDesc Should Not Be Null Or Empty");
                Assert.That(validateBankAccountQAVSRRequest.bankAccountID,          Is.Not.LessThan(0),                 "Request: bankAccountID Should Not Be Less Than 0");
                Assert.That(validateBankAccountQAVSRRequest.bankAccSwiftCode,       Is.Null.Or.TypeOf<string>(),        "Request: bankAccSwiftCode Should Not Be Null Or Empty");
                Assert.That(validateBankAccountQAVSRRequest.isActive,               Is.Not.LessThan(0),                 "Request: isActive Should Not Be Less Than 0");
                Assert.That(validateBankAccountQAVSRRequest.paymentTypeCD,          Is.Not.LessThan(0),                 "Request: paymentTypeCD Should Not Be Less Than 0");
                Assert.That(validateBankAccountQAVSRRequest.paymentFreqCD,          Is.Not.LessThan(0),                 "Request: paymentFreqCD Should Not Be Less Than 0");
                Assert.That(validateBankAccountQAVSRRequest.paymentRefId,           Is.Not.LessThan(0),                 "Request: paymentRefId Should Not Be Less Than 0");
                Assert.That(validateBankAccountQAVSRRequest.premium,                Is.Not.LessThan(0),                 "Request: premium Should Not Be Less Than 0");
                Assert.That(validateBankAccountQAVSRRequest.earlyTracking,          Is.True.Or.False,                   "Request: earlyTracking Should Be True Or False");
                Assert.That(validateBankAccountQAVSRRequest.debitDay,               Is.Not.LessThan(0),                 "Request: debitDay Should Not Be Less Than 0");
                Assert.That(validateBankAccountQAVSRRequest.firstDebitDay,          Is.Not.EqualTo(default(DateTime)),  "Request: firstDebitDay Should Not Be Equal To Default DateTime");
                Assert.That(validateBankAccountQAVSRRequest.firstDebitMonth,        Is.Not.EqualTo(default(DateTime)),  "Request: firstDebitMonth Should Not Be Equal To Default DateTime");
                Assert.That(validateBankAccountQAVSRRequest.effectiveDate,          Is.Not.EqualTo(default(DateTime)),  "Request: effectiveDate Should Not Be Equal To Default DateTime");
                
                Assert.That(validateBankAccountQAVSRRequest.gsd,                    Is.Null.Or.TypeOf<object>(),        "Request: gsd Should Be Null Or Type Of Object");
                Assert.That(validateBankAccountQAVSRRequest.gsd!.deductionAuthorization,        Is.Null.Or.True.Or.False,        "Request: gsd.gsdId Should Be True Or False");
                Assert.That(validateBankAccountQAVSRRequest.gsd.payrollName,        Is.Null.Or.TypeOf<string>(),        "Request: gsd.gsdName Should Not Be Null Or Empty");
                Assert.That(validateBankAccountQAVSRRequest.gsd.payrollId,          Is.Null.Or.TypeOf<string>(),        "Request: gsd.gsdShortName Should Not Be Null Or Empty");
                Assert.That(validateBankAccountQAVSRRequest.gsd.departmentId,       Is.Not.LessThan(0),                 "Request: gsd.gsdType Should Not Be Less Than 0");
                Assert.That(validateBankAccountQAVSRRequest.gsd.departmentName,     Is.Null.Or.TypeOf<string>(),        "Request: gsd.departmentName Should Not Be Null Or Empty");
                Assert.That(validateBankAccountQAVSRRequest.gsd.employeeNumber,     Is.Null.Or.TypeOf<string>(),        "Request: gsd.companyId Should Not Be Less Than 0");
                Assert.That(validateBankAccountQAVSRRequest.gsd.mandateType,        Is.Null.Or.TypeOf<string>(),        "Request: gsd.employeeName Should Not Be Null Or Empty");

                Assert.That(validateBankAccountQAVSRRequest.lastChanged,            Is.Not.EqualTo(default(DateTime)),  "Request: lastChanged Should Not Be Equal To Default DateTime");
                Assert.That(validateBankAccountQAVSRRequest.userID,                 Is.Null.Or.TypeOf<string>(),        "Request: userID Should Not Be Null Or Empty");
                Assert.That(validateBankAccountQAVSRRequest.entityNo,               Is.Null.Or.TypeOf<string>(),        "Request: entityNo Should Not Be Null Or Empty");
                Assert.That(validateBankAccountQAVSRRequest.agentCode,              Is.Null.Or.TypeOf<string>(),        "Request: agentCode Should Not Be Null Or Empty");
                Assert.That(validateBankAccountQAVSRRequest.agentName,              Is.Null.Or.TypeOf<string>(),        "Request: agentName Should Not Be Null Or Empty");
                Assert.That(validateBankAccountQAVSRRequest.payAtNumber,            Is.Null.Or.TypeOf<string>(),        "Request: payAtNumber Should Not Be Null Or Empty");
                Assert.That(validateBankAccountQAVSRRequest.csdEmployeeNo,          Is.Null.Or.TypeOf<string>(),        "Request: csdEmployeeNo Should Not Be Null Or Empty");
                Assert.That(validateBankAccountQAVSRRequest.csdCompanyDepartment,   Is.Null.Or.TypeOf<string>(),        "Request: csdCompanyDepartment Should Not Be Null Or Empty");
                Assert.That(validateBankAccountQAVSRRequest.csdCompanyName,         Is.Null.Or.TypeOf<string>(),        "Request: csdCompanyName Should Not Be Null Or Empty");
                Assert.That(validateBankAccountQAVSRRequest.csdCompanyCd,           Is.Not.LessThan(0),                 "Request: csdCompanyCd Should Not Be Less Than 0");
            });
            DocumentTemplate.DisplayBody("Validated: validateBankAccountQAVSRRequest: Is Not Null");
        }
        public void ValidateValidateBankAccountQAVSRResponsetResponseDataIsNotNullOrEmptyOrLessThanZero(ValidateBankAccountQAVSRResponse validateBankAccountQAVSRResponse)
        {
            Assert.Multiple(() =>
            {
                Assert.That(validateBankAccountQAVSRResponse.isValid,                                   Is.True.Or.False,               "Response: isValid Should Be True Or False");
                Assert.That(validateBankAccountQAVSRResponse.shouldUpdateAccountType,                   Is.True.Or.False,               "Response: shouldUpdateAccountType Should Be True Or False");
                Assert.That(validateBankAccountQAVSRResponse.overrideIsValid,                           Is.True.Or.False,               "Response: overrideIsValid Should Be True Or False");
                Assert.That(validateBankAccountQAVSRResponse.message,                                   Is.Null.Or.TypeOf<string?>(),   "Response: message Should Be Null Or Type Of String");
                Assert.That(validateBankAccountQAVSRResponse.fraudsterFailure,                          Is.Not.LessThan(0),             "Response: fraudsterFailure Should Not Be Less Than 0");
                Assert.That(validateBankAccountQAVSRResponse.correctBankName,                           Is.Null.Or.TypeOf<string>(),   "Response: correctBankName Should  Be Null Or Type Of String");
                                
                Assert.That(validateBankAccountQAVSRResponse.softyCompResult,                           Is.Not.Null,                    "Response: softyCompResult Should Not Be Null");
                Assert.That(validateBankAccountQAVSRResponse.softyCompResult.isValid,                   Is.True.Or.False,               "Response: softyCompResult.isValid Should Be True Or False");
                Assert.That(validateBankAccountQAVSRResponse.softyCompResult.wasTestResultOverridden,   Is.True.Or.False,               "Response: softyCompResult.wasTestResultOverridden Should Be True Or False");
                Assert.That(validateBankAccountQAVSRResponse.softyCompResult.wasTestPerformed,          Is.True.Or.False,               "Response: softyCompResult.wasTestPerformed Should Be True Or False"); 
                Assert.That(validateBankAccountQAVSRResponse.softyCompResult.message,                   Is.Null.Or.TypeOf<string?>(),   "Response: softyCompResult.message Should Be Null Or Type Of String");

                Assert.That(validateBankAccountQAVSRResponse.fraudsterResult,                           Is.Not.Null,                    "Response: fraudsterResult Should Not Be Null");
                Assert.That(validateBankAccountQAVSRResponse.fraudsterResult.isValid,                   Is.True.Or.False,               "Response: fraudsterResult.isValid Should Be True Or False");
                Assert.That(validateBankAccountQAVSRResponse.fraudsterResult.wasTestResultOverridden,   Is.True.Or.False,               "Response: fraudsterResult.wasTestResultOverridden Should Be True Or False");
                Assert.That(validateBankAccountQAVSRResponse.fraudsterResult.wasTestPerformed,          Is.True.Or.False,               "Response: fraudsterResult.wasTestPerformed Should Be True Or False");
                Assert.That(validateBankAccountQAVSRResponse.fraudsterResult.fraudsterFailureType,      Is.Not.LessThan(0),             "Response: fraudsterResult.fraudsterFailureType Should Not Be Less Than 0");
                Assert.That(validateBankAccountQAVSRResponse.fraudsterResult.message,                   Is.Null.Or.TypeOf<string?>(),   "Response: fraudsterResult.message Should Be Null Or Type Of String");


                Assert.That(validateBankAccountQAVSRResponse.d3BlackListResult,                         Is.Not.Null,                    "Response: d3BlackListResult Should Not Be Null");
                Assert.That(validateBankAccountQAVSRResponse.d3BlackListResult.isValid,                 Is.True.Or.False,               "Response: d3BlackListResult.isValid Should Be True Or False");   
                Assert.That(validateBankAccountQAVSRResponse.d3BlackListResult.wasTestResultOverridden, Is.True.Or.False,               "Response: d3BlackListResult.wasTestResultOverridden Should Be True Or False");
                Assert.That(validateBankAccountQAVSRResponse.d3BlackListResult.wasTestPerformed,        Is.True.Or.False,               "Response: d3BlackListResult.wasTestPerformed Should Be True Or False");
                Assert.That(validateBankAccountQAVSRResponse.d3BlackListResult.message,                 Is.Null.Or.TypeOf<string?>(),   "Response: d3BlackListResult.message Should Be Null Or Type Of String");

                Assert.That(validateBankAccountQAVSRResponse.avsrResult,                                Is.Not.Null,                    "Response: avsrResult Should Not Be Null");
                Assert.That(validateBankAccountQAVSRResponse.avsrResult.avsrResult,                     Is.Not.Null,                    "Response: avsrResult.avsrResult Should Not Be Null");
                Assert.That(validateBankAccountQAVSRResponse.avsrResult.avsrResult.qLinkAvsrCheckId,    Is.Not.Null.And.Not.Empty,      "Response: avsrResult.avsrResult.qLinkAvsrCheckId Should Not Be Null Or Empty");
                Assert.That(validateBankAccountQAVSRResponse.avsrResult.avsrResult.errorCode,           Is.Not.Null.And.Not.Empty,      "Response: avsrResult.avsrResult.errorCode Should Not Be Null Or Empty");
                Assert.That(validateBankAccountQAVSRResponse.avsrResult.avsrResult.errorDescription,    Is.Not.Null.And.Not.Empty,      "Response: avsrResult.avsrResult.errorDescription Should Not Be Null Or Empty");
                Assert.That(validateBankAccountQAVSRResponse.avsrResult.avsrResult.sessionId,           Is.Not.Null.And.Not.Empty,      "Response: avsrResult.avsrResult.sessionId Should Not Be Null Or Empty");
                Assert.That(validateBankAccountQAVSRResponse.avsrResult.avsrResult.accountStatusId,     Is.Not.Null.And.Not.Empty,      "Response: avsrResult.avsrResult.accountStatusId Should Not Be Null Or Empty");
                Assert.That(validateBankAccountQAVSRResponse.avsrResult.avsrResult.wasCachedResultUsed, Is.True.Or.False,               "Response: avsrResult.avsrResult.wasCachedResultUsed Should Be True Or False");
                Assert.That(validateBankAccountQAVSRResponse.avsrResult.avsrResult.wasBankAccountFound, Is.True.Or.False,               "Response: avsrResult.avsrResult.wasBankAccountFound Should Be True Or False");
                Assert.That(validateBankAccountQAVSRResponse.avsrResult.avsrResult.isBankAccountOpen,   Is.True.Or.False,               "Response: avsrResult.avsrResult.isBankAccountOpen Should Be True Or False");
                Assert.That(validateBankAccountQAVSRResponse.avsrResult.avsrResult.doesBankAccountTypeMatch, Is.True.Or.False,          "Response: avsrResult.avsrResult.doesBankAccountTypeMatch Should Be True Or False");
                Assert.That(validateBankAccountQAVSRResponse.avsrResult.avsrResult.doesInitialsMatch,   Is.True.Or.False,               "Response: avsrResult.avsrResult.doesInitialsMatch Should Be True Or False");
                Assert.That(validateBankAccountQAVSRResponse.avsrResult.avsrResult.doesIdentityNumberMatch, Is.True.Or.False,           "Response: avsrResult.avsrResult.doesIdentityNumberMatch Should Be True Or False");
                Assert.That(validateBankAccountQAVSRResponse.avsrResult.avsrResult.doesNameMatch,       Is.True.Or.False,               "Response: avsrResult.avsrResult.doesNameMatch Should Be True Or False");
                Assert.That(validateBankAccountQAVSRResponse.avsrResult.avsrResult.doesAcceptsDebits,   Is.True.Or.False,               "Response: avsrResult.avsrResult.doesAcceptsDebbits Should Be True Or False");
                Assert.That(validateBankAccountQAVSRResponse.avsrResult.avsrResult.doesAcceptsCredits,  Is.True.Or.False,               "Response: avsrResult.avsrResult.doesAcceptsCredits Should Be True Or False");
                Assert.That(validateBankAccountQAVSRResponse.avsrResult.avsrResult.didTimeout,          Is.True.Or.False,               "Response: avsrResult.avsrResult.didTimeout Should Be True Or False");
                Assert.That(validateBankAccountQAVSRResponse.avsrResult.avsrResult.accountLengthMatch,  Is.True.Or.False,               "Response: avsrResult.avsrResult.accountLengthMatch Should Be True Or False");
                Assert.That(validateBankAccountQAVSRResponse.avsrResult.avsrResult.missingParameter,    Is.True.Or.False,               "Response: avsrResult.avsrResult.missingParameter Should Be True Or False");
                Assert.That(validateBankAccountQAVSRResponse.avsrResult.avsrResult.doesPhoneMatch,      Is.True.Or.False,               "Response: avsrResult.avsrResult.doesPhoneMatch Should Be True Or False");
                Assert.That(validateBankAccountQAVSRResponse.avsrResult.avsrResult.doesEmailMatch,      Is.True.Or.False,               "Response: avsrResult.avsrResult.doesEmailMatch Should Be True Or False");
                Assert.That(validateBankAccountQAVSRResponse.avsrResult.avsrResult.hasBankAccountBeenOpenForMoreThan3Months, Is.True.Or.False,  "Response: avsrResult.avsrResult.hasBankAccountBeenOpenForMoreThan3Months Should Be True Or False");
                Assert.That(validateBankAccountQAVSRResponse.avsrResult.avsrResult.responseDate,        Is.Not.EqualTo(default(DateTime)),      "Response: avsrResult.avsrResult.responseDate Should Not Be Equal To Default DateTime");
                Assert.That(validateBankAccountQAVSRResponse.avsrResult.avsrResult.wasTestPerformed,    Is.True.Or.False,               "Response: avsrResult.avsrResult.wasTestPerformed Should Be True Or False");
                Assert.That(validateBankAccountQAVSRResponse.avsrResult.avsrResult.isValid,             Is.True.Or.False,               "Response: avsrResult.avsrResult.isValid Should Be True Or False");
                Assert.That(validateBankAccountQAVSRResponse.avsrResult.avsrResult.message,             Is.Null.Or.TypeOf<string?>(),   "Response: avsrResult.avsrResult.message Should Be Null Or Type Of String");
                Assert.That(validateBankAccountQAVSRResponse.avsrResult.avsrResult.isForcedSuccessResponse, Is.True.Or.False,           "Response: avsrResult.avsrResult.isForcedSuccessResponse Should Be True Or False");
                
                Assert.That(validateBankAccountQAVSRResponse.avsrResult.payDay,                         Is.Not.LessThan(0),             "Response: avsrResult.payDay Should Not Be Less Than 0");
                Assert.That(validateBankAccountQAVSRResponse.avsrResult.hasException,                   Is.True.Or.False,               "Response: avsrResult.hasException Should Be True Or False");
                
                Assert.That(validateBankAccountQAVSRResponse.correctBankAccountType,                    Is.Not.Null,                    "Response: correctBankAccountType Should Not Be Null");
                Assert.That(validateBankAccountQAVSRResponse.correctBankAccountType.type,               Is.Not.LessThan(0),             "Response: correctBankAccountType Should not be nullShould not be less than zero");
            });
            DocumentTemplate.DisplayBody("Validated: validateBankAccountQAVSRResponse: Is Not Null Or Empty, Type Of string, Is True Or false, Integer Is Not Less Than 0, DateTime Is Not Equal To Default DateTime");
        }
        public ValidateBankAccountQAVSRResponse PopulateValidateBankAccountQAVSRResponse(RestResponse response)
        {
            using JsonDocument document = JsonDocument.Parse(response.Content);
            ValidateBankAccountQAVSRResponse validateBankAccountQAVSRResponse = new()
            {
                softyCompResult = new TestResult(),
                fraudsterResult = new TestResult(),
                d3BlackListResult = new TestResult(),
                avsrResult = new BankAccountAVSRResult(),
                correctBankAccountType = new CorrectBankAccountType()
            };

            foreach (var property in document.RootElement.EnumerateObject())
            {
                switch (property.Name)
                {
                    case "isvalid":                 validateBankAccountQAVSRResponse.isValid =                  (bool)utilitiesHelper.ReadBooleanNullable(property.Value)!; break;
                    case "shouldupdateaccounttype": validateBankAccountQAVSRResponse.shouldUpdateAccountType =  (bool)utilitiesHelper.ReadBooleanNullable(property.Value)!; break;
                    case "overrideisvalid":         validateBankAccountQAVSRResponse.overrideIsValid =          (bool)utilitiesHelper.ReadBooleanNullable(property.Value)!; break;
                    case "message":                 validateBankAccountQAVSRResponse.message =                  utilitiesHelper.ReadStringNullable(property.Value)!; break;
                    case "fraudsterFailure":        validateBankAccountQAVSRResponse.fraudsterFailure =         (int)utilitiesHelper.ReadInt32Nullable(property.Value)!; break;
                    case "correctBankName":         validateBankAccountQAVSRResponse.correctBankName =          utilitiesHelper.ReadStringNullable(property.Value) ?? null!; break;
                    case "softyCompResult":
                        var softyCompResult = new TestResult();
                        foreach (var item in property.Value.EnumerateObject())
                        {
                            switch (item.Name)
                            {
                                case "isValid":                 softyCompResult.isValid =                   (bool)utilitiesHelper.ReadBooleanNullable(item.Value)!; break;
                                case "wasTestResultOverridden": softyCompResult.wasTestResultOverridden =   (bool)utilitiesHelper.ReadBooleanNullable(item.Value)!; break;
                                case "wasTestPerformed":        softyCompResult.wasTestPerformed =          (bool)utilitiesHelper.ReadBooleanNullable(item.Value)!; break;
                                case "message":                 softyCompResult.message =                   utilitiesHelper.ReadStringNullable(item.Value) ?? null!; break;
                            }
                            validateBankAccountQAVSRResponse.softyCompResult = softyCompResult;
                        }
                        break;
                    case "fraudsterResult":
                        var fraudsterResult = new TestResult();
                        foreach (var item in property.Value.EnumerateObject())
                        {
                            switch (item.Name)
                            {
                                case "wasTestResultOverridden": fraudsterResult.wasTestResultOverridden =   (bool)utilitiesHelper.ReadBooleanNullable(item.Value)!; break;
                                case "wasTestPerformed":        fraudsterResult.wasTestPerformed =          (bool)utilitiesHelper.ReadBooleanNullable(item.Value)!; break;
                                case "fraudsterFailureType":    fraudsterResult.fraudsterFailureType =      (int)utilitiesHelper.ReadInt32Nullable(item.Value)!; break;
                                case "isValid":                 fraudsterResult.isValid =                   (bool)utilitiesHelper.ReadBooleanNullable(item.Value)!; break;
                                case "message":                 fraudsterResult.message =                   utilitiesHelper.ReadStringNullable(item.Value) ?? null!; break;
                            }
                            validateBankAccountQAVSRResponse.fraudsterResult = fraudsterResult;
                        }
                        break;
                    case "d3BlackListResult":
                        var d3BlackListResult = new TestResult();
                        foreach (var item in property.Value.EnumerateObject())
                        {
                            switch (item.Name)
                            {
                                case "wasTestResultOverridden": d3BlackListResult.wasTestResultOverridden = (bool)utilitiesHelper.ReadBooleanNullable(item.Value)!; break;
                                case "wasTestPerformed":        d3BlackListResult.wasTestPerformed =        (bool)utilitiesHelper.ReadBooleanNullable(item.Value)!; break;
                                case "isValid":                 d3BlackListResult.isValid =                 (bool)utilitiesHelper.ReadBooleanNullable(item.Value)!; break;
                                case "message":                 d3BlackListResult.message =                 utilitiesHelper.ReadStringNullable(item.Value) ?? null!; break;
                            }
                            validateBankAccountQAVSRResponse.d3BlackListResult = d3BlackListResult;
                        }
                        break;
                    case "avsrResult":
                        var bankAccountAVSRResult = new BankAccountAVSRResult() { 
                            avsrResult = new AVSRResult()
                        };
                        foreach (var item in property.Value.EnumerateObject())
                        {
                            switch (item.Name)
                            {
                                case "qLinkAvsrCheckId":            bankAccountAVSRResult.avsrResult.qLinkAvsrCheckId =           utilitiesHelper.ReadStringNullable(item.Value) ?? string.Empty; break;
                                case "errorCode":                   bankAccountAVSRResult.avsrResult.errorCode =                  utilitiesHelper.ReadStringNullable(item.Value) ?? string.Empty; break;
                                case "errorDescription":            bankAccountAVSRResult.avsrResult.errorDescription =           utilitiesHelper.ReadStringNullable(item.Value) ?? string.Empty; break;
                                case "sessionId":                   bankAccountAVSRResult.avsrResult.sessionId =                  utilitiesHelper.ReadStringNullable(item.Value) ?? string.Empty; break;
                                case "accountStatusId":             bankAccountAVSRResult.avsrResult.accountStatusId =            utilitiesHelper.ReadStringNullable(item.Value) ?? string.Empty; break;
                                case "wasCachedResultUsed":         bankAccountAVSRResult.avsrResult.wasCachedResultUsed =        (bool)utilitiesHelper.ReadBooleanNullable(item.Value)!; break;
                                case "wasBankAccountFound":         bankAccountAVSRResult.avsrResult.wasBankAccountFound =        (bool)utilitiesHelper.ReadBooleanNullable(item.Value)!; break;
                                case "isBankAccountOpen":           bankAccountAVSRResult.avsrResult.isBankAccountOpen =          (bool)utilitiesHelper.ReadBooleanNullable(item.Value)!; break;
                                case "doesBankAccountTypeMatch":    bankAccountAVSRResult.avsrResult.doesBankAccountTypeMatch =   (bool)utilitiesHelper.ReadBooleanNullable(item.Value)!; break;
                                case "doesInitialsMatch":           bankAccountAVSRResult.avsrResult.doesInitialsMatch =          (bool)utilitiesHelper.ReadBooleanNullable(item.Value)!; break;
                                case "doesIdentityNumberMatch":     bankAccountAVSRResult.avsrResult.doesIdentityNumberMatch =    (bool)utilitiesHelper.ReadBooleanNullable(item.Value)!; break;
                                case "doesNameMatch":               bankAccountAVSRResult.avsrResult.doesNameMatch =              (bool)utilitiesHelper.ReadBooleanNullable(item.Value)!; break;
                                case "doesAcceptsDebits":           bankAccountAVSRResult.avsrResult.doesAcceptsDebits =          (bool)utilitiesHelper.ReadBooleanNullable(item.Value)!; break;
                                case "doesAcceptsCredits":          bankAccountAVSRResult.avsrResult.doesAcceptsCredits =         (bool)utilitiesHelper.ReadBooleanNullable(item.Value)!; break;
                                case "didTimeout":                  bankAccountAVSRResult.avsrResult.didTimeout =                 (bool)utilitiesHelper.ReadBooleanNullable(item.Value)!; break;
                                case "accountLengthMatch":          bankAccountAVSRResult.avsrResult.accountLengthMatch =         (bool)utilitiesHelper.ReadBooleanNullable(item.Value)!; break;
                                case "missingParameter":            bankAccountAVSRResult.avsrResult.missingParameter =           (bool)utilitiesHelper.ReadBooleanNullable(item.Value)!; break;
                                case "doesPhoneMatch":              bankAccountAVSRResult.avsrResult.doesPhoneMatch =             (bool)utilitiesHelper.ReadBooleanNullable(item.Value)!; break;
                                case "doesEmailMatch":              bankAccountAVSRResult.avsrResult.doesEmailMatch =             (bool)utilitiesHelper.ReadBooleanNullable(item.Value)!; break;
                                case "payDay":                      bankAccountAVSRResult.payDay =                                  (int)utilitiesHelper.ReadInt32Nullable(item.Value)!; break;
                                case "hasBankAccountBeenOpenForMoreThan3Months": bankAccountAVSRResult.avsrResult.hasBankAccountBeenOpenForMoreThan3Months = (bool)utilitiesHelper.ReadBooleanNullable(item.Value)!; break;
                                case "responseDate":                bankAccountAVSRResult.avsrResult.responseDate =               (DateTime)utilitiesHelper.ReadDateTimeNullable(item.Value)!; break;
                                case "wasTestPerformed":            bankAccountAVSRResult.avsrResult.wasTestPerformed =           (bool)utilitiesHelper.ReadBooleanNullable(item.Value)!; break;
                                case "isValid":                     bankAccountAVSRResult.avsrResult.isValid =                    (bool)utilitiesHelper.ReadBooleanNullable(item.Value)!; break;
                                case "message":                     bankAccountAVSRResult.avsrResult.message =                    utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "isForcedSuccessResponse":     bankAccountAVSRResult.avsrResult.isForcedSuccessResponse =    (bool)utilitiesHelper.ReadBooleanNullable(item.Value)!; break;
                                case "hasException":                bankAccountAVSRResult.hasException =               (bool)utilitiesHelper.ReadBooleanNullable(item.Value)!; break;
                                default: DocumentTemplate.DisplayBody($"Unkown Property: {item.Name}"); break;

                            }
                        }
                        validateBankAccountQAVSRResponse.avsrResult = bankAccountAVSRResult;
                        break;
                    case "correctBankAccountType":
                        foreach (var item in property.Value.EnumerateObject())
                        {
                            switch (item.Name)
                            {
                                case "type": validateBankAccountQAVSRResponse.correctBankAccountType.type = (int)utilitiesHelper.ReadInt32Nullable(item.Value)!; break;
                            }
                        }
                        break;
                    default: DocumentTemplate.DisplayBody($"Unkown Property: {property.Name}"); break;
                }
            }
            return validateBankAccountQAVSRResponse;
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
