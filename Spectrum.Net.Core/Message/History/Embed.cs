using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core.Message.History
{
    public class Embed
    {
        [JsonProperty("embed_type")]
        public String EmbedType { get; internal set; }

        [JsonProperty("url")]
        public String Url { get; internal set; }

        [JsonProperty("provider_name")]
        public String ProviderName { get; internal set; }

        [JsonProperty("title")]
        public String Title { get; internal set; }

        [JsonProperty("description")]
        public String Description { get; internal set; }
        
        [JsonProperty("image")]
        public String Image { get; internal set; }

        [JsonProperty("image_width")]
        public UInt32? ImageWidth { get; internal set; }

        [JsonProperty("image_height")]
        public UInt32? ImageHeight { get; internal set; }

        [JsonProperty("provider_icon")]
        public String ProviderIcon { get; internal set; }

        [JsonProperty("time_fetched")]
        public Int64 TimeFetched { get; internal set; }

        [JsonIgnore]
        public DateTime Timestamp
        {
            get { return new DateTime((this.TimeFetched * 10000000) + 621355968000000000); }
            set { this.TimeFetched = (value.Ticks - 621355968000000000) / 10000000; }
        }

        [JsonProperty("sizes")]
        public Dictionary<String, Thumbnail> Sizes { get; internal set; }
    }
}
