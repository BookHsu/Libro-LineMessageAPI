using System;
using System.IO;
using System.Net.Http;
using System.Text;

namespace Libro.LineMessageApi.Http
{
    internal static class HttpContentSyncExtensions
    {
        internal static string ReadAsStringSync(this HttpContent content)
        {
            if (content == null)
            {
                return string.Empty;
            }

            var charset = content.Headers.ContentType?.CharSet;
            Encoding encoding;
            if (!string.IsNullOrWhiteSpace(charset))
            {
                try
                {
                    encoding = Encoding.GetEncoding(charset);
                }
                catch (ArgumentException)
                {
                    encoding = Encoding.UTF8;
                }
            }
            else
            {
                encoding = Encoding.UTF8;
            }

            using var stream = content.ReadAsStream();
            using var reader = new StreamReader(stream, encoding, detectEncodingFromByteOrderMarks: true);
            return reader.ReadToEnd();
        }
    }
}
