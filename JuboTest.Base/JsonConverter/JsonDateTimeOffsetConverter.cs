using System.Text.Json;
using System.Text.Json.Serialization;

namespace JuboTest
{
    public class JsonDateTimeOffsetConverter
    {
        public class Default : JsonConverter<DateTimeOffset>
        {
            public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                return DateTimeOffset.Parse(reader.GetString());
            }

            public override void Write(Utf8JsonWriter writer, DateTimeOffset dateTimeValue, JsonSerializerOptions options)
            {
                writer.WriteStringValue(dateTimeValue.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK"));
            }
        }

        public class Nullable : JsonConverter<DateTimeOffset?>
        {
            public override DateTimeOffset? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                string value = reader.GetString();

                return value.IsNullOrEmpty() ? default : DateTimeOffset.Parse(value);
            }

            public override void Write(Utf8JsonWriter writer, DateTimeOffset? dateTimeValue, JsonSerializerOptions options)
            {
                writer.WriteStringValue(dateTimeValue.HasValue ? dateTimeValue.Value.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK") : string.Empty);
            }
        }
    }
}