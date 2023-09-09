using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BackendStressTest.Api.Configurations
{
    public class DateOnlyConfiguration : JsonConverter<DateOnly>
    {
        private const string format = "yyyy-MM-dd";

        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions _) =>
            DateOnly.TryParseExact(reader.GetString(), format, out var date) ? date : default;

        public override void Write(Utf8JsonWriter writer, DateOnly date, JsonSerializerOptions _) =>
            writer.WriteStringValue(date.ToString(format, CultureInfo.InvariantCulture));
    }
}
