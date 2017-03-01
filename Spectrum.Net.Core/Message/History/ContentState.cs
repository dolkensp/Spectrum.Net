using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core.Message.History
{
    public class ContentState
    {
        [JsonProperty("entityMap")]
        public Object EntityMap { get; internal set; } = new Object { };

        [JsonProperty("blocks")]
        public IEnumerable<ContentBlock> Blocks { get; internal set; }
    }
}