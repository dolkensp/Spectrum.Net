using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core.Message.History
{
    public class Presence
    {
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("status")]
        public MemberPresence? Status { get; set; }

        [JsonProperty("since")]
        public UInt64 Since { get; set; }

        [JsonProperty("info")]
        public String Info { get; set; }
    }
}
