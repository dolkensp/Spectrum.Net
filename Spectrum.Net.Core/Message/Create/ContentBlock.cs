using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Spectrum.Net.Core.Message.Create
{
    public abstract class ContentBlock
    {
        [JsonProperty("depth")]
        public Int32 Depth { get; set; }

        [JsonProperty("entityRanges")]
        public EntityRange[] EntityRanges { get; set; } = new EntityRange[] { };

        [JsonProperty("inlineStyleRanges")]
        public StyleRange[] StyleRanges { get; set; } = new StyleRange[] { };

        [JsonProperty("key")]
        public String Key { get; internal set; } = ContentBlock.NewKey();

        [JsonProperty("text")]
        public String Text { get; set; }

        [JsonProperty("type")]
        public String Type { get; set; } = "unstyled";

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

        private static String _lastKey;
        private static Int64 KEY_SPACE = 60466175;
        public static String NewKey()
        {
            var nextKey = (DateTime.UtcNow.Ticks % KEY_SPACE).ToBase(36);

            while (_lastKey == nextKey)
            {
                nextKey = (DateTime.UtcNow.Ticks % KEY_SPACE).ToBase(36);
                Thread.Sleep(0);
            }

            return _lastKey = nextKey;
        }
    }

    public class ContentBlockRequest : ContentBlock
    {
        [JsonProperty("data")]
        public MessageData Data { get; set; } = new MessageData { };
    }

    public class ContentBlockResponse : ContentBlock
    {
        [JsonProperty("data")]
        public MessageData[] Data { get; internal set; }
    }
}