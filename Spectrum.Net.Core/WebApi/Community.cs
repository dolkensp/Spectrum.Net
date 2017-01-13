using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core
{
    public class Community : Entity
    {
        [JsonProperty("slug")]
        public String Slug { get; set; }

        [JsonProperty("avatar")]
        public String Avatar { get; set; }

        [JsonProperty("banner")]
        public String Banner { get; set; }

        [JsonProperty("lobbies")]
        public MessageLobby[] Lobbies { get; set; }

        [JsonProperty("forum_channel_groups")]
        public ChannelGroup[] ForumChannelGroups { get; set; }

        [JsonProperty("roles")]
        public CommunityRole[] Roles { get; set; }
        // Roles
    }
}