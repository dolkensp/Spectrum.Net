using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core
{
    public class Reaction
    {
        [JsonProperty("type")]
        public String Type { get; set; }

        [JsonProperty("count")]
        public Int32 Count { get; set; }

        [JsonProperty("members")]
        public Dictionary<UInt64, String> Members { get; set; }
    }
}
