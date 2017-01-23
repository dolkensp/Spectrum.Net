using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core
{
    public class MessageLobby
    {
        [JsonProperty("id")]
        public Int32 Id { get; set; }

        [JsonProperty("name")]
        public String Name { get; set; }

        [JsonProperty("subscription_key")]
        public String SubscriptionKey { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("type")]
        public LobbyType Type { get; set; }

        [JsonProperty("community_id")]
        public Int32? CommunityId { get; set; }

        [JsonProperty("description")]
        public String Description { get; set; }

        [JsonProperty("color")]
        public String Color { get; set; }

        [JsonProperty("last_read")]
        public Int32? LastRead { get; set; }

        [JsonProperty("latest")]
        public Int32? Latest { get; set; }

        [JsonProperty("members")]
        public Member[] Members { get; set; }

        [JsonProperty("new_messages")]
        public Int32 NewMessages { get; set; }

        [JsonProperty("last_message")]
        public Message LastMessage { get; set; }

        // Permissions
    }
}