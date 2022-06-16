using System;
using Newtonsoft.Json;

namespace DataCollection.Converters
{
    // public class DateTimeConverter : JsonConverter<DateTime>
    // {
    //     public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    //     {
    //         string data = reader.GetString();
    //         if (data is null)
    //         {
    //             throw new Exception("nulled datetime given for non-nullable datetime field");
    //         }
    //         return DateTime.Parse(data);
    //     }
    //
    //     public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    //     {
    //
    //             writer.WriteStringValue(value.ToUniversalTime().ToString("O"));
    //     }
    // }
}