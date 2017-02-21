using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core.Session
{
    public class Settings
    {
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("theme")]
        public Theme Theme { get; set; }

        [JsonProperty("notitication_popup_enabled")]
        public Boolean NotiticationPopupEnabled { get; set; }

        [JsonProperty("notitication_browser_enabled")]
        public Boolean NotiticationBrowserEnabled { get; set; }

        [JsonProperty("notitication_sound_enabled")]
        public Boolean NotiticationSoundEnabled { get; set; }
    }
}