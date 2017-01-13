using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core
{
    public class MemberMeta
    {
        [JsonProperty("signature")]
        public String Signature { get; set; }

        [JsonProperty("badges")]
        public Badge[] Badges { get; set; }
    }
}