using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core.WebSocket
{
    public class Payload
    {
        [JsonIgnore]
        public PayloadType Type
        {
            get { return EnumUtils.ToEnum(this._Type, PayloadType.Unknown); }
            set { this._Type = EnumUtils.ToString(value); }
        }
        
        [JsonProperty("type")]
        internal String _Type { get; set; }
    }
}
