using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.JsonValidation;
using Newtonsoft.Json.Schema;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.JsonValidation
{
    #region 1) Core Schema Model (Reusable) 
    public interface IJsonRule
    {
        string PropertyName { get; }
        bool Required { get; }
        ISet<JsonValueKind> AllowedKinds { get; }
        IJsonSchema? NestedSchema { get; }
    }
    public interface IJsonSchema
    {
        IReadOnlyList<IJsonRule> Rules { get; }
    }
    #endregion

    #region 2) Validator + Result (Separation of Concerns)
    public interface IJsonValidator
    {
        public ValidationResult Validate(JsonElement element, IJsonSchema schema, string path = "$");
    }
    #endregion
}
