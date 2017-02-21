using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core.Message.Create
{
    public class Entity
    {
        [JsonProperty("data")]
        public String Data { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("mutability")]
        public Mutability? Mutability { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("type")]
        public EntityType? Type { get; set; }
    }
}