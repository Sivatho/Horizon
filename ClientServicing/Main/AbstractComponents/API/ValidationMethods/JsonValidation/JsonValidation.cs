using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.JsonValidation;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.JsonValidation
{
    #region 1) Core Schema Model(Reusable)
    /**/
    public sealed class JsonRule : IJsonRule
    {
        public string PropertyName { get; }
        public bool Required { get; }
        public ISet<JsonValueKind> AllowedKinds { get; }
        public IJsonSchema? NestedSchema { get; }
        public JsonRule(
            string propertyName,
            IEnumerable<JsonValueKind> allowedKinds,
            bool required = true,
            IJsonSchema? nestedSchema = null)
        {
            PropertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName));
            AllowedKinds = new HashSet<JsonValueKind>(allowedKinds ?? throw new ArgumentNullException(nameof(allowedKinds)));
            Required = required;
            NestedSchema = nestedSchema;
        }
    }
    //Code change
    /*public record JsonRule(
        string PropertyName,
        bool Required,
        ISet<JsonValueKind> AllowedKinds,
        IJsonSchema? NestedSchema
    ) : IJsonRule;*/
    public sealed class JsonSchema : IJsonSchema
    {
        private readonly List<IJsonRule> _rules = new();
        public IReadOnlyList<IJsonRule> Rules => _rules;
        public JsonSchema(IEnumerable<IJsonRule> rules)
        {
            if (rules == null) throw new ArgumentNullException(nameof(rules));
            _rules.AddRange(rules);
        }
        public sealed class Builder
        {
            private readonly List<IJsonRule> _rules = new();
            public Builder Property(
                string name,
                IEnumerable<JsonValueKind> kinds,
                IJsonSchema? nested = null,
                bool required = true)
            {
                _rules.Add(new JsonRule(name, kinds, required, nested));
                return this;
            }
            public Builder OptionalProperty(
                string name,
                IEnumerable<JsonValueKind> kinds,
                IJsonSchema? nested = null)
                => Property(name, kinds, nested, required: false);
            public JsonSchema Build() => new JsonSchema(_rules);
        }
    }
    public static class JsonKinds
    {
        public static readonly ISet<JsonValueKind> Boolean = new HashSet<JsonValueKind>
        {
            JsonValueKind.True, JsonValueKind.False
        };
        public static ISet<JsonValueKind> Of(params JsonValueKind[] kinds)
                => new HashSet<JsonValueKind>(kinds ?? Array.Empty<JsonValueKind>());
    }
    //Code change
    public sealed class PrimitiveSchema : IJsonSchema, IRootConstrained
    {
        public PrimitiveSchema(ISet<JsonSchema> kinds, ISet<string>? enumStrings = null)
        {

            AllowedRootKinds = (ISet<JsonValueKind>?)(kinds ?? throw new ArgumentNullException(nameof(kinds)));
            AllowedRootStringEnum = enumStrings;
            Rules = Array.Empty<IJsonRule>();
        }
        public IReadOnlyList<IJsonRule> Rules { get; }
        public ISet<JsonValueKind> AllowedRootKinds { get; }
        public ISet<string>? AllowedRootStringEnum { get; }
    }
    //Code change
    public sealed class ObjectSchema : IJsonSchema, IRootConstrained
    {
        public ObjectSchema(IEnumerable<IJsonRule> rules)
        {

            Rules = rules?.ToList() ?? new List<IJsonRule>();
            AllowedRootKinds = new HashSet<JsonValueKind> {
                JsonValueKind.Object
            };
        }
        public IReadOnlyList<IJsonRule> Rules { get; }
        public ISet<JsonValueKind> AllowedRootKinds { get; }
        public ISet<string>? AllowedRootStringEnum { get; } = null;
    }
    #endregion

    #region 2) Validator + Result (Separation of Concerns)
    public sealed class ValidationFailure
    {
        public string Path { get; }
        public string Message { get; }
        public ValidationFailure(string path, string message)
        {
            Path = path;
            Message = message;
        }
        public override string ToString() => $"{Path}: {Message}";
    }
    public sealed class ValidationResult
    {
        public bool IsValid => Failures.Count == 0;
        public IReadOnlyList<ValidationFailure> Failures { get; }

        public ValidationResult(IEnumerable<ValidationFailure> failures)
        {
            Failures = failures?.ToList() ?? new List<ValidationFailure>();
        }
        public string ToFailureMessage()
        {
            if (IsValid) return "Validation succeeded.";
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("JSON validation failed:");
            foreach (var f in Failures) sb.AppendLine(" - " + f);
            return sb.ToString();
        }
    }
    public sealed class JsonValidator : IJsonValidator
    {
        public ValidationResult Validate(JsonElement element, IJsonSchema schema, string path = "$")
        {
            var failures = new List<ValidationFailure>();

            // ─────────────────────────────────────────────
            // 1) ROOT-LEVEL VALIDATION (NEW)
            // ─────────────────────────────────────────────
            if (schema is IRootConstrained root)
            {
                // Check allowed root kinds (String, True, False, Object)
                if (!root.AllowedRootKinds.Contains(element.ValueKind))
                {
                    var expected = string.Join(", ", root.AllowedRootKinds);
                    failures.Add(new ValidationFailure(
                        path,
                        $"Unexpected root kind '{element.ValueKind}'. Expected: {expected}"
                    ));
                    return new ValidationResult(failures);
                }

                // If the root is a STRING, enforce allowed enum values if specified
                if (element.ValueKind == JsonValueKind.String &&
                    root.AllowedRootStringEnum is { Count: > 0 })
                {
                    var actual = element.GetString();
                    if (!root.AllowedRootStringEnum.Contains(actual!))
                    {
                        var expectedVals = string.Join(", ", root.AllowedRootStringEnum);
                        failures.Add(new ValidationFailure(
                            path,
                            $"Unexpected root string '{actual}'. Expected: {expectedVals}"
                        ));
                        return new ValidationResult(failures);
                    }
                }

                // IMPORTANT:
                // If the root is NOT an object,
                // STOP HERE — DO NOT try to read properties.
                if (element.ValueKind != JsonValueKind.Object)
                {
                    return new ValidationResult(failures);
                }
            }
            else
            {
                // If schema has rules but element isn't an object → fail early
                if (schema.Rules.Count > 0 && element.ValueKind != JsonValueKind.Object)
                {
                    failures.Add(new ValidationFailure(
                        path,
                        $"Expected root object to validate properties, but got '{element.ValueKind}'."
                    ));
                    return new ValidationResult(failures);
                }
            }

            // ─────────────────────────────────────────────
            // 2) OBJECT PROPERTY VALIDATION (UNCHANGED)
            // ─────────────────────────────────────────────
            foreach (var rule in schema.Rules)
            {
                var propPath = $"{path}.{rule.PropertyName}";

                if (!element.TryGetProperty(rule.PropertyName, out var prop))
                {
                    if (rule.Required)
                        failures.Add(new ValidationFailure(propPath, "Required property missing."));
                    continue;
                }

                if (!rule.AllowedKinds.Contains(prop.ValueKind))
                {
                    var expected = string.Join(", ", rule.AllowedKinds.OrderBy(k => k));
                    failures.Add(new ValidationFailure(
                        propPath,
                        $"Unexpected kind '{prop.ValueKind}'. Expected: {expected}"));
                    continue;
                }

                if (rule.NestedSchema != null)
                {
                    switch (prop.ValueKind)
                    {
                        case JsonValueKind.Object:
                            failures.AddRange(Validate(prop, rule.NestedSchema, propPath).Failures);
                            break;

                        case JsonValueKind.Array:
                            int idx = 0;
                            foreach (var item in prop.EnumerateArray())
                            {
                                var itemPath = $"{propPath}[{idx}]";
                                if (item.ValueKind != JsonValueKind.Object)
                                {
                                    failures.Add(new ValidationFailure(itemPath, "Array item not an object"));
                                }
                                else
                                {
                                    failures.AddRange(Validate(item, rule.NestedSchema, itemPath).Failures);
                                }
                                idx++;
                            }
                            break;

                        default:
                            failures.Add(new ValidationFailure(
                                propPath,
                                "Nested schema provided but value is not an object or array."));
                            break;
                    }
                }
            }

            return new ValidationResult(failures);
        }

        /*public ValidationResult Validate(JsonElement element, IJsonSchema schema, string path = "$")
        {
            var failures = new List<ValidationFailure>();
            // ---------- ROOT-LEVEL VALIDATION (optional) ----------
            if (schema is IRootConstrained root)
            {
                // Enf orce allowed kinds at the root (if provided)
                if (root.AllowedRootKinds is { Count: > 0 } &&
                    !root.AllowedRootKinds.Contains(element.ValueKind))
                {
                    var expected = string.Join(", ", root.AllowedRootKinds.OrderBy(k => k));
                    failures.Add(new ValidationFailure(
                        path,
                        $"Unexpected root kind '{element.ValueKind}'. Expected: {expected}"
                    ));
                    return new ValidationResult(failures); // cannot proceed if root kind is wrong
                }

                // If root is a STRING and we have an enum constraint, enforce it
                if (element.ValueKind == JsonValueKind.String &&
                    root.AllowedRootStringEnum is { Count: > 0 })
                {
                    var value = element.GetString();
                    if (!root.AllowedRootStringEnum.Contains(value!))
                    {
                        var expectedVals = string.Join(", ", root.AllowedRootStringEnum.OrderBy(s => s));
                        failures.Add(new ValidationFailure(
                            path,
                            $"Unexpected root string value '{value}'. Expected one of: {expectedVals}"
                        ));
                        return new ValidationResult(failures);
                    }
                }

                // Short-circuit: if the root is NOT an object, there are no properties to validate.
                if (element.ValueKind != JsonValueKind.Object)
                    return new ValidationResult(failures);
            }
            else
            {
                // If the schema has property rules but the element isn't an object, fail gracefully.
                if (schema.Rules.Count > 0 && element.ValueKind != JsonValueKind.Object)
                {
                    failures.Add(new ValidationFailure(
                        path,
                        $"Expected root kind 'Object' to validate properties, but was '{element.ValueKind}'."
                    ));
                    return new ValidationResult(failures);
                }
            }

            // ---------- OBJECT PROPERTY VALIDATION (existing logic) ----------
            foreach (var rule in schema.Rules)
            {
                var propPath = $"{path}.{rule.PropertyName}";
                if (!element.TryGetProperty(rule.PropertyName, out var prop))
                {
                    if (rule.Required)
                        failures.Add(new ValidationFailure(propPath, "Required property missing."));
                    continue;
                }

                if (!rule.AllowedKinds.Contains(prop.ValueKind))
                {
                    var expected = string.Join(", ", rule.AllowedKinds.OrderBy(k => k));
                    failures.Add(new ValidationFailure(
                        propPath,
                        $"Unexpected kind '{prop.ValueKind}'. Expected: {expected}"));
                    continue;
                }

                if (rule.NestedSchema != null)
                {
                    switch (prop.ValueKind)
                    {

                        case JsonValueKind.Object:
                            failures.AddRange(Validate(prop, rule.NestedSchema, propPath).Failures);
                            break;

                        case JsonValueKind.Array:
                            int idx = 0;
                            foreach (var item in prop.EnumerateArray())
                            {
                                var itemPath = $"{propPath}[{idx}]";
                                if (item.ValueKind != JsonValueKind.Object)
                                {
                                    failures.Add(new ValidationFailure(itemPath, "Array item not an object"));
                                }
                                else
                                {
                                    failures.AddRange(Validate(item, rule.NestedSchema, itemPath).Failures);
                                }
                                idx++;
                            }
                            break;
                        default:
                            failures.Add(new ValidationFailure(
                            propPath,
                            "Nested schema provided but value is not an object or array."));
                            break;

                    }
                }
            }
            return new ValidationResult(failures);
        }*/
    }
    #endregion

    #region 3) Envelope Schema Factory (Reusability)
    public static class ResponseSchemas
    {
        ///<summary>
        ///Method Name:StandardEnvelopeObject
        ///Description:
        ///Advantage:
        ///Disadvantage:
        ///</summary>
        public static JsonSchema StandardEnvelopeObject(Action<JsonSchema.Builder> dataRules)
        {
            if (dataRules == null) throw new ArgumentNullException(nameof(dataRules));

            var dataBuilder = new JsonSchema.Builder();
            dataRules(dataBuilder);
            var dataSchema = dataBuilder.Build();

            return new JsonSchema.Builder()
                .Property("succeeded", JsonKinds.Boolean)
                .Property("message", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("errors", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("data", JsonKinds.Of(JsonValueKind.Object), nested: dataSchema)
                .Build();
        }
        ///<summary>
        ///Method Name:StandardEnvelopeAny
        ///Description:
        ///Advantage:
        ///Disadvantage:
        ///</summary>
        public static JsonSchema StandardEnvelopeAny(Action<JsonSchema.Builder>? objectOrItemRules = null)
        {
            var nestedBuilder = new JsonSchema.Builder();
            objectOrItemRules?.Invoke(nestedBuilder);
            var nestedSchema = nestedBuilder.Build();

            return new JsonSchema.Builder()
            .Property("succeeded", JsonKinds.Boolean)
            .Property("message", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
            .Property("errors", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
            .Property("data", JsonKinds.Of(JsonValueKind.Object,
                                                JsonValueKind.Array,
                                                JsonValueKind.String,
                                                JsonValueKind.Number,
                                                JsonValueKind.True,
                                                JsonValueKind.False,
                                                JsonValueKind.Null), nested: nestedSchema)
            .Build();
        }
        ///<summary>
        ///Method Name: StandardEnvelopeArray
        ///Description:
        ///Advantage:
        ///Disadvantage:
        ///</summary>
        public static JsonSchema StandardEnvelopeArray(Action<JsonSchema.Builder> itemRules)
        {
            if (itemRules == null) throw new ArgumentNullException(nameof(itemRules));

            var itemBuilder = new JsonSchema.Builder();
            itemRules(itemBuilder);
            var itemSchema = itemBuilder.Build();

            // Assumption: nested: itemSchema is interpreted by your builder as the "items" schema for arrays.
            return new JsonSchema.Builder()
                .Property("succeeded", JsonKinds.Boolean)
                .Property("message", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("errors", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("data", JsonKinds.Of(JsonValueKind.Array), nested: itemSchema)
                .Build();
        }
        ///<summary>
        ///Method Name: StandardEnvelopePrimitive
        ///Description:
        ///Advantage:
        ///Disadvantage:
        ///</summary>
        public static JsonSchema StandardEnvelopePrimitive(params JsonValueKind[] primitiveKinds)
        {
            if (primitiveKinds == null || primitiveKinds.Length == 0)
                throw new ArgumentException("At least one primitive kind must be specified.", nameof(primitiveKinds));

            return new JsonSchema.Builder()
                .Property("succeeded", JsonKinds.Boolean)
                .Property("message", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("errors", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("data", JsonKinds.Of(primitiveKinds))
                .Build();
        }
        public static JsonSchema BoolPrimitiveEnvelope(params JsonValueKind[] primitiveKinds)
        {
            return new JsonSchema.Builder()
                .Property("enum", JsonKinds.Of(primitiveKinds))
                .Build();
        }
        public static JsonSchema StandardResponseDataBoolSchema()
        {
            return new JsonSchema.Builder()
                .Property("succeeded", JsonKinds.Boolean)
                .Property("message", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("errors", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("data", JsonKinds.Of(JsonValueKind.True, JsonValueKind.False))
            .Build();
        }

        public static JsonSchema StatusDataEnvelop(Action<JsonSchema.Builder>? objectOrItemRules = null)
        {
            var nestedBuilder = new JsonSchema.Builder();
            objectOrItemRules?.Invoke(nestedBuilder);
            var nestedSchema = nestedBuilder.Build();

            return new JsonSchema.Builder()
            .Property("success",  JsonKinds.Boolean)
            .Property("message",    JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
            .Property("result",       JsonKinds.Of(JsonValueKind.Object,JsonValueKind.Array), nested: nestedSchema)
            .Build();
        }
    }
    public static class PolicySchemas
    {
        ///<summary>
        ///Method Name:PolicyEnvelopeStrict
        ///Description:
        ///Advantage:
        ///Disadvantage:
        ///</summary>
        public static JsonSchema PolicyEnvelopeStrict => ResponseSchemas.StandardEnvelopeObject(data =>
        {
            data.Property("legacyPolicyNo", JsonKinds.Of(JsonValueKind.Null, JsonValueKind.String))
                .Property("policyNo", JsonKinds.Of(JsonValueKind.Null, JsonValueKind.Number))
                .Property("status", JsonKinds.Of(JsonValueKind.Null, JsonValueKind.String))
                .Property("statusCD", JsonKinds.Of(JsonValueKind.Null, JsonValueKind.Number))
                .Property("statusDate", JsonKinds.Of(JsonValueKind.Null, JsonValueKind.String));
        });
        ///<summary>
        ///Method Name:StandardEnvelopeObject
        ///Description:
        ///Advantage:
        ///Disadvantage:
        ///</summary>
        public static JsonSchema PolicyStatusBody()
        {
            return new JsonSchema.Builder()
                .Property("success", JsonKinds.Boolean)
                .Property("message", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("totalPolicies", JsonKinds.Of(JsonValueKind.Number))
                .Property("limitExceeded", JsonKinds.Boolean)
                .Build();
        }
    }
    #endregion

    #region 4) RestSharp Extension (Test Readability)
    public static class RestResponseExtensions
    {
        ///<summary>
        ///Method Name:
        ///Description:
        ///Advantage:
        ///Disadvantage:
        ///</summary>
        public static void ShouldMatchSchema(this RestResponse response, IJsonSchema schema)
        {
            bool isValid = false;
            var result = ValidateAgainst(response, schema);
            if (!result.IsValid)
            {
                throw new InvalidOperationException(result.ToFailureMessage());
            }
            isValid = result.IsValid;
            DocumentTemplate.DisplayFieldAndValue("Validated: Response Data Should Match Schema ", isValid.ToString());
        }
        ///<summary>
        ///Method Name:
        ///Description:
        ///Advantage:
        ///Disadvantage:
        ///</summary>
        public static void Data_Should_Accept_Valid_Names_And_Types(this RestResponse response, IJsonSchema schema)
        {
            bool isValid = false;

            var result = ValidateAgainst(response, schema);
            if (!result.IsValid)
            {
                throw new InvalidOperationException(result.ToFailureMessage());
            }
            isValid = result.IsValid;
            Assert.That(result.IsValid, Is.True, result.ToFailureMessage());
            DocumentTemplate.DisplayFieldAndValue("Validated: Response Data Should Accept Valid Names And Types ", isValid.ToString());
        }
        ///<summary>
        ///Method Name:
        ///Description:
        ///Advantage:
        ///Disadvantage:
        ///</summary>
        public static ValidationResult ValidateAgainst(this RestResponse response, IJsonSchema schema)
        {
            if (response == null) throw new ArgumentNullException(nameof(response));
            if (schema == null) throw new ArgumentNullException(nameof(schema));

            if (string.IsNullOrWhiteSpace(response.Content))
                return new ValidationResult(new[] { new ValidationFailure("$", "Response content is empty.") });

            using var doc = JsonDocument.Parse(response.Content!);
            var validator = new JsonValidator();
            return validator.Validate(doc.RootElement, schema);
        }
    }
    #endregion

    #region 5) Your Exact Schema (mirrors your original rules)
    #region Policy    
    public static class ResponseSchemasEnvelope
    {
        public static JsonSchema BooleanResponse = ResponseSchemas.BoolPrimitiveEnvelope(JsonValueKind.True, JsonValueKind.False);
        public static JsonSchema DataFalseSchema = ResponseSchemas.StandardEnvelopePrimitive(JsonValueKind.True, JsonValueKind.False);
        public static JsonSchema DataBooleanSchema = ResponseSchemas.StandardEnvelopePrimitive(JsonValueKind.True, JsonValueKind.False);
        public static JsonSchema DataNumberSchema = ResponseSchemas.StandardEnvelopePrimitive(JsonValueKind.Number);
        public static JsonSchema EligibilitySchema = ResponseSchemas.StandardEnvelopeObject(data =>
        {
            data.Property("isEligibile", JsonKinds.Boolean)
                .Property("message", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null));
        });
        public static JsonSchema BankSchema = ResponseSchemas.StandardEnvelopeObject(data =>
        {
            data.Property("bankID", JsonKinds.Of(JsonValueKind.Number))
                .Property("bankName", JsonKinds.Of(JsonValueKind.String))
                .Property("bankShortName", JsonKinds.Of(JsonValueKind.String))
                .Property("dispSeq", JsonKinds.Of(JsonValueKind.Number))
                .Property("isActive", JsonKinds.Boolean)
                .Property("lastChanged", JsonKinds.Of(JsonValueKind.String))
                .Property("userID", JsonKinds.Of(JsonValueKind.String));
        });
        public static JsonSchema PolicyBeneficiaryDetailsSchema = ResponseSchemas.StandardEnvelopeObject(data =>
        {
            data.Property("totalAllocated", JsonKinds.Of(JsonValueKind.Number))
                .Property("beneficiaryDetailsItems", JsonKinds.Of(JsonValueKind.Array), nested:
                    new JsonSchema.Builder()
                        .Property("rownumber", JsonKinds.Of(JsonValueKind.Number))
                        .Property("entityNo", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                        .Property("policyNo", JsonKinds.Of(JsonValueKind.Number))
                        .Property("firstName", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                        .Property("surname", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                        .Property("titleCd", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                        .Property("titleDescr", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                        .Property("statusCd", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                        .Property("statusDescr", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                        .Property("entityRelationId", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                        .Property("relationCd", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                        .Property("relationDescr", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                        .Property("legalReferenceNumber", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                        .Property("legalReferenceNumberMasked", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                        .Property("legalReferenceNumberTypeCd", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                        .Property("percAllocation", JsonKinds.Of(JsonValueKind.Number))
                        .Property("dateOfBirth", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                        .Property("status", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                        .Property("physicalAddress", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                        .Property("addressLine2", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                        .Property("suburb", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                        .Property("addressCity", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                        .Property("addressPostCode", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                        .Property("cellNumber", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                        .Property("cellNumberMasked", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                        .Property("homeNumber", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                        .Property("workNumber", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                        .Property("alternateNumber", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                        .Property("whatsappNumber", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                        .Property("emailAddress", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                        .Property("fullname", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                        .Property("totalPercentageAvailable", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                        .Property("role", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                        .Property("genderCd", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                        .Property("auditToken", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                        .Property("bankAccNo", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                        .Build()
                )
                .Property("policyNo", JsonKinds.Of(JsonValueKind.Number))
                .Property("auditToken", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null));
        });
        public static JsonSchema PolicyDetailsSchema = ResponseSchemas.StandardEnvelopeObject(data =>
        {
            data.Property("policy_NO", JsonKinds.Of(JsonValueKind.Number))
                .Property("entityNo", JsonKinds.Of(JsonValueKind.Number))
                .Property("legacy_Pol_No", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("annualIncrease", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                .Property("dateOfCommencement", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("reInstatedDate", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("lapsedDate", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("venue", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("salesPerson", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("campaignCode", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                .Property("policyFee", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                .Property("captureDate", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("preferedCommunicationMethod", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                .Property("masterContract", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("title", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("titleID", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                .Property("firstname", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("surname", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("legalRefNo", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("legalNumberType", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                .Property("dateOfBirth", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("preferredTelTypeCd", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                .Property("faxNumber", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("homeNumber", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("emailAddress", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("cellNumber", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("workNumber", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("alternateNumber", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("whatsappNumber", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("physicalAddress1", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("physicalAddress2", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("physicalSuburb", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("physicalTown", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("physicalPostalCode", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("postalAddress1", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("postalAddress2", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("postalSuburb", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("postalTown", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("postalPostalCode", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("genderCD", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                .Property("smokerCd", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                .Property("smokerDescr", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("lastBillingDate", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("lastPaidDate", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("nextBillingDate", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("policyPremiumAmount", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                .Property("premiumCount", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                .Property("paymentFrequency", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null));
        });
        public static JsonSchema BankListSchema = ResponseSchemas.StandardEnvelopeArray(item =>
        {
            item.Property("bankID", JsonKinds.Of(JsonValueKind.Number))
                .Property("bankName", JsonKinds.Of(JsonValueKind.String))
                .Property("bankShortName", JsonKinds.Of(JsonValueKind.String))
                .Property("dispSeq", JsonKinds.Of(JsonValueKind.Number))
                .Property("isActive", JsonKinds.Boolean)
                .Property("lastChanged", JsonKinds.Of(JsonValueKind.String))
                .Property("userID", JsonKinds.Of(JsonValueKind.String));
        });
        public static JsonSchema PolicyListSchema = ResponseSchemas.StandardEnvelopeArray(item =>
        {
            item.Property("entityID", JsonKinds.Of(JsonValueKind.Number))
                .Property("ifaNo", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("entityNo", JsonKinds.Of(JsonValueKind.Number))
                .Property("entityName", JsonKinds.Of(JsonValueKind.String))
                .Property("entitySurname", JsonKinds.Of(JsonValueKind.String))
                .Property("entityDOB", JsonKinds.Of(JsonValueKind.String))
                .Property("legalRefNo", JsonKinds.Of(JsonValueKind.String))
                .Property("legalRefNoType", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("citizenshipCD", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                .Property("alpha3Code", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("citizenship", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("emailAddress", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("cellphoneNumber", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("physicalAddress1", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("legacyPolicyNo", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("policyNo", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                .Property("roleCd", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                .Property("status", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("statusCD", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                .Property("planTypeDescr", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("statusDate", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("dateOfCommencement", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("premiumAmt", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                .Property("salesPerson", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("rewardStatus", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("debiCheckStatus", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("agency", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("payor", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("payorLegalReferenceNumber", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("payorCellphoneNumber", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("payorEmailAddress", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("beneficiaryName", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("paymentTypeCD", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                .Property("inspiratorNo", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("region", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("partnerCD", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                .Property("partnerCode", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("schemeCD", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                .Property("schemeDesc", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("planCD", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                .Property("planDesc", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("channelCD", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                .Property("channelDesc", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("entityFullname", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null));
        });
        public static JsonSchema BenefitCoversSchema = ResponseSchemas.StandardEnvelopeArray(item =>
        {
            item.Property("benefitID", JsonKinds.Of(JsonValueKind.Number))
                .Property("benefitCover", JsonKinds.Of(JsonValueKind.Number));
        });
        public static JsonSchema GetPolicyProductLineSChema = ResponseSchemas.StandardEnvelopeObject(data =>
        {
            data.Property("policyNo", JsonKinds.Of(JsonValueKind.Number))
            .Property("productLineCD", JsonKinds.Of(JsonValueKind.Number))
            .Property("productLineDescription", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
            .Build();
        });
        public static JsonSchema PayerDetailsEnvelopeSchema = ResponseSchemas.StandardEnvelopeObject(data =>
        {
            data.Property("payerEntityNo", JsonKinds.Of(JsonValueKind.Number))
                .Property("policyNumber", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("productCategory", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("productCategoryId", JsonKinds.Of(JsonValueKind.Number))
                .Property("title", JsonKinds.Of(JsonValueKind.String))
                .Property("titleCd", JsonKinds.Of(JsonValueKind.Number))
                .Property("firstName", JsonKinds.Of(JsonValueKind.String))
                .Property("surname", JsonKinds.Of(JsonValueKind.String))
                .Property("initials", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("emailAddress", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("isAMember", JsonKinds.Boolean)
                .Property("legalRefNo", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("legalRefNoTypeCD", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                .Property("dateOfBirth", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("genderCd", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                .Property("employerName", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("employeeNumber", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("employeeDepartmentCd", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                .Property("employeeDepartment", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("authorizationTypeCd", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                .Property("payroll", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("mandateType", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("agentID", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                .Property("isAuthorized", JsonKinds.Of(JsonValueKind.True, JsonValueKind.False, JsonValueKind.Null))
                .Property("homeNumber", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("cellNumber", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("workNumber", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("relationCd", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                .Property("bankId", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                .Property("bankName", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("bankShortName", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("branchNo", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                .Property("branchCode", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("bankAccNo", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("bankAccHolderInitial", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("bankAccHolderName", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("bankAccTypeCD", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                .Property("bankAccTypeDesc", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("bankAccountID", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                .Property("bankAccSwiftCode", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("isActive", JsonKinds.Of(JsonValueKind.True, JsonValueKind.False, JsonValueKind.Null))
                .Property("paymentTypeCD", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                .Property("paymentFreqCD", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                .Property("paymentRefId", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.String, JsonValueKind.Null))
                .Property("premium", JsonKinds.Of(JsonValueKind.Number))
                .Property("earlyTracking", JsonKinds.Boolean)
                .Property("debitDay", JsonKinds.Of(JsonValueKind.Number))
                .Property("firstDebitDay", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("firstDebitMonth", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("effectiveDate", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("gsd", JsonKinds.Of(JsonValueKind.Object), nested:
                    new JsonSchema.Builder()
                        .Property("deductionAuthorization", JsonKinds.Boolean)
                        .Property("payrollName", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                        .Property("payrollId", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                        .Property("departmentId", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                        .Property("departmentName", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                        .Property("employeeNumber", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                        .Property("mandateType", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                        .Build())
                .Property("lastChanged", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("userID", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("entityNo", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                .Property("agentCode", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("agentName", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("payAtNumber", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("csdEmployeeNo", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("csdCompanyDepartment", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("csdCompanyName", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("csdCompanyCd", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null));
        });
        public static JsonSchema RemovalFromBillingHistorySchema = ResponseSchemas.StandardEnvelopeArray(item =>
        {
            item.Property("removeID", JsonKinds.Of(JsonValueKind.Number))
                .Property("policyNo", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                .Property("removeCD", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                .Property("removalDate", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("premiumAmt", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                .Property("effDate", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("endDate", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("months", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("statusCD", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                .Property("s_Desc", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("comments", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("audModUser", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null));
        });
        public static JsonSchema UpdateCancelPolicyDetailsSchema = ResponseSchemas.StandardResponseDataBoolSchema();
        public static JsonSchema triggerEventSchema = ResponseSchemas.StandardEnvelopeObject(data => {
            data.Property("token", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
            .Property("success", JsonKinds.Of(JsonValueKind.True, JsonValueKind.False))
            .Property("message", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
            .Build();
        });
        public static JsonSchema getEventDetailConstructBPESchema = ResponseSchemas.StandardEnvelopeObject(data => {
            data.Property("jsonData", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
            .Property("success", JsonKinds.Of(JsonValueKind.True, JsonValueKind.False))
            .Property("message", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
            .Build();
        });
        public static JsonSchema checkStatusResponseSchema = ResponseSchemas.StatusDataEnvelop(data => {
            var dataProperties = new JsonSchema.Builder()
                 .Property("amount", JsonKinds.Of(JsonValueKind.Number))
                 .Property("ifaBusinessFeeIncluded", JsonKinds.Of(JsonValueKind.True, JsonValueKind.False))
                 .Property("success", JsonKinds.Of(JsonValueKind.True, JsonValueKind.False))
                 .Property("message", JsonKinds.Of(JsonValueKind.String))
                 .Property("status", JsonKinds.Of(JsonValueKind.String))
                 .Property("payerIdentityNumber", JsonKinds.Of(JsonValueKind.String))
                 .Property("payerMobileTelephoneNumber", JsonKinds.Of(JsonValueKind.String))
                 .Property("policyNumber", JsonKinds.Of(JsonValueKind.String))
                 .Property("createdAt", JsonKinds.Of(JsonValueKind.String))
                 .Property("mandateType", JsonKinds.Of(JsonValueKind.String))
                 .Build();
            data.Property("success", JsonKinds.Of(JsonValueKind.True, JsonValueKind.False))
            .Property("message", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
            .Property("data", JsonKinds.Of(JsonValueKind.Object, JsonValueKind.Array),nested: dataProperties)
            .Build();
        });
        
        ///<summary>
        ///Method Name: 
        ///Description:
        ///Advantage:
        ///Disadvantage:
        ///</summary>
        public static JsonSchema PolicySchema => ResponseSchemas.StandardEnvelopeObject(data =>
        {
            data.Property("legacyPolicyNo", JsonKinds.Of(JsonValueKind.Null, JsonValueKind.String))
                .Property("policyNo", JsonKinds.Of(JsonValueKind.Null, JsonValueKind.Number))
                .Property("status", JsonKinds.Of(JsonValueKind.Null, JsonValueKind.String))
                .Property("statusCD", JsonKinds.Of(JsonValueKind.Null, JsonValueKind.Number))
                .Property("statusDate", JsonKinds.Of(JsonValueKind.Null, JsonValueKind.String));
        });
        ///<summary>
        ///Method Name: 
        ///Description:
        ///Advantage:
        ///Disadvantage:
        ///</summary>
        public static JsonSchema AvsValidationBody()
        {
            return new JsonSchema.Builder()
                .Property("isValid", JsonKinds.Boolean)
                .Property("shouldUpdateAccountType", JsonKinds.Boolean)
                .Property("overrideIsValid", JsonKinds.Boolean)
                .Property("message", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("fraudsterFailure", JsonKinds.Of(JsonValueKind.Number))
                .Property("correctBankName", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("softyCompResult", JsonKinds.Of(JsonValueKind.Object))
                .Property("fraudsterResult", JsonKinds.Of(JsonValueKind.Object))
                .Property("d3BlackListResult", JsonKinds.Of(JsonValueKind.Object))
                .Property("avsrResult", JsonKinds.Of(JsonValueKind.Object))
                .Build();
        }
        public static JsonSchema GetUnmetPremiumResponseSchema()
        {

            var totalUnmetPremiumResultItem = new JsonSchema.Builder()
                .Property("numberOfMonths", JsonKinds.Of(JsonValueKind.Number))
                .Property("totalAmountMissed", JsonKinds.Of(JsonValueKind.Number))
                .Property("description", JsonKinds.Of(JsonValueKind.String))
                .Build();
            var unmetPremiumSummaryItem = new JsonSchema.Builder()
                .Property("policyNo", JsonKinds.Of(JsonValueKind.Number))
                .Property("legacy_Pol_No", JsonKinds.Of(JsonValueKind.String))
                .Property("month", JsonKinds.Of(JsonValueKind.Number))
                .Property("paymentDate", JsonKinds.Of(JsonValueKind.String))
                .Property("trackingDays", JsonKinds.Of(JsonValueKind.Number))
                .Property("paymentType", JsonKinds.Of(JsonValueKind.String))
                .Property("description", JsonKinds.Of(JsonValueKind.String))
                .Property("premiumAmount", JsonKinds.Of(JsonValueKind.Number))
                .Property("amountPaid", JsonKinds.Of(JsonValueKind.Number))
                .Build();
            return ResponseSchemas.StandardEnvelopeAny(data =>
            {
                data.Property("totalUnmetPremiumResult", JsonKinds.Of(JsonValueKind.Array), nested: totalUnmetPremiumResultItem)
                    .Property("unmetPremiumSummary", JsonKinds.Of(JsonValueKind.Array), nested: unmetPremiumSummaryItem);
            });
        }
        public static JsonSchema AffordabilityEnquirySchema()
        {
            return new JsonSchema.Builder()
                .Property("isValid", JsonKinds.Boolean)
                .Property("errorMessage", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("createdTimestamp", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("requestId", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("identityNumber", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("amount", JsonKinds.Of(JsonValueKind.Number))
                .Property("initials", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("surname", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("errorCodeId", JsonKinds.Of(JsonValueKind.Number))
                .Property("errorCode", JsonKinds.Of(JsonValueKind.Number))
                .Property("correlationId", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Property("employeeNumberHash", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                .Build();
        }
        public static JsonSchema policyAccountHistoryResponseSchema =
            ResponseSchemas.StandardEnvelopeObject(data =>
            {
                // accountingHistoryPaymentResults
                data.Property(
                    "accountingHistoryPaymentResults",
                    JsonKinds.Of(JsonValueKind.Object),
                    new JsonSchema.Builder()
                        .Property("totalNumberOfPayments", JsonKinds.Of(JsonValueKind.Number))
                        .Property("totalAmountReceived", JsonKinds.Of(JsonValueKind.Number))
                        .Property("totalAmountOutstanding", JsonKinds.Of(JsonValueKind.Number))
                        .Property("collectionMethod", JsonKinds.Of(JsonValueKind.String))
                        .Property("mandateType", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                        .Property("gsdType", JsonKinds.Of(JsonValueKind.Number))
                        .Property("suspenseAmt", JsonKinds.Of(JsonValueKind.Number))
                        .Build()
                );

                // accountingHistoryPolicyResults (array of objects)
                data.Property(
                    "accountingHistoryPolicyResults",
                    JsonKinds.Of(JsonValueKind.Array),
                    new JsonSchema.Builder()
                        .Property("policyNo", JsonKinds.Of(JsonValueKind.Number))
                        .Property("legacy_Pol_No", JsonKinds.Of(JsonValueKind.String))
                        .Property("referenceNO", JsonKinds.Of(JsonValueKind.String))
                        .Property("month", JsonKinds.Of(JsonValueKind.Number))
                        .Property("raisedDate", JsonKinds.Of(JsonValueKind.String))
                        .Property("bankSubmissionDate", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                        .Property("strikeDate", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                        .Property("paymentDate", JsonKinds.Of(JsonValueKind.String))
                        .Property("trackingDays", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                        .Property("mandateType", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                        .Property("paymentType", JsonKinds.Of(JsonValueKind.String))
                        .Property("description", JsonKinds.Of(JsonValueKind.String))
                        .Property("premiumAmount", JsonKinds.Of(JsonValueKind.Number))
                        .Property("amountPaid", JsonKinds.Of(JsonValueKind.Number))
                        .Build()
                );
            });
        public static JsonSchema policyAccountHistorySummaryResponseSchema =
            ResponseSchemas.StandardEnvelopeArray(item =>
            {
                item
                    .Property("policyNo", JsonKinds.Of(JsonValueKind.Number))
                    .Property("legacy_Pol_No", JsonKinds.Of(JsonValueKind.String))
                    .Property("referenceNO", JsonKinds.Of(JsonValueKind.String))
                    .Property("month", JsonKinds.Of(JsonValueKind.Number))
                    .Property("raisedDate", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                    .Property("bankSubmissionDate", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                    .Property("strikeDate", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                    .Property("paymentDate", JsonKinds.Of(JsonValueKind.String))
                    .Property("trackingDays", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null))
                    .Property("mandateType", JsonKinds.Of(JsonValueKind.String, JsonValueKind.Null))
                    .Property("paymentType", JsonKinds.Of(JsonValueKind.String))
                    .Property("description", JsonKinds.Of(JsonValueKind.String))
                    .Property("premiumAmount", JsonKinds.Of(JsonValueKind.Number))
                    .Property("amountPaid", JsonKinds.Of(JsonValueKind.Number));
            });

        public static JsonSchema PolicyCashReceiptResponseSchema =
            ResponseSchemas.StandardEnvelopeArray(item =>
            {
                item
                    .Property("policyNo", JsonKinds.Of(JsonValueKind.Number))  // Integer if supported
                    .Property("reference", JsonKinds.Of(JsonValueKind.String))
                    .Property("billingPeriod", JsonKinds.Of(JsonValueKind.Number))  // Integer if supported
                    .Property("raisedDate", JsonKinds.Of(JsonValueKind.String))  // ISO 8601
                    .Property("mandateType", JsonKinds.Of(JsonValueKind.String))
                    .Property("description", JsonKinds.Of(JsonValueKind.String))
                    .Property("premium", JsonKinds.Of(JsonValueKind.Number))
                    .Property("susTransTotal", JsonKinds.Of(JsonValueKind.Number, JsonValueKind.Null));
            });
        #endregion
        #endregion

        #region 6) Replace Your Method With This (Clean & Single Line)
        public class JsonValidation
    {
        public void ValidateResponsePropertyNameIsValidAndDataTypesIsValid(RestResponse restResponse)
        {
            restResponse.ShouldMatchSchema(ResponseSchemasEnvelope.PolicySchema);
        }
    }
    #endregion
}
}