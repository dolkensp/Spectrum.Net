using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core.Session
{
    public class CommunityRole
    {
        [JsonProperty("id")]
        public UInt64 Id { get; set; }

        [JsonProperty("name")]
        public String Name { get; set; }

        // community_id
        // type
        // order
        // visible
        // description
        // color
        // permissions
        // members_count
    }
}