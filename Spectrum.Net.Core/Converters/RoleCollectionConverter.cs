using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Spectrum.Net.Core.Message.History;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core.Converters
{
    internal class RoleCollectionConverter : JsonConverter
    {
        JsonSchema _arraySchema;
        JsonSchema _mapSchema1;
        JsonSchema _mapSchema2;

        public RoleCollectionConverter()
        {
            JsonSchemaGenerator generator = new JsonSchemaGenerator();
            this._arraySchema = generator.Generate(typeof(UInt64[]));
            this._mapSchema1 = generator.Generate(typeof(Dictionary<UInt64, UInt64[]>));
            this._mapSchema2 = generator.Generate(typeof(Dictionary<UInt64, String[]>));
        }

        public override void WriteJson(JsonWriter writer, Object value, JsonSerializer serializer)
        {
            RoleCollection castValue = value as RoleCollection;

            if (castValue == null)
            {
                writer.WriteRaw("[]");
                return;
            }

            if (castValue.Mapping == null)
            {
                serializer.Serialize(writer, castValue.Listing);
                return;
            }

            serializer.Serialize(writer, castValue.Mapping);
        }

        public override Object ReadJson(JsonReader reader, Type objectType, Object existingValue, JsonSerializer serializer)
        {
            var roles = JToken.ReadFrom(reader);

            if (roles.IsValid(this._mapSchema1))
            {
                return (RoleCollection)roles.ToObject<Dictionary<UInt64, UInt64[]>>();
            }

            if (roles.IsValid(this._mapSchema2))
            {
                return (RoleCollection)roles.ToObject<Dictionary<UInt64, UInt64[]>>();
            }

            if (roles.IsValid(this._arraySchema))
            {
                return (RoleCollection)roles.ToObject<UInt64[]>();
            }

            return null;
        }

        public override Boolean CanConvert(Type objectType)
        {
            return objectType == typeof(RoleCollection) ||
                objectType == typeof(UInt64[]) ||
                objectType == typeof(Dictionary<UInt64, UInt64[]>);
        }
    }
}
