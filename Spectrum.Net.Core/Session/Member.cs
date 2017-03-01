using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core.Session
{
    public class Member
    {
        [JsonProperty("id")]
        public UInt64 Id { get; internal set; }

        [JsonProperty("displayname")]
        public String DisplayName { get; internal set; }

        [JsonProperty("nickname")]
        public String Nickname { get; internal set; }

        [JsonProperty("avatar")]
        public String Avatar { get; internal set; }

        [JsonProperty("meta")]
        public MemberMeta Meta { get; internal set; }

        [JsonProperty("presence")]
        public Presence Presence { get; internal set; }
    }
}