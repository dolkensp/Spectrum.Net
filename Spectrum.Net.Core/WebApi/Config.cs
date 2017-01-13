using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core
{
    public class Config
    {
        // permissions_schema

        [JsonProperty("broadcaster_websocket_url")]
        public String BroadcasterWebsocketUrl { get; set; }

        [JsonProperty("provider_signin_url")]
        public String ProviderSigninUrl { get; set; }

        [JsonProperty("provider_profile_url")]
        public String ProviderProfileUrl { get; set; }

        [JsonProperty("provider_profile_edit_url")]
        public String ProviderProfileEditUrl { get; set; }

        [JsonProperty("provider_community_edit_url")]
        public String ProviderCommunityEditUrl { get; set; }

        [JsonProperty("provider_signout_url")]
        public String ProviderSignoutUrl { get; set; }
    }
}