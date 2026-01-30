using LineMessageApiSDK.Method;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LineMessageApiSDK.Tests
{
    [TestClass]
    public class LineApiEndpointsTests
    {
        [TestMethod]
        public void BuildMessageContent_Should_Use_ApiData_Domain()
        {
            var url = LineApiEndpoints.BuildMessageContent("message-id");

            Assert.AreEqual("https://api-data.line.me/v2/bot/message/message-id/content", url);
        }

        [TestMethod]
        public void BuildUserProfile_Should_Use_Api_Domain()
        {
            var url = LineApiEndpoints.BuildUserProfile("user-id");

            Assert.AreEqual("https://api.line.me/v2/bot/profile/user-id", url);
        }
    }
}
