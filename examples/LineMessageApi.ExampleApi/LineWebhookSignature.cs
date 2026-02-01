using System.Security.Cryptography;
using System.Text;

namespace LineMessageApi.ExampleApi;

public static class LineWebhookSignature
{
    public static bool Verify(string body, string channelSecret, string signature)
    {
        if (string.IsNullOrWhiteSpace(body) ||
            string.IsNullOrWhiteSpace(channelSecret) ||
            string.IsNullOrWhiteSpace(signature))
        {
            return false;
        }

        var keyBytes = Encoding.UTF8.GetBytes(channelSecret);
        var bodyBytes = Encoding.UTF8.GetBytes(body);
        using var hmac = new HMACSHA256(keyBytes);
        var hash = hmac.ComputeHash(bodyBytes);
        var computed = Convert.ToBase64String(hash);
        return string.Equals(computed, signature, StringComparison.Ordinal);
    }
}
