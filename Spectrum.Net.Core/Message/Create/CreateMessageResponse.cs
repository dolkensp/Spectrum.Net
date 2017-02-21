using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core.Message.Create
{
    public class CreateMessageResponse : CreateMessageRequest
    {
        [JsonProperty("content_state")]
        public new ContentStateResponse ContentState { get; set; }

        [JsonProperty("id")]
        public UInt64 Id { get; internal set; }

        [JsonProperty("member")]
        public Member Member { get; internal set; }

        [JsonProperty("time_created")]
        public Int64 TimeCreated { get; internal set; } = (DateTime.UtcNow.Ticks - 621355968000000000) / 10000000;

        [JsonProperty("time_modified")]
        public Int64 TimeModified { get; internal set; } = (DateTime.UtcNow.Ticks - 621355968000000000) / 10000000;
    }
}