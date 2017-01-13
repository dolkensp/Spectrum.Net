using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core
{
    public class ChannelGroup : Entity
    {
        [JsonProperty("community_id")]
        public Int32? CommunityId { get; set; }

        [JsonProperty("order")]
        public Int32? Order { get; set; }

        [JsonProperty("channels")]
        public ForumChannel[] Channels { get; set; }
    }
}