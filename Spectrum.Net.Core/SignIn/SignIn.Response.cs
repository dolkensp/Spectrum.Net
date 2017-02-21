using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core
{
    public static partial class SignIn
    {
        public class Response
        {
            [JsonProperty("displayname")]
            public String DisplayName { get; set; }

            [JsonProperty("nickname")]
            public String Nickname { get; set; }

            [JsonProperty("session_id")]
            public String SessionId { get; set; }

            [JsonProperty("session_name")]
            public String SessionName { get; set; }
        }
    }
}