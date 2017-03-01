using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Spectrum.Net.Core.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core.Message.History
{
    public class Member
    {
        [JsonProperty("avatar")]
        public String Avatar { get; internal set; }

        [JsonProperty("displayname")]
        public String DisplayName { get; internal set; }

        [JsonProperty("id")]
        public UInt64 Id { get; internal set; }

        [JsonProperty("meta")]
        public MemberMeta Meta { get; internal set; }

        [JsonProperty("nickname")]
        public String Nickname { get; internal set; }

        [JsonProperty("presence")]
        public Presence Presence { get; internal set; }

        /// <summary>
        /// Map of Lobby:Role[]
        /// </summary>
        [JsonProperty("roles")]
        [JsonConverter(typeof(RoleCollectionConverter))]
        public RoleCollection Roles { get; internal set; }
    }

    public class RoleCollection
    {
        public Dictionary<UInt64, UInt64[]> Mapping { get; set; }
        public UInt64[] Listing { get; set; }

        public static implicit operator RoleCollection(UInt64[] value)
        {
            return new RoleCollection
            {
                Listing = value
            };
        }

        public static implicit operator RoleCollection(Dictionary<UInt64, UInt64[]> value)
        {
            return new RoleCollection
            {
                Mapping = value,
                Listing = value.SelectMany(v => v.Value).ToArray()
            };
        }
    }
}