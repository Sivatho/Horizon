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

        /// <summary>
        /// Loads and deserializes JSON test data into a strongly typed collection.
        /// 
        /// <para><b>Description:</b></para>
        /// Reads a JSON file from the specified test data folder, deserializes the content
        /// into a list of objects of type <typeparamref name="T"/>, and returns the result.
        /// This helper is typically used in automated testing to supply structured test data.
        /// 
        /// <para><b>Benefits:</b></para>
        /// - Simplifies loading and deserializing JSON-based test data  
        /// - Ensures case-insensitive property mapping for resilience  
        /// - Avoids null reference issues by always returning an initialized list  
        /// - Allows reusable, type-safe data loading across multiple test scenarios  
        /// </summary>
        /// <typeparam name="T">The object type to deserialize each JSON item into.</typeparam>
        /// <param name="parentFolderName">The name of the parent folder where the JSON test file is stored.</param>
        /// <param name="fileName">The JSON file name including extension.</param>
        /// <returns>A collection of deserialized objects of type <typeparamref name="T"/>.</returns>
        public IEnumerable<T> ReadJsonTestDataList<T>(string parentFolderName, string fileName) where T : class
        {
            string jsonString = utilitiesHelper.ReadTestDataJson(parentFolderName, fileName);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<List<T>>(jsonString, options) ?? new List<T>();
        }
        /// <summary>
        /// Description:
        /// Benefits
        /// </summary>
        /// <param name="parentFolderName"></param>
        /// <param name="fileNameAndExt"></param>
        /// <returns></returns>
        public IEnumerable<TestCaseData> ReadJsonTestDataFields(string parentFolderName, string fileNameAndExt)
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
