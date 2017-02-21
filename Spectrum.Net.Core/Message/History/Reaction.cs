using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core.Message.History
{
    public class Reaction
    {
        [JsonProperty("type")]
        public String Type { get; internal set; }

        [JsonProperty("count")]
        public Int32 Count { get; internal set; }

        [JsonProperty("members")]
        public Dictionary<UInt64, String> Members { get; internal set; }
    }
}
