using System.Text.Json;
using System.Text.Json.Serialization;

namespace Libro.LineMessageApi.Serialization
{
    /// <summary>System.Text.Json 序列化實作。</summary>
    public class SystemTextJsonSerializer : IJsonSerializer
    {
        private static readonly JsonSerializerOptions SerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            Converters =
            {
                new FlexibleBoolConverter(),
                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
            }
        };

        /// <inheritdoc />
        public string Serialize<T>(T value)
        {
            return JsonSerializer.Serialize(value, SerializerOptions);
        }

        /// <inheritdoc />
        public T Deserialize<T>(string value)
        {
            return JsonSerializer.Deserialize<T>(value, SerializerOptions);
        }
    }
}

