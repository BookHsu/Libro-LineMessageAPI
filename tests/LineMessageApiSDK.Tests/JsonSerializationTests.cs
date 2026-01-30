using System.Text.Json;
using LineMessageApiSDK.LineMessageObject;
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

            var json = JsonSerializer.Serialize(template);

            StringAssert.Contains(json, "\"type\":\"buttons\"");
        }

        [TestMethod]
        public void Serialize_LineAction_Should_Use_String_Enum_For_Mode()
        {
            var action = new LineAction(ActionType.datetimepicker)
            {
                mode = DateTimePickerType.date
            };

            var json = JsonSerializer.Serialize(action);

            StringAssert.Contains(json, "\"mode\":\"date\"");
        }
    }
}
