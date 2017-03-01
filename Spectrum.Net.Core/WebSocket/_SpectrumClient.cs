#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
    public delegate void FrameReceivedDelegate(String buffer);
    public delegate void KeepAliveDelegate();

    public partial class SpectrumClient : IDisposable
    {
        public event FrameReceivedDelegate FrameReceived;
        public event KeepAliveDelegate KeepAlive;

        private String _wsToken;
        private String _wsRoot;

        public async Task ConnectAsync()
        {
            if (this._socketClient.State != WebSocketState.Open)
            {
                await this._socketClient.ConnectAsync(new Uri($"{this._wsRoot}/?token={this._wsToken}"), this._cancellationOwner.Token);

                this.SpinKeepAlive();
                this.SpinWebSocket();
            }
        }

        private async Task SpinWebSocket()
        {
            while (!this._cancellationOwner.IsCancellationRequested && this._socketClient.State == WebSocketState.Open)
            {
                try
                {
                    var buffer = new Byte[1024];
                    var sb = new StringBuilder();

                    var result = await this._socketClient.ReceiveAsync(new ArraySegment<Byte>(buffer), this._cancellationOwner.Token);

                    switch (result.MessageType)
                    {
                        case WebSocketMessageType.Close:
                            await this._socketClient.CloseAsync(WebSocketCloseStatus.NormalClosure, String.Empty, this._cancellationOwner.Token);
                            break;
                        case WebSocketMessageType.Text:
                            sb.Append(Encoding.UTF8.GetString(buffer, 0, result.Count));
                            while (!result.EndOfMessage)
                            {
                                result = await this._socketClient.ReceiveAsync(new ArraySegment<Byte>(buffer), this._cancellationOwner.Token);
                                sb.Append(Encoding.UTF8.GetString(buffer, 0, result.Count));
                            }

                            var str_buffer = sb.ToString();
                            var jToken = JToken.Parse(str_buffer);

                            if (jToken.HasValues && jToken["type"] != null)
                            {
                                var payloadType = EnumUtils.ToEnum<PayloadType>(jToken["type"].Value<String>(), PayloadType.Unknown);

                                switch (payloadType)
                                {
                                    // {"type":"message_lobby.presence.join","lobby_id":23178,"member_id":2305,"member":{"nickname":"eXpG_Neutron","displayname":"eXpG_Neutron","avatar":"https:\/\/robertsspaceindustries.com\/media\/k7aiwf1sigzm5r\/heap_infobox\/EXpG_Neutron.jpg?v=1488177011","meta":{"signature":"","badges":[{"name":"Grand Admiral","icon":"https:\/\/robertsspaceindustries.com\/media\/lish0be1gvu8xr\/heap_note\/Grand-Admiral.png"},{"name":"Backer","icon":"https:\/\/robertsspaceindustries.com\/rsi\/static\/images\/profile\/icon-backer.png"},{"name":"Explorer-Germany","icon":"https:\/\/robertsspaceindustries.com\/media\/6cedovn1e0wejr\/heap_note\/EXPG-Thumbnail.png","url":"https:\/\/robertsspaceindustries.com\/orgs\/EXPG"}]},"roles":{"1":["4","6","11","12"],"1091":["7641","7643"],"9711":["67976","67977"]},"presence":{"status":"online","info":null,"since":1488293242}}}
                                    case PayloadType.MessageLobby_Presence_Join:
                                        {
                                            var payload = jToken.ToObject<Lobby.Join.Payload>();
                                            var lobby = this._communities.SelectMany(c => c.Lobbies).Where(l => l.Id == payload.LobbyId).FirstOrDefault();
                                            this.MemberPresenceJoin?.Invoke(payload, lobby);
                                            break;
                                        }
                                    // {"type":"message_lobby.presence.leave","lobby_id":23178,"member_id":2305}
                                    case PayloadType.MessageLobby_Presence_Leave:
                                        {
                                            var payload = jToken.ToObject<Lobby.Leave.Payload>();
                                            var lobby = this._communities.SelectMany(c => c.Lobbies).Where(l => l.Id == payload.LobbyId).FirstOrDefault();
                                            this.MemberPresenceLeave?.Invoke(payload, lobby);
                                            break;
                                        }
                                    // {"type":"member.presence.update","member_id":27,"presence":{"status":"away","info":null,"since":1488293110}}
                                    case PayloadType.Member_Presence_Update:
                                        {
                                            var payload = jToken.ToObject<Lobby.Update.Payload>();
                                            var lobby = this._communities.SelectMany(c => c.Lobbies).Where(l => l.Id == payload.LobbyId).FirstOrDefault();
                                            this.MemberPresenceUpdate?.Invoke(payload, lobby);
                                            break;
                                        }
                                    case PayloadType.Message_New:
                                        {
                                            var payload = jToken.ToObject<Message.New.Payload>();
                                            var lobby = payload.Message.Lobby ?? this._communities.SelectMany(c => c.Lobbies).Where(l => l.Id == payload.Message.LobbyId).FirstOrDefault();
                                            this.MessageReceived?.Invoke(payload, lobby);
                                            break;
                                        }
                                    case PayloadType.Unknown:
                                        {
                                            // File.AppendAllText("payload.unknown.log", $"\r\n{str_buffer}");
                                            // Console.WriteLine($"Unknown Payload: {payloadType}");
                                            this.FrameReceived?.Invoke(str_buffer);
                                            break;
                                        }
                                    default:
                                        this.FrameReceived?.Invoke(str_buffer);
                                        break;
                                }
                            }
                            else
                            {
                                this.FrameReceived?.Invoke(str_buffer);
                            }
                            break;
                        case WebSocketMessageType.Binary:
                            throw new NotImplementedException();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        private async Task SpinKeepAlive()
        {
            while (!this._cancellationOwner.IsCancellationRequested && this._socketClient.State == WebSocketState.Open)
            {
                Thread.Sleep(KEEPALIVE_TIMEOUT);

                await this._wsLock.WaitAsync();

                try
                {
                    await this._socketClient.SendAsync(KEEPALIVE, WebSocketMessageType.Text, true, this._cancellationOwner.Token);
                    this.KeepAlive?.Invoke();
                }
                finally
                {
                    this._wsLock.Release();
                }
            }
        }

        public SemaphoreSlim _wsLock = new SemaphoreSlim(1, 1);

        public async Task SendPayloadAsync(WebSocket.Payload payload)
        {
            await this._wsLock.WaitAsync();

            try
            {
                await this._socketClient.SendAsync(new ArraySegment<Byte>(Encoding.ASCII.GetBytes(payload.ToJSON())), WebSocketMessageType.Text, true, this._cancellationOwner.Token);
            }
            finally
            {
                this._wsLock.Release();
            }
        }

        public async Task DisconnectAsync()
        {
            this._cancellationOwner.Cancel(false);

            await this._socketClient.CloseAsync(WebSocketCloseStatus.NormalClosure, "OK", new CancellationTokenSource { }.Token);
        }
    }
}

#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed