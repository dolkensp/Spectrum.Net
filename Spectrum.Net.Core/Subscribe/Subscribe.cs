using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core.Subscribe
{
    public class Payload : WebSocket.Payload
    {
        [JsonProperty("subscription_keys")]
        public String[] SubscriptionKeys { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("subscription_scope")]
        public SubscriptionScope SubscriptionScope { get; set; }
    }
}