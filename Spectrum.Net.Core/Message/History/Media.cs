using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core.Message.History
{
    public class Media
    {
        [JsonProperty("id")]
        public String ID { get; internal set; }

        [JsonProperty("type")]
        public String Type { get; internal set; }

        [JsonProperty("data")]
        public Embed Data { get; internal set; }
    }
}
