using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core
{
    public class Message
    {
        [JsonProperty("id")]
        public Int32 Id { get; internal set; }

        [JsonProperty("time_created")]
        public Int64 TimeCreated { get; internal set; } = (DateTime.UtcNow.Ticks - 621355968000000000) / 10000000;

        [JsonProperty("time_modified")]
        public Int64 TimeModified { get; internal set; } = (DateTime.UtcNow.Ticks - 621355968000000000) / 10000000;

        [JsonProperty("lobby_id")]
        public String LobbyId { get; set; }

        [JsonProperty("media_id")]
        public String MediaId { get; set; }

        [JsonProperty("content_state")]
        public ContentState ContentState { get; set; }

        [JsonProperty("plaintext")]
        public String PlainText { get; set; }

        [JsonProperty("member")]
        public Member Member { get; set; }

        [JsonProperty("erased_by")]
        public Member ErasedBy { get; set; }

        [JsonProperty("is_erased")]
        public Boolean IsErased { get; set; }
    }
}