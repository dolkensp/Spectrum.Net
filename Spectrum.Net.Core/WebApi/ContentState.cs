using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core
{
    public class ContentState
    {
        [JsonProperty("entityMap")]
        public Object EntityMap { get; set; } = new Object { };

        [JsonProperty("blocks")]
        public ContentBlock[] Blocks { get; set; }
    }
}