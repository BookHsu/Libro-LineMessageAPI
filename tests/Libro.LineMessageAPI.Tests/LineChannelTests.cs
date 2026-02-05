using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Libro.LineMessageApi.Tests
{
    [TestClass]
    public class LineChannelTests
    {
        [TestMethod]
        public void Constructor_Should_Set_ChannelAccessToken()
        {
            // 建立測試用的 LineChannel
            var channel = new LineChannel("token-value");

            // 驗證 token 是否正確設定
            Assert.AreEqual("token-value", channel.channelAccessToken);
        }

        [TestMethod]
        public void LeaveRoomOrGroup_Should_Throw_When_SourceType_Is_User()
        {
            // 建立測試用的 LineChannel
            var channel = new LineChannel("token-value");

            // SourceType 為 user 時應拋出例外
            Assert.ThrowsException<NotSupportedException>(() =>
                channel.LeaveRoomOrGroup("source-id", SourceType.user));
        }

        [TestMethod]
        public void LeaveRoomOrGroupAsync_Should_Throw_When_SourceType_Is_User()
        {
            // 建立測試用的 LineChannel
            var channel = new LineChannel("token-value");

            // SourceType 為 user 時應拋出例外
            Assert.ThrowsException<NotSupportedException>(() =>
                channel.LeaveRoomOrGroupAsync("source-id", SourceType.user));
        }
    }
}

