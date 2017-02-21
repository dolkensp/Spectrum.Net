using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core.Message.Create
{
    public class Member
    {
        [JsonProperty("avatar")]
        public String Avatar { get; set; }

        [JsonProperty("displayname")]
        public String DisplayName { get; set; }

        [JsonProperty("id")]
        public UInt64 Id { get; set; }

        [JsonProperty("nickname")]
        public String Nickname { get; set; }
    }
}