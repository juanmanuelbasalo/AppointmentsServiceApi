using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.Helpers
{
    public static class JsonHelper
    {
        public static T LoadJsonFromFile<T>(this string filePath)
        {
            using (var streamReader = new StreamReader(filePath))
            {
                var file = streamReader.ReadToEnd();
                T jsonObject = JsonConvert.DeserializeObject<T>(file);
                return jsonObject;
            }
        }

        public static T Deserialize<T>(this string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}
