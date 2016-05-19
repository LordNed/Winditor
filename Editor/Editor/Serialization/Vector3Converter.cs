using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace WindEditor.Serialization
{
    public class Vector3Converter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(OpenTK.Vector3);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var temp = JObject.Load(reader);
            return new OpenTK.Vector3(((float?)temp["X"]).GetValueOrDefault(), ((float?)temp["Y"]).GetValueOrDefault(), ((float?)temp["Z"]).GetValueOrDefault());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var vec = (OpenTK.Vector3)value;
            serializer.Serialize(writer, new { X = vec.X, Y = vec.Y , Z = vec.Z});
        }
    }
}
