using Libro.LineMessageApi.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Libro.LineMessageApi.Tests
{
    [TestClass]
    public class HttpContentSyncExtensionsTests
    {
        [TestMethod]
        public void ReadAsStringSync_Should_Respect_Charset()
        {
            var text = "\u6e2c\u8a66ABC";
            var bytes = Encoding.Unicode.GetBytes(text);
            using var content = new ByteArrayContent(bytes);
            content.Headers.ContentType = new MediaTypeHeaderValue("text/plain")
            {
                CharSet = "utf-16"
            };

            var result = content.ReadAsStringSync();

            Assert.AreEqual(text, result);
        }

        [TestMethod]
        public void ReadAsStringSync_Should_Fallback_To_Utf8_On_Invalid_Charset()
        {
            var text = "fallback";
            var bytes = Encoding.UTF8.GetBytes(text);
            using var content = new ByteArrayContent(bytes);
            content.Headers.ContentType = new MediaTypeHeaderValue("text/plain")
            {
                CharSet = "invalid-charset"
            };

            var result = content.ReadAsStringSync();

            Assert.AreEqual(text, result);
        }
    }
}
