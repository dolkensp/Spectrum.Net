using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using History = Spectrum.Net.Core.Message.History;

namespace Spectrum.Net.Core.Message.New
{
    public class Payload : WebSocket.Payload
    {
        [JsonProperty("message")]
        public History.Message Message { get; set; }

        [JsonProperty("action_id")]
        public UInt64 ActionId { get; internal set; }

        [JsonProperty("owner")]
        public Boolean Owner { get; set; } = true;
    }
}