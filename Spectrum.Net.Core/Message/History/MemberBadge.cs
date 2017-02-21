using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core.Message.History
{
    public class MemberBadge
    {
        [JsonProperty("name")]
        public String Name { get; internal set; }

        [JsonProperty("icon")]
        public String Icon { get; internal set; }

        [JsonProperty("url")]
        public String Url { get; internal set; }
    }
}