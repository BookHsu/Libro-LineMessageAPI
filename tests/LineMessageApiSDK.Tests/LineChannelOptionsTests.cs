using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LineMessageApiSDK.Tests
{
    [TestClass]
    public class LineChannelOptionsTests
    {
        [TestMethod]
        public void Options_Default_Should_Be_Empty_String()
        {
            var options = new LineChannelOptions();

            Assert.AreEqual(string.Empty, options.ChannelAccessToken);
            Assert.AreEqual(string.Empty, options.ChannelSecret);
        }
    }
}
