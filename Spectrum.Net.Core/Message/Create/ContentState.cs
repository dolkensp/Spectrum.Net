using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core.Message.Create
{
    public class ContentStateRequest
    {
        [JsonProperty("blocks")]
        public IEnumerable<ContentBlock> Blocks { get; set; }

        [JsonProperty("entityMap")]
        public Dictionary<UInt64, Entity> EntityMap { get; set; } = new Dictionary<UInt64, Entity> { };
    }

    public class ContentStateResponse
    {
        [JsonProperty("blocks")]
        public IEnumerable<ContentBlock> Blocks { get; internal set; }

        [JsonProperty("entityMap")]
        public IEnumerable<Entity> EntityMap { get; internal set; }
    }
}