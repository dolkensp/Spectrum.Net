using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Spectrum.Net.Core.Message.Create;
using Spectrum.Net.Core.Message.History;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core.Converters
{
    internal class MessageDataConverter : JsonConverter
    {
        JsonSchema _arraySchema;
        JsonSchema _objectSchema;

        public MessageDataConverter()
        {
            JsonSchemaGenerator generator = new JsonSchemaGenerator();
            this._arraySchema = generator.Generate(typeof(MessageData[]));
            this._objectSchema = generator.Generate(typeof(MessageData));
        }

        public override void WriteJson(JsonWriter writer, Object value, JsonSerializer serializer)
        {
            var empty = value is Array && (value as Array).Length == 0;

            if (value == null || empty)
            {
                writer.WriteRawValue("{}");
                return;
            }

            var objectType = value.GetType();

            if (objectType == typeof(MessageData))
            {

            }

            // if (typeof)
            
            // RoleCollection castValue = value as RoleCollection;
            // 
            // if (castValue == null)
            // {
            //     writer.WriteRaw("[]");
            //     return;
            // }
            // 
            // if (castValue.Mapping == null)
            // {
            //     serializer.Serialize(writer, castValue.Listing);
            //     return;
            // }
            // 
            // serializer.Serialize(writer, castValue.Mapping);
        }

        public override Object ReadJson(JsonReader reader, Type objectType, Object existingValue, JsonSerializer serializer)
        {
            var data = JToken.ReadFrom(reader);
            
            if (data.IsValid(this._objectSchema))
            {
                return new MessageData[] { (MessageData)data.ToObject<MessageData>() };
            }

            if (data.IsValid(this._arraySchema))
            {
                return (MessageData[])data.ToObject<MessageData[]>();
            }

            return null;
        }

        public override Boolean CanConvert(Type objectType)
        {
            return objectType == typeof(MessageData) ||
                objectType == typeof(MessageData[]);
        }
    }
}
