using qa_dotnet_cucumber.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace qa_dotnet_cucumber.Helpers
{
    public static class JsonDataReader
    {
        /*Give me a key, and I will give you back one EducationData object.*/
        public static EducationData GetEducationData(string dataKey)
        {
            //find the json file
            string filePath = Path.Combine(
                Directory.GetCurrentDirectory(),"TestData","EducationTestData.json");

            //If the file does not exist.
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(
                    $"Education test data file was not found: {filePath}");
            }

            //Read the JSON file
            string json = File.ReadAllText(filePath);

            //create json settings
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            //Read this JSON and convert each named dataset into an EducationData object
            var educationData =
                JsonSerializer.Deserialize<Dictionary<string, EducationData>>(json,options);

            //Find the dataset whose name matches the key I asked for, 
            //If the dictionary is null OR the requested key cannot be found, throw an error.
            if (educationData == null ||
                !educationData.TryGetValue(dataKey, out EducationData? data))
            {
                throw new KeyNotFoundException(
                    $"Education test data key '{dataKey}' was not found.");
            }

            return data;
        }
    }
}
