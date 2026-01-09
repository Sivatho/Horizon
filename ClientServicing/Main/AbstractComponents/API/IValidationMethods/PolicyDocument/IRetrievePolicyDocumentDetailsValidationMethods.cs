using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Models.PolicyDocument;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.PolicyDocument
{
    public interface IRetrievePolicyDocumentDetailsValidationMethods
    {
        public void ValidateRetrievePolicyDocumentDetailsRequestIsNotNullOrEmptyOrLessThanZero(CheckPolicyDocumentExistRequest checkPolicyDocumentExistRequest);
        public void ValidateRetrievePolicyDocumentDetailsResponseIsNotNullOrEmptyOrLessThanZero(RetrievePolicyDocumentDetailsResponse retrievePolicyDocumentDetailsResponse);
    }
}
