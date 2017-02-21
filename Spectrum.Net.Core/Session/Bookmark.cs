using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core.Session
{
    public class Bookmark
    {
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("type")]
        public BookmarkType Type { get; internal set; }

        [JsonProperty("entity")]
        public Dictionary<String, String> _Entity { get; internal set; }
    }
}