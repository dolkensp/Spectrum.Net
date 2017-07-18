using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core.Message.History
{
    public class Thumbnail
    {
        [JsonProperty("url")]
        public String Url { get; internal set; }

        [JsonProperty("image_width")]
        public UInt32 ImageWidth { get; internal set; }

        [JsonProperty("image_height")]
        public UInt32 ImageHeight { get; internal set; }
    }
}
