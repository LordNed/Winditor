using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace WindEditor.Serialization
{
    public class Vector3dConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(OpenTK.Vector3d);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var temp = JObject.Load(reader);
            return new OpenTK.Vector3d(((double?)temp["X"]).GetValueOrDefault(), ((double?)temp["Y"]).GetValueOrDefault(), ((double?)temp["Z"]).GetValueOrDefault());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var vec = (OpenTK.Vector3d)value;
            serializer.Serialize(writer, new { X = vec.X, Y = vec.Y , Z = vec.Z});
        }
    }
}
