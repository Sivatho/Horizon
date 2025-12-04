using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.Bank
{
    public abstract class AbstractValidationMethods
    {
        abstract public void ValidateResponseContentsIsValid_AndDataTypesIsValid(RestResponse restResponse);
        abstract public void ValidateResponseSchemaIsValid(RestResponse restResponse, string folder, string jsonfile);
    }
}
