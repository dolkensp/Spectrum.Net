using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core.Message.History
{
    public class EntityRange
    {
        [JsonProperty("offset")]
        public Int32 Offset { get; internal set; }

        [JsonProperty("length")]
        public Int32 Length { get; internal set; }

        [JsonProperty("key")]
        public String Key { get; internal set; }
    }
}