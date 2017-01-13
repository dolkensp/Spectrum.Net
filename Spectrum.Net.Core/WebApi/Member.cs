using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core
{
    public class Member
    {
        [JsonProperty("id")]
        public String Id { get; set; }

        [JsonProperty("displayname")]
        public String DisplayName { get; set; }

        [JsonProperty("nickname")]
        public String Nickname { get; set; }

        [JsonProperty("avatar")]
        public String Avatar { get; set; }

        [JsonProperty("meta")]
        public MemberMeta Meta { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("presence")]
        public MemberPresence? Presence { get; set; }
    }
}
