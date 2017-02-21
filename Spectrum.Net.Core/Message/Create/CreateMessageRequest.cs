using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core.Message.Create
{
    public class CreateMessageRequest
    {
        [JsonProperty("content_state")]
        public ContentStateRequest ContentState { get; set; }

        [JsonProperty("highlight_role_id")]
        public UInt64? HighlightRoleId { get; set; }

        [JsonProperty("lobby_id")]
        public UInt64 LobbyId { get; set; }

        [JsonProperty("media_id")]
        public String MediaId { get; set; }

        private String _plainText;

        [JsonProperty("plaintext")]
        public String PlainText
        {
            get { return this._plainText = this._plainText ?? String.Join(" ", this.ContentState.Blocks.Select(c => c.Text)); }
            internal set { this._plainText = value; }
        }
    }
}