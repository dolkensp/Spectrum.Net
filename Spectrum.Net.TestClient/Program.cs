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

namespace Spectrum.Net.TestClient
{
    class Program
    {
        static void Main(String[] args) => new Program().RunAsync().GetAwaiter().GetResult();

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

                await client.SubscribeAsync(config.Data.Communities.SelectMany(c => c.Lobbies.Select(l => l.SubscriptionKey)).ToArray()); // Subscribe to all lobbies

                var history = await client.LoadMessagesAsync(1); // Load messages from main lobby

                var sendMessage = await client.SendMessageAsync(new Create.CreateMessageRequest
                {
                    ContentState = new Create.ContentStateRequest
                    {
                        Blocks = new Create.ContentBlockRequest[]
                        {
                            new Create.ContentBlockRequest
                            {
                                Text = "hi there :bow_and_arrow: ",
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

        private void Client_MessageReceived(New.Payload payload)
        {
            Console.WriteLine($"{payload.Message.Member.DisplayName}: {payload.Message.PlainText}"); // Write Message
        }

        private void Client_OnFrameReceived(String frame)
        {
            File.AppendAllText("frame.log", $"\r\n{frame}");
        }
    }
}
