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
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Spectrum.Net.Core
{
    public delegate Task FrameReceivedDelegate(String buffer);
    public delegate Task KeepAliveDelegate();
    public delegate Task ConnectDelegate();
    public delegate Task ExceptionHandlerDelegate(Exception ex);

    public partial class SpectrumClient : IDisposable
    {
        public event FrameReceivedDelegate OnFrameReceived;
        public event KeepAliveDelegate OnKeepAlive;
        public event ConnectDelegate OnConnect;
        public event ConnectDelegate OnDisconnect;
        public event ExceptionHandlerDelegate OnError;

        public SemaphoreSlim _wsLock = new SemaphoreSlim(1, 1);
        public SemaphoreSlim _sendLock = new SemaphoreSlim(1, 1);

        private String _wsToken;
        private String _wsRoot;

        public WebSocketState State { get { return this._socketClient.State; } }

        public async Task ConnectAsync()
        {
            await this.CheckSocket(true);

            this.SpinKeepAlive();
            this.SpinWebSocket();

            await Task.CompletedTask;
        }

        private Int32 _errorCount = 0;

        private async Task CheckSocket(Boolean clean = false)
        {
            if (this._socketClient.State != WebSocketState.Open)
            {
                await this._wsLock.WaitAsync();

                try
                {
                    if (this._socketClient.State != WebSocketState.Open)
                    {
                        if (!clean)
                        {
                            Task.Run(async () =>
                            {
                                this._errorCount++;
                                await Task.Delay(ERROR_TIMEOUT * this._errorCount);
                                this._errorCount--;
                            });

                            await (this.OnDisconnect?.Invoke() ?? Task.CompletedTask);

                            await Task.Delay(MAJOR_TIMEOUT * this._errorCount);

                            if (this._errorCount > 1)
                            {
                                await this.IdentifyAsync(this._rsiToken, this._rsiTokenName);
                            }
                        }

                        Debug.WriteLine($"Connecting to {this._wsRoot}/?token={this._wsToken}");

                        this._socketClient.Dispose();
                        this._socketClient = new ClientWebSocket();

                        await this._socketClient.ConnectAsync(new Uri($"{this._wsRoot}/?token={this._wsToken}"), this._cancellationOwner.Token);

                        await (this.OnConnect?.Invoke() ?? Task.CompletedTask);
                    }
                }
                catch (Exception ex)
                {
                    await (this.OnError?.Invoke(ex) ?? Task.CompletedTask);

                    Task.Run(async () =>
                    {
                        this._errorCount++;
                        await Task.Delay(ERROR_TIMEOUT * this._errorCount);
                        this._errorCount--;
                    });
                    
                    ExceptionDispatchInfo.Capture(ex).Throw();
                }
                finally
                {
                    this._wsLock.Release();
                }
            }
        }

        private async Task SpinWebSocket()
        {
            while (!this._cancellationOwner.IsCancellationRequested)
            {
                try
                {
                    await this.CheckSocket();

                    if (this._socketClient.State == WebSocketState.Open)
                    {
                        var buffer = new Byte[1024];
                        var sb = new StringBuilder();

                        var result = await this._socketClient.ReceiveAsync(new ArraySegment<Byte>(buffer), this._cancellationOwner.Token);

                        switch (result.MessageType)
                        {
                            case WebSocketMessageType.Close:
                                // await this._socketClient.CloseAsync(WebSocketCloseStatus.NormalClosure, String.Empty, this._cancellationOwner.Token);
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
                                                await (this.OnMemberJoin?.Invoke(payload, lobby) ?? Task.CompletedTask);
                                                break;
                                            }
                                        // {"type":"message_lobby.presence.leave","lobby_id":23178,"member_id":2305}
                                        case PayloadType.MessageLobby_Presence_Leave:
                                            {
                                                var payload = jToken.ToObject<Lobby.Leave.Payload>();
                                                var lobby = this._communities.SelectMany(c => c.Lobbies).Where(l => l.Id == payload.LobbyId).FirstOrDefault();
                                                await (this.OnMemberLeave?.Invoke(payload, lobby) ?? Task.CompletedTask);
                                                break;
                                            }
                                        // {"type":"member.presence.update","member_id":27,"presence":{"status":"away","info":null,"since":1488293110}}
                                        case PayloadType.Member_Presence_Update:
                                            {
                                                var payload = jToken.ToObject<Lobby.Update.Payload>();
                                                var lobby = this._communities.SelectMany(c => c.Lobbies).Where(l => l.Id == payload.LobbyId).FirstOrDefault();
                                                await (this.OnMemberUpdate?.Invoke(payload, lobby) ?? Task.CompletedTask);
                                                break;
                                            }
                                        case PayloadType.Message_New:
                                            {
                                                var payload = jToken.ToObject<Message.New.Payload>();
                                                var lobby = payload.Message.Lobby ?? this._communities.SelectMany(c => c.Lobbies).Where(l => l.Id == payload.Message.LobbyId).FirstOrDefault();
                                                await (this.OnMessageReceived?.Invoke(payload, lobby) ?? Task.CompletedTask);
                                                break;
                                            }
                                        case PayloadType.Unknown:
                                            {
                                                // File.AppendAllText("payload.unknown.log", $"\r\n{str_buffer}");
                                                // Console.WriteLine($"Unknown Payload: {payloadType}");
                                                await (this.OnFrameReceived?.Invoke(str_buffer) ?? Task.CompletedTask);
                                                break;
                                            }
                                        default:
                                            await (this.OnFrameReceived?.Invoke(str_buffer) ?? Task.CompletedTask);
                                            break;
                                    }
                                }
                                else
                                {
                                    await (this.OnFrameReceived?.Invoke(str_buffer) ?? Task.CompletedTask);
                                }
                                break;
                            case WebSocketMessageType.Binary:
                                throw new NotImplementedException();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error: {ex.Message}");

                    await (this.OnError?.Invoke(ex) ?? Task.CompletedTask);
                }
            }
        }

        private async Task SpinKeepAlive()
        {
            while (!this._cancellationOwner.IsCancellationRequested)
            {
                await this.CheckSocket();

                if (this._socketClient.State == WebSocketState.Open)
                {
                    this.SendRawPayloadAsync(1);
                    
                    try
                    {
                        await (this.OnKeepAlive?.Invoke() ?? Task.CompletedTask);
                    }
                    catch (Exception ex)
                    {
                        await (this.OnError?.Invoke(ex) ?? Task.CompletedTask);
                    }
                }

                await Task.Delay(KEEPALIVE_TIMEOUT);
            }
        }

        private async Task SendRawPayloadAsync(Object payload)
        {
            await this._sendLock.WaitAsync();

            Exception error = null;

            try
            {
                await this._socketClient.SendAsync(new ArraySegment<Byte>(Encoding.ASCII.GetBytes(payload.ToJSON())), WebSocketMessageType.Text, true, this._cancellationOwner.Token);
            }
            catch (Exception ex)
            {
                error = ex;
            }
            finally
            {
                this._sendLock.Release();
            }

            if (error != null)
            {
                await (this.OnError?.Invoke(error) ?? Task.CompletedTask);
                ExceptionDispatchInfo.Capture(error).Throw();
            }
        }

        public async Task SendPayloadAsync(WebSocket.Payload payload)
        {
            await this.SendRawPayloadAsync(payload);
        }

        public async Task DisconnectAsync()
        {
            // if (!this._cancellationOwner.IsCancellationRequested)
            // {
            //     this._cancellationOwner.Cancel(false);
            // }

            this._socketClient.Dispose();

            await Task.CompletedTask;
        }
    }
}

#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed