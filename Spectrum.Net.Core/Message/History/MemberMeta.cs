using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core.Message.History
{
    public class MemberMeta
    {
        [JsonProperty("signature")]
        public String Signature { get; internal set; }

        [JsonProperty("badges")]
        public MemberBadge[] Badges { get; internal set; }
    }
}