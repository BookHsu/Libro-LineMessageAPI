using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LineMessageApiSDK.Tests
{
    [TestClass]
    public class LineChannelTests
    {
        [TestMethod]
        public void Constructor_Should_Set_ChannelAccessToken()
        {
            var channel = new LineChannel("token-value");

            Assert.AreEqual("token-value", channel.channelAccessToken);
        }

        [TestMethod]
        public void LeaveRoomOrGroup_Should_Throw_When_SourceType_Is_User()
        {
            var channel = new LineChannel("token-value");

            Assert.ThrowsException<NotSupportedException>(() =>
                channel.Leave_Room_Or_Group("source-id", SourceType.user));
        }

        [TestMethod]
        public void LeaveRoomOrGroupAsync_Should_Throw_When_SourceType_Is_User()
        {
            var channel = new LineChannel("token-value");

            Assert.ThrowsException<NotSupportedException>(() =>
                channel.Leave_Room_Or_GroupAsync("source-id", SourceType.user));
        }
    }
}
