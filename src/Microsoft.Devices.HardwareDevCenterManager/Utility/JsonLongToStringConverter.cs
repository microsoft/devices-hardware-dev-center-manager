using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Microsoft.Devices.HardwareDevCenterManager.Utility;

public class LongToStringJsonConverter : JsonConverter<string>
{

    public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Number)
        {
            long stringValue = reader.GetInt64();
            return stringValue.ToString(CultureInfo.InvariantCulture);
        }
        else if (reader.TokenType == JsonTokenType.String)
        {
            return reader.GetString();
        }

        throw new System.Text.Json.JsonException();
    }

    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value);
    }
}
