using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core
{
    public class ContentBlock
    {
        [JsonProperty("key")]
        public String Key { get; set; } // "key": "8j4qg",

        [JsonProperty("text")]
        public String Text { get; set; } // "text": "uhh i dunno what's oging on any more haha",

        [JsonProperty("type")]
        public String Type { get; set; } = "unstyled"; // "type": "unstyled",

        [JsonProperty("depth")]
        public Int32 Depth { get; set; } // "depth": 0,

        [JsonProperty("inlineStyleRanges")]
        public StyleRange[] StyleRanges { get; set; } = new StyleRange[] { }; // "inlineStyleRanges": [],

        [JsonProperty("entityRanges")]
        public EntityRange[] EntityRanges { get; set; } = new EntityRange[] { }; // "entityRanges": [],

        [JsonProperty("data")]
        public Data[] Data { get; set; } = new Data[] { }; // "data": { }

        // var seenKeys = {};
        // var MULTIPLIER = Math.pow(2, 24);
        // 
        // function generateRandomKey() {
        //   var key = void 0;
        //   while (key === undefined || seenKeys.hasOwnProperty(key) || !isNaN(+key)) {
        //     key = Math.floor(Math.random() * MULTIPLIER).toString(32);
        //   }
        //   seenKeys[key] = true;
        //   return key;
        // }

        public static String NewKey()
        {
            return Guid.NewGuid().ToString();
        }
    }
}