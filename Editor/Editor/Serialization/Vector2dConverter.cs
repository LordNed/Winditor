using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace WindEditor.Serialization
{
    public class Vector2dConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(OpenTK.Vector2d);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var temp = JObject.Load(reader);
            return new OpenTK.Vector2d(((double?)temp["X"]).GetValueOrDefault(), ((double?)temp["Y"]).GetValueOrDefault());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var vec = (OpenTK.Vector2d)value;
            serializer.Serialize(writer, new { X = vec.X, Y = vec.Y });
        }
    }
}
