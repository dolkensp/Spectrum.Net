using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core
{
    public class Result<T>
    {
        [JsonProperty("code")]
        public String Code { get; set; }

        [JsonProperty("data")]
        public T Data { get; set; }

        [JsonProperty("msg")]
        public String Message { get; set; }

        [JsonProperty("success")]
        public Int32 Success { get; set; }
    }
}