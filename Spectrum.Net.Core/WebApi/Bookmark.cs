using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core
{
    public class Bookmark
    {
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("type")]
        public BookmarkType Type { get; set; }

        // [JsonProperty("entity")]
        // public Entity Entity { get; set; }
    }
}