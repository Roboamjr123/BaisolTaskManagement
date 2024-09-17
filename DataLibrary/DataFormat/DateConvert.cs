
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataLibrary.DataFormat
{
    public class DateConvert : JsonConverter<DateTime>
    {
        private readonly string[] formats = new[] { "MM/dd/yyyy", "yyyy-MM-ddTHH:mm:ss.fffZ", "yyyy-MM-ddTHH:mm:ssZ" };

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string dateString = reader.GetString();

            // Try to parse the date with multiple formats
            if (DateTime.TryParseExact(dateString, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
            {
                return date;
            }

            throw new JsonException($"Unable to parse '{dateString}' as a valid date.");
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            // Write the date in the desired format
            writer.WriteStringValue(value.ToString("MM/dd/yyyy"));
        }
    }
}
