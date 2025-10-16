using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InvoiceEF.Crud.CrossCutting
{
    public static class JsonSerializerExtensions
    {
        public static JsonSerializerOptions Options { get; set; }

        static JsonSerializerExtensions()
        {
            Options = new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                PropertyNameCaseInsensitive = true,
                MaxDepth = 10,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            };
            Options.Converters.Add(new JsonStringEnumConverter());
        }

        public static void AddCustomJsonOptions(JsonSerializerOptions newOptions)
        {
            Options = newOptions;
        }

        public static string Serialize<T>(T? data)
        {
            return JsonSerializer.Serialize(data, Options);
        }

        public static T? Deserialize<T>(this Stream stream)
        {
            return JsonSerializer.Deserialize<T>(stream, Options);
        }

        public static T? Deserialize<T>(this string json)
        {
            return JsonSerializer.Deserialize<T>(json, Options);
        }

        public async static Task<T?> DeserializeAsync<T>(this Stream stream)
        {
            return await JsonSerializer.DeserializeAsync<T>(stream, Options);
        }

    }
}
