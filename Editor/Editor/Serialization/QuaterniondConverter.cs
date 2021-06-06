using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace WindEditor.Serialization
{
    public class QuaterniondConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(OpenTK.Quaterniond);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var temp = JObject.Load(reader);
            return new OpenTK.Quaterniond(((double?)temp["X"]).GetValueOrDefault(), ((double?)temp["Y"]).GetValueOrDefault(), ((double?)temp["Z"]).GetValueOrDefault(), ((double?)temp["W"]).GetValueOrDefault());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var quat = (OpenTK.Quaterniond)value;
            serializer.Serialize(writer, new { X = quat.X, Y = quat.Y , Z = quat.Z, W = quat.W});
        }
    }
}
