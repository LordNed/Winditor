using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenTK;

namespace SuperBMDLib.Util
{
    /// <summary>
    /// A JSON converter for OpenTK's Vector2 class.
    /// </summary>
    class Vector2Converter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Vector2);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var obj = JToken.Load(reader);
            if (obj.Type == JTokenType.Array)
            {
                var arr = (JArray)obj;
                if (arr.Count == 2)
                {
                    return new Vector2(arr[0].Value<float>(), arr[1].Value<float>());
                }
            }
            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var vector = (Vector2)value;
            writer.WriteStartArray();
            writer.WriteValue(vector.X);
            writer.WriteValue(vector.Y);
            writer.WriteEndArray();
        }
    }

    /// <summary>
    /// A JSON converter for OpenTK's Vector3 class.
    /// </summary>
    class Vector3Converter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Vector3);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var obj = JToken.Load(reader);
            if (obj.Type == JTokenType.Array)
            {
                var arr = (JArray)obj;
                if (arr.Count == 3)
                {
                    return new Vector3(arr[0].Value<float>(), arr[1].Value<float>(), arr[2].Value<float>());
                }
            }
            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var vector = (Vector3)value;
            writer.WriteStartArray();
            writer.WriteValue(vector.X);
            writer.WriteValue(vector.Y);
            writer.WriteValue(vector.Z);
            writer.WriteEndArray();
        }
    }

    /// <summary>
    /// A JSON converter for OpenTK's Matrix4 class.
    /// </summary>
    class Matrix4Converter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Matrix4);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var obj = JToken.Load(reader);
            if (obj.Type == JTokenType.Array)
            {
                var arr = (JArray)obj;
                if (arr.Count == 4)
                {
                    var row0 = (JArray)arr[0];
                    var row1 = (JArray)arr[1];
                    var row2 = (JArray)arr[2];
                    var row3 = (JArray)arr[3];

                    if (row0.Count == 4 && row1.Count == 4 && row2.Count == 4 && row3.Count == 4)
                    {
                        return new Matrix4(
                            row0[0].Value<float>(), row0[1].Value<float>(), row0[2].Value<float>(), row0[3].Value<float>(),
                            row1[0].Value<float>(), row1[1].Value<float>(), row1[2].Value<float>(), row1[3].Value<float>(),
                            row2[0].Value<float>(), row2[1].Value<float>(), row2[2].Value<float>(), row2[3].Value<float>(),
                            row3[0].Value<float>(), row3[1].Value<float>(), row3[2].Value<float>(), row3[3].Value<float>()
                            );
                    }
                }
            }
            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var matrix = (Matrix4)value;
            writer.WriteStartArray();
            foreach (Vector4 row in new Vector4[] { matrix.Row0, matrix.Row1, matrix.Row2, matrix.Row3 })
            {
                writer.WriteStartArray();
                writer.WriteValue(row.X);
                writer.WriteValue(row.Y);
                writer.WriteValue(row.Z);
                writer.WriteValue(row.W);
                writer.WriteEndArray();
            }
            writer.WriteEndArray();
        }
    }

    /// <summary>
    /// A JSON converter for OpenTK's Matrix2x3 class.
    /// </summary>
    class Matrix2x3Converter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Matrix2x3);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var obj = JToken.Load(reader);
            if (obj.Type == JTokenType.Array)
            {
                var arr = (JArray)obj;
                if (arr.Count == 2)
                {
                    var row0 = (JArray)arr[0];
                    var row1 = (JArray)arr[1];

                    if (row0.Count == 3 && row1.Count == 3)
                    {
                        return new Matrix2x3(
                            row0[0].Value<float>(), row0[1].Value<float>(), row0[2].Value<float>(),
                            row1[0].Value<float>(), row1[1].Value<float>(), row1[2].Value<float>()
                            );
                    }
                }
            }
            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var matrix = (Matrix2x3)value;
            writer.WriteStartArray();
            writer.WriteStartArray();
            writer.WriteValue(matrix.Row0.X);
            writer.WriteValue(matrix.Row0.Y);
            writer.WriteValue(matrix.Row0.Z);
            writer.WriteEndArray();
            writer.WriteStartArray();
            writer.WriteValue(matrix.Row1.X);
            writer.WriteValue(matrix.Row1.Y);
            writer.WriteValue(matrix.Row1.Z);
            writer.WriteEndArray();
            writer.WriteEndArray();
        }
    }
}
