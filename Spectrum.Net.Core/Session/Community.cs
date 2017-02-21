using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core.Session
{
    public class Community
    {
        [JsonProperty("id")]
        public UInt64 Id { get; internal set; }

        [JsonProperty("name")]
        public String Name { get; internal set; }

        [JsonProperty("slug")]
        public String Slug { get; internal set; }

        [JsonProperty("avatar")]
        public String Avatar { get; internal set; }

        [JsonProperty("banner")]
        public String Banner { get; internal set; }

        [JsonProperty("lobbies")]
        public Lobby[] Lobbies { get; internal set; }

        [JsonProperty("forum_channel_groups")]
        public ChannelGroup[] ForumChannelGroups { get; internal set; }

        [JsonProperty("roles")]
        public CommunityRole[] Roles { get; internal set; }
    }
}