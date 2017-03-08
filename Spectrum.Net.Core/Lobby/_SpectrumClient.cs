using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.WebSockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Spectrum.Net.Core
{
    public delegate Task MemberJoinDelegate(Lobby.Join.Payload presence, Session.Lobby lobby);
    public delegate Task MemberLeaveDelegate(Lobby.Leave.Payload presence, Session.Lobby lobby);
    public delegate Task MemberUpdateDelegate(Lobby.Update.Payload presence, Session.Lobby lobby);

    public partial class SpectrumClient
    {
        public event MemberJoinDelegate OnMemberJoin;
        public event MemberLeaveDelegate OnMemberLeave;
        public event MemberUpdateDelegate OnMemberUpdate;

        public async Task<Result<Message.History.Member[]>> LoadPresencesAsync(UInt64 lobbyId)
        {
            var payload = new { lobby_id = lobbyId };

            var container = new ObjectContent(payload.GetType(), payload, this._mediaTypeFormatter);

            var result = await this._apiClient.PostAsync("/api/spectrum/lobby/presences", container);

            var str_result = await result.Content.ReadAsStringAsync();

            return str_result.FromJSON<Result<Message.History.Member[]>>();
        }

        public async Task<Result<Session.Lobby>> LoadPrivateLobbyInfoAsync(UInt64 memberId)
        {
            var payload = new { member_id = memberId };

            var container = new ObjectContent(payload.GetType(), payload, this._mediaTypeFormatter);

            var result = await this._apiClient.PostAsync("/api/spectrum/lobby/info", container);

            var str_result = await result.Content.ReadAsStringAsync();

            return str_result.FromJSON<Result<Session.Lobby>>();
        }
    }
}