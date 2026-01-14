using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FSharp.Json;

namespace ClientServicing.Main.Resources.Helper
{
    public class JsonDataLoader
    {
        UtilitiesHelper utilitiesHelper = new UtilitiesHelper();
        public  IEnumerable<T> LoadJsonDataObjects<T>(string parentFolderName, string fileNameAndExt) where T : class
        {
            string jsonString = utilitiesHelper.ReadTestDataJson(parentFolderName, fileNameAndExt);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<List<T>>(jsonString, options) ?? new List<T>();
        }
        public IEnumerable<TestCaseData> LoadJsonDataFields(string parentFolderName, string fileNameAndExt)
        {
            string jsonString = utilitiesHelper.ReadTestDataJson(parentFolderName, fileNameAndExt);

            var dict = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonString)!;

            foreach (var kvp in dict)
            {   
                yield return new TestCaseData(kvp.Key, kvp.Value)
                    .SetName($"Given_RequestPayload{kvp.Key}Is{kvp.Value}_Then_ValidateFetchBankResponseIsOk_And_PropertyNameIsValid_And_DataTypesIsValid_And_IsNotNullOrEmpty_And_SchemaIsValid");
            }
        }
    }
}
