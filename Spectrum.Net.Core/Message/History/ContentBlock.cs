using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Spectrum.Net.Core.Message.History
{
    public class ContentBlock
    {
        [JsonProperty("depth")]
        public Int32 Depth { get; internal set; }

        [JsonProperty("entityRanges")]
        public IEnumerable<EntityRange> EntityRanges { get; internal set; } = new EntityRange[] { };

        [JsonProperty("inlineStyleRanges")]
        public IEnumerable<StyleRange> StyleRanges { get; internal set; } = new StyleRange[] { };

        [JsonProperty("key")]
        public String Key { get; internal set; }

        [JsonProperty("text")]
        public String Text { get; internal set; }

        [JsonProperty("type")]
        public String Type { get; internal set; } = "unstyled";
    }
}