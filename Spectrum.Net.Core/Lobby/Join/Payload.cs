using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core.Lobby.Join
{
    public class Payload : WebSocket.Payload
    {
        [JsonProperty("lobby_id")]
        public UInt64 LobbyId { get; internal set; }

        [JsonProperty("member_id")]
        public UInt64 MemberId { get; internal set; }

        [JsonProperty("member")]
        public Message.History.Member Member { get; internal set; }
    }
}
