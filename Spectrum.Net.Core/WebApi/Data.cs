using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core
{
    public class Data
    {
        [JsonProperty("entityMap")]
        public EntityMap EntityMap { get; set; }

        [JsonProperty("blocks")]
        public ContentBlock[] Blocks { get; set; }
    }
}
