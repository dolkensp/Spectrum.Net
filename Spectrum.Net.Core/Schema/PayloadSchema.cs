using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core.Schema
{
    internal class PayloadSchema
    {
        [JsonProperty("type")]
        internal String Type { get; set; }
    }
}
