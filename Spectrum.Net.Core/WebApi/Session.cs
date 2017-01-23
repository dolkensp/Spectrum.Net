using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core
{
    public class Session
    {
        [JsonProperty("config")]
        public Config Config { get; set; }

        [JsonProperty("token")]
        public String Token { get; set; }

        [JsonProperty("member")]
        public Member Member { get; set; }

        [JsonProperty("settings")]
        public Settings Settings { get; set; }

        [JsonProperty("roles")]
        public Dictionary<UInt64, UInt64[]> Roles { get; set; }

        [JsonProperty("communities")]
        public Community[] Communities { get; set; }

        [JsonProperty("bookmarks")]
        public Bookmark[] Bookmarks { get; set; }

        [JsonProperty("private_lobbies")]
        public MessageLobby[] PrivateLobbies { get; set; }

        [JsonProperty("notifications")]
        public Notification[] Notifications { get; set; }

        [JsonProperty("notifications_unread")]
        public Int32 NotificationsUnread { get; set; }
    }
}