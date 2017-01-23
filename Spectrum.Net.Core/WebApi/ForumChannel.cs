using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core
{
    public class ForumChannel
    {
        [JsonProperty("id")]
        public Int32 Id { get; set; }

        [JsonProperty("name")]
        public String Name { get; set; }

        [JsonProperty("community_id")]
        public Int32? CommunityId { get; set; }

        [JsonProperty("group_id")]
        public Int32 GroupId { get; set; }

        [JsonProperty("order")]
        public Int32? Order { get; set; }

        [JsonProperty("description")]
        public String Description { get; set; }

        [JsonProperty("color")]
        public String Color { get; set; }

        [JsonProperty("subscription_key")]
        public String SubscriptionKey { get; set; }

        [JsonProperty("labels")]
        public Label[] Labels { get; set; }

        [JsonProperty("threads_count")]
        public Int32 ThreadsCount { get; set; }

        // NotificationSubscription

        // Permissions
    }
}