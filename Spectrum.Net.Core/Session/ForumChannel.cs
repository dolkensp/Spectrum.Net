using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core.Session
{
    public class ForumChannel
    {
        [JsonProperty("id")]
        public UInt64 Id { get; set; }

        [JsonProperty("name")]
        public String Name { get; set; }

        [JsonProperty("community_id")]
        public UInt64? CommunityId { get; set; }

        [JsonProperty("group_id")]
        public UInt64 GroupId { get; set; }

        [JsonProperty("order")]
        public UInt64? Order { get; set; }

        [JsonProperty("description")]
        public String Description { get; set; }

        [JsonProperty("color")]
        public String Color { get; set; }

        [JsonProperty("subscription_key")]
        public String SubscriptionKey { get; set; }

        [JsonProperty("labels")]
        public Label[] Labels { get; set; }

        [JsonProperty("threads_count")]
        public UInt64 ThreadsCount { get; set; }

        // NotificationSubscription

        // Permissions
    }
}