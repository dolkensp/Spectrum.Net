using Spectrum.Net.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Create = Spectrum.Net.Core.Message.Create;
using New = Spectrum.Net.Core.Message.New;
using Session = Spectrum.Net.Core.Session;

namespace Spectrum.Net.TestClient
{
    class Program
    {
        static void Main(String[] args) => new Program().RunAsync().GetAwaiter().GetResult();

        private UInt64[] _cigStaff = new UInt64[] { };
        private UInt64[] _cigRoles = new UInt64[] { };
        private Session.Community _community;

        public async Task RunAsync()
        {
            using (var client = new SpectrumClient(deviceId: ConfigurationManager.AppSettings["Spectrum.DeviceId"]))
            {
                var config = await client.LoginAsync(
                    username: ConfigurationManager.AppSettings["Spectrum.Username"], 
                    password: ConfigurationManager.AppSettings["Spectrum.Password"]);

                // client.FrameReceived += Client_OnFrameReceived; // Catch raw frame
                client.MessageReceived += Client_MessageReceived; // Catch new messages
                
                await client.ConnectAsync();

                // Locate Avocado Community
                this._community = config.Data.Communities
                    .Where(c => c.Slug == "AVOCADO")
                    .FirstOrDefault();

                this._cigRoles = this._community.Roles
                    .Where(r => r.Name == "CIG" || r.Name == "Prophet")
                    .Select(r => r.Id)
                    .ToArray();

                // Subscribe to Avocado Lobbies
                await client.SubscribeAsync(this._community.Lobbies.Select(l => l.SubscriptionKey));

                client.KeepAlive += async () =>
                {
                    var buffer = new List<UInt64> { };

                    foreach (var lobby in this._community.Lobbies)
                    {
                        var presences = await client.LoadPresencesAsync(lobby.Id);

                        buffer.AddRange(presences.Data
                            .Where(p => p.Roles.Mapping[this._community.Id].Intersect(this._cigRoles).Any())
                            .Select(p => p.Id));
                    }

                    this._cigStaff = buffer.Distinct().ToArray();
                };

                // var members = await client.SearchMembersAsync(this._community.Id, "j0sh");

                // var privateLobby = await client.LoadPrivateLobbyInfoAsync(4490);

                UInt64 lobbyId = 1; // 21684
                UInt64? initialId = 1; // null;
                UInt64? minId = initialId;

                var history = new Result<Core.Message.History.HistoryResponse> { };
                var extract1 = new List<Core.Message.History.Message> { };
                var extract2 = new List<Core.Message.History.Message> { };
                var extract3 = new List<Core.Message.History.Message> { };

                // while (extract1.Count < 10000 && extract2.Count < 1000 && extract3.Count < 10 && minId != 1)
                // {
                //     history = await client.LoadMessagesAsync(lobbyId, minId, 100); // Load messages from main lobby
                // 
                //     if ((history?.Data?.Messages?.Count() ?? 0) == 0) break; // Exit when we're out of messages
                // 
                //     extract1.AddRange(history.Data.Messages.Where(m => m.HighlightRoleId.HasValue));
                //     extract2.AddRange(history.Data.Messages.Where(m => (m?.Member?.Nickname ?? String.Empty).Contains("Bault")));
                //     extract3.AddRange(history.Data.Messages.Where(m => (m?.PlainText ?? String.Empty).Contains("alluran")));
                // 
                //     minId = history.Data.Messages.Select(m => m.Id).Min();
                // }

                var sendMessage = await client.SendMessageAsync(new Create.CreateMessageRequest
                {
                    ContentState = new Create.ContentStateRequest
                    {
                        Blocks = new Create.ContentBlock[]
                        {
                            new Create.ContentBlock
                            {
                                Text = "hi there :bow_and_arrow:",
                                EntityRanges = new Create.EntityRange[]
                                {
                                    new Create.EntityRange
                                    {
                                        Key = 0,
                                        Length = 15,
                                        Offset = 9,
                                    }
                                }
                            }
                        },
                        EntityMap = new Dictionary<UInt64, Create.Entity>
                        {
                            {
                                0,
                                new Create.Entity
                                {
                                    Data = ":bow_and_arrow:",
                                    Mutability = Mutability.Immutable,
                                    Type = EntityType.Emoji
                                }
                            }
                        }
                    },
                    LobbyId = 20554,
                }); // Send Message

                var softErase = await client.SoftEraseAsync(sendMessage.Data.Id); // Delete Message

                await Task.Delay(-1);

                await client.DisconnectAsync(); // Disconnect WebSocket
            }
        }

        private void Client_MessageReceived(New.Payload payload, Session.Lobby lobby)
        {
            if (lobby?.Type == LobbyType.Private)
            {
                Console.WriteLine($"[DM] {payload.Message.Member.DisplayName}: {payload.Message.PlainText}"); // Write Message
            }
            else
            {
                Console.WriteLine($"[{lobby?.Name ?? "Unknown"}] {payload.Message.Member.DisplayName}: {payload.Message.PlainText}"); // Write Message
            }
        }

        private void Client_OnFrameReceived(String frame)
        {
            // File.AppendAllText("frame.log", $"\r\n{frame}");
        }
    }
}
