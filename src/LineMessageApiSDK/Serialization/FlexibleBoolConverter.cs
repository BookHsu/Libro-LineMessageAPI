using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LineMessageApiSDK.Serialization
{
    /// <summary>
    /// Allows boolean values to be read from string or number tokens.
    /// </summary>
    internal sealed class FlexibleBoolConverter : JsonConverter<bool>
    {
        public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.True:
                    return true;
                case JsonTokenType.False:
                    return false;
                case JsonTokenType.Number:
                    if (reader.TryGetInt64(out var number))
                    {
                        return number != 0;
                    }
                    break;
                case JsonTokenType.String:
                    var text = (reader.GetString() ?? string.Empty).Trim();
                    if (bool.TryParse(text, out var parsed))
                    {
                        return parsed;
                    }

                    switch (text.ToLowerInvariant())
                    {
                        case "1":
                        case "yes":
                        case "y":
                        case "on":
                        case "enabled":
                        case "auto":
                            return true;
                        case "0":
                        case "no":
                        case "n":
                        case "off":
                        case "disabled":
                        case "manual":
                            return false;
                    }
                    break;
            }

            // Fallback to false for unknown string formats to avoid breaking deserialization.
            return false;
        }

        public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
        {
            writer.WriteBooleanValue(value);
        }
    }
}
