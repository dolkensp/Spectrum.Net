using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core
{
    public class MessagePayload : Payload
    {
        [JsonProperty("message")]
        public Message Message { get; set; }

        [JsonProperty("action_id")]
        public Int32 ActionId { get; internal set; }

        [JsonProperty("owner")]
        public Boolean Owner { get; set; } = true;
    }
}