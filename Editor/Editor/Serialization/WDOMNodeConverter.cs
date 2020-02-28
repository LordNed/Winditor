using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WindEditor.Serialization
{
    public class WDOMNodeJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.IsSubclassOf(typeof(WDOMNode));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var node = (WDOMNode)value;

            var wproperties = node.GetType().GetProperties().Where(prop =>
            {
                CustomAttributeData[] custom_attributes = prop.CustomAttributes.ToArray();
                if (custom_attributes.Length == 0 || custom_attributes[0].AttributeType.Name != "WProperty")
                    return false;
                return true;
            });

            Dictionary<string, object> objPropsDict = wproperties.ToDictionary(prop => prop.Name, prop => prop.GetValue(node, null));
            serializer.Serialize(writer, objPropsDict);
        }
    }
}
