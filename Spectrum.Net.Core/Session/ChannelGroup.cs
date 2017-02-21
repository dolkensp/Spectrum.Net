using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core.Session
{
    public class ChannelGroup
    {
        [JsonProperty("id")]
        public UInt64 Id { get; internal set; }

        [JsonProperty("name")]
        public String Name { get; internal set; }

        [JsonProperty("community_id")]
        public UInt64? CommunityId { get; internal set; }

        [JsonProperty("order")]
        public UInt64? Order { get; internal set; }

        [JsonProperty("channels")]
        public ForumChannel[] Channels { get; internal set; }
    }
}