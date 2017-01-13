using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core
{
    public class Badge
    {
        [JsonProperty("name")]
        public String Name { get; set; }

        [JsonProperty("icon")]
        public String Icon { get; set; }

        [JsonProperty("url")]
        public String Url { get; set; }
    }
}