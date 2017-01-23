using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Spectrum.Net.Core
{
    public partial class SpectrumClient : IDisposable
    {
        private HttpClient _apiClient = new HttpClient { };
        private ClientWebSocket _socketClient = new ClientWebSocket();
        private CancellationTokenSource _cancellationOwner = new CancellationTokenSource { };
        private MediaTypeFormatter _mediaTypeFormatter = new JsonMediaTypeFormatter { };

        public static readonly Int32 KEEPALIVE_TIMEOUT = 30000;
        public static readonly ArraySegment<Byte> KEEPALIVE = new ArraySegment<Byte>(new Byte[] { 8 });

        private String _rsiTokenName;
        private String _rsiToken;

        public SpectrumClient(String baseAddress = @"https://spectrum.robertsspaceindustries.com")
        {
            this._apiClient.BaseAddress = new Uri(baseAddress);
        }

        public async Task<Result<SignInResponse>> LoginAsync(String username, String password)
        {
            var payload = new SignInRequest { Username = username, Password = password, Remember = 0 };
            var container = new ObjectContent<SignInRequest>(payload, this._mediaTypeFormatter);

            // container.Headers.Add("x-rsi-ptu-token", this._rsiToken);

            var result = await this._apiClient.PostAsync("/api/account/signin", container);

            var str_result = await result.Content.ReadAsStringAsync();

            var signin = str_result.FromJSON<Result<SignInResponse>>();

            if (signin.Data != null)
            {
                this._rsiTokenName = $"x-{signin.Data.SessionName}";
                this._rsiToken = signin.Data.SessionId;
            }

            return signin;
        }

        public async Task<Result<Session>> IdentifyAsync()
        {
            var container = new ObjectContent<Object>(new Object { }, this._mediaTypeFormatter);

            container.Headers.Add(this._rsiTokenName, this._rsiToken);

            var result = await this._apiClient.PostAsync("/api/spectrum/auth/identify", container);

            var str_result = await result.Content.ReadAsStringAsync();

            var obj_result = str_result.FromJSON<Result<Session>>();

            this._wsToken = obj_result.Data.Token;
            this._wsRoot = obj_result.Data.Config.BroadcasterWebsocketUrl;

            return obj_result;
        }

        public async Task<Result<Session>> IdentifyAsync(String token, String tokenName = "x-rsi-token")
        {
            this._rsiToken = token;
            this._rsiTokenName = tokenName;

            var obj_result = await this.IdentifyAsync();

            this._wsToken = obj_result.Data.Token;
            this._wsRoot = obj_result.Data.Config.BroadcasterWebsocketUrl;

            return obj_result;
        }

        public async Task<Result<Message>> SendMessageAsync(Message message)
        {
            var container = new ObjectContent<Message>(message, this._mediaTypeFormatter);
            container.Headers.Add(this._rsiTokenName, this._rsiToken);
            container.Headers.Add("x-tavern-id", "52dzt61b6oyss");

            var result = await this._apiClient.PostAsync("/api/spectrum/message/create", container);

            var str_result = await result.Content.ReadAsStringAsync();

            return str_result.FromJSON<Result<Message>>();
        }

        public async Task<Result<Message>> SoftEraseAsync(UInt64 messageId)
        {
            var payload = new
            {
                message_id = messageId
            };

            var container = new ObjectContent(payload.GetType(), payload, this._mediaTypeFormatter);
            container.Headers.Add(this._rsiTokenName, this._rsiToken);
            container.Headers.Add("x-tavern-id", "52dzt61b6oyss");

            var result = await this._apiClient.PostAsJsonAsync("/api/spectrum/message/soft-erase", container);

            var str_result = await result.Content.ReadAsStringAsync();

            return str_result.FromJSON<Result<Message>>();
        }

        public async Task<Result<History>> LoadMessagesAsync(UInt64 lobbyId, UInt64? before = null, Int32 size = 50)
        {
            var payload = new { lobby_id = lobbyId, before = before, size = size };

            var container = new ObjectContent(payload.GetType(), payload, this._mediaTypeFormatter);
            container.Headers.Add(this._rsiTokenName, this._rsiToken);
            container.Headers.Add("x-tavern-id", "52dzt61b6oyss");

            var result = await this._apiClient.PostAsync("/api/spectrum/message/history", container);

            var str_result = await result.Content.ReadAsStringAsync();

            return str_result.FromJSON<Result<History>>();
        }

        public void Dispose()
        {
            if (this._socketClient.State == WebSocketState.Open) this.DisconnectAsync().Wait();

            this._socketClient.Dispose();

            this._apiClient.Dispose();
        }
    }
}