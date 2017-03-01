using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Spectrum.Net.Core.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core.WebSocket
{
    public class Payload
    {
        [JsonProperty("type")]
        [JsonConverter(typeof(PayloadTypeConverter))]
        internal PayloadType Type { get; set; }
    }
}
