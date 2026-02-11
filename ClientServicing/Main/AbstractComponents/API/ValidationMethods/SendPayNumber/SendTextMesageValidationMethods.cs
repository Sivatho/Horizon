 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.SendPayAtNumber;
using ClientServicing.Main.IController;
using ClientServicing.Main.Models.SendPayAtNumber;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.SendPayNumber
{
    public class SendTextMesageValidationMethods : AbstractValidationMethods, ISendTextMessageValidationMethods
    {
        public override void ValidateResponseFieldParametersIsValid(RestResponse restResponse)
        {
            throw new NotImplementedException();
        }

        public override void ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(RestResponse restResponse)
        {
            throw new NotImplementedException();
        }

        public void ValidateSendTextMessageRequestIsNotNullOrEmpty(SendTextMessageRequest sendTextMessageRequest)
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(sendTextMessageRequest,                         Is.Not.Null.Or.Empty, "RendTextMessageRequest Should Not Be NUll or Empty");
                Assert.That(sendTextMessageRequest.mobileTelephoneNumber,   Is.Not.Null.Or.Empty, "MobileTelephoneNumber Should Not Be NUll or Empty");
                Assert.That(sendTextMessageRequest.subAffiliateCode,        Is.Not.Null.Or.Empty, "subAffiliateCode Should Not Be NUll or Empty");
                Assert.That(sendTextMessageRequest.textMessageContent,      Is.Not.Null.Or.Empty, "TextMessageContent Should Not Be NUll or Empty");
            }
            DocumentTemplate.DisplayBody("Validated: SendTextMessageRequest : Is Not Null Or Empty");
        }
    }
}
