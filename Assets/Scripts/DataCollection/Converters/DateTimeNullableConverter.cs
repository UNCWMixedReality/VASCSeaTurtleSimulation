using System;
// using System.Text.Json;
// using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace DataCollection.Converters
{
    public class DateTimeNullableConverter : JsonConverter
    {
        // public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        // {
        //     string data = reader.GetString();
        //     if (data is null)
        //     {
        //         return null;
        //     }
        //     return DateTime.Parse(data);
        // }
        //
        // public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        // {
        //     if (value is null)
        //     {
        //         writer.WriteStringValue("null");
        //     }
        //     else
        //     {
        //         writer.WriteStringValue(value?.ToUniversalTime().ToString("O"));
        //     }
        // }

        // public override void WriteJson(JsonWriter writer, DateTime? value, JsonSerializer serializer)
        // {
        //     
        // }

        // public override DateTime? ReadJson(JsonReader reader, Type objectType, DateTime? existingValue, bool hasExistingValue,
        //     JsonSerializer serializer)
        // {
        //     string data = reader.ReadAsString();
        //     if (data is null || data == "null")
        //     {
        //         return null;
        //     }
        //     return DateTime.Parse(data);
        // }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string data = reader.ReadAsString();
            if (data is null || data == "null")
            {
                return null;
            }
            return DateTime.Parse(data);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime) || objectType == typeof(DateTime?);
        }
    }
}