namespace Libro.LineMessageApi.Serialization
{
    /// <summary>JSON 序列化介面。</summary>
    public interface IJsonSerializer
    {
        /// <summary>序列化物件。</summary>
        string Serialize<T>(T value);

        /// <summary>反序列化物件。</summary>
        T Deserialize<T>(string value);
    }
}

