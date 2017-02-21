using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core.Message.Create
{
    public class EntityRange
    {
        [JsonProperty("offset")]
        public Int32 Offset { get; set; }

        [JsonProperty("length")]
        public Int32 Length { get; set; }

        [JsonProperty("key")]
        public Int32 Key { get; set; }
    }
}