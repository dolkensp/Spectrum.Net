using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core
{
    public class Payload
    {
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("type")]
        public PayloadType Type { get; set; }
    }
}
