using LineMessageApiSDK.LineMessageObject;
using LineMessageApiSDK.LineReceivedObject;
using LineMessageApiSDK.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LineMessageApiSDK.Tests
{
    [TestClass]
    public class JsonSerializationTests
    {
        [TestMethod]
        public void Serialize_Template_Should_Use_String_Enum()
        {
            var template = new Template(TemplateType.buttons)
            {
                text = "hello"
            };

            var serializer = new SystemTextJsonSerializer();

            var json = serializer.Serialize(template);

            StringAssert.Contains(json, "\"type\":\"buttons\"");
        }

        [TestMethod]
        public void Serialize_LineAction_Should_Use_String_Enum_For_Mode()
        {
            var action = new LineAction(ActionType.datetimepicker)
            {
                mode = DateTimePickerType.date
            };

            var serializer = new SystemTextJsonSerializer();

            var json = serializer.Serialize(action);

            StringAssert.Contains(json, "\"mode\":\"date\"");
        }

        [TestMethod]
        public void Serialize_LineAction_Should_Include_DisplayText()
        {
            var action = new LineAction(ActionType.postback)
            {
                data = "action=postback",
                displayText = "這是留言"
            };

            var serializer = new SystemTextJsonSerializer();

            var json = serializer.Serialize(action);

            StringAssert.Contains(json, "\"displayText\":\"這是留言\"");
        }

        [TestMethod]
        public void Serialize_LineAction_Should_Omit_DisplayText_When_Null()
        {
            var action = new LineAction(ActionType.postback)
            {
                data = "action=postback"
            };

            var serializer = new SystemTextJsonSerializer();

            var json = serializer.Serialize(action);

            StringAssert.DoesNotContain(json, "\"displayText\":");
        }

        [TestMethod]
        public void Deserialize_UserProfile_Should_Be_Case_Insensitive()
        {
            var serializer = new SystemTextJsonSerializer();
            var json = "{\"DisplayName\":\"Line User\",\"pictureUrl\":\"url\",\"statusMessage\":\"hello\",\"userId\":\"id\"}";

            var profile = serializer.Deserialize<UserProfile>(json);

            Assert.AreEqual("Line User", profile.displayName);
            Assert.AreEqual("url", profile.pictureUrl);
            Assert.AreEqual("hello", profile.statusMessage);
            Assert.AreEqual("id", profile.userId);
        }
    }
}
