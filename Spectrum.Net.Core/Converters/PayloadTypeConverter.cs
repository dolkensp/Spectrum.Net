using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core.Converters
{
    internal class PayloadTypeConverter : JsonConverter
    {
        public override Boolean CanConvert(Type objectType)
        {
            return objectType == typeof(PayloadType);
        }

        public override Object ReadJson(JsonReader reader, Type objectType, Object existingValue, JsonSerializer serializer)
        {
            return EnumUtils.ToEnum($"{reader.Value}", PayloadType.Unknown);
        }

        public override void WriteJson(JsonWriter writer, Object value, JsonSerializer serializer)
        {
            var castValue = value as PayloadType?;
            writer.WriteValue(EnumUtils.ToString(castValue ?? PayloadType.Unknown));
        }
    }
}
