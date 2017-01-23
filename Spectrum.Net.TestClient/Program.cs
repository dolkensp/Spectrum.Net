using Spectrum.Net.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.TestClient
{
    class Program
    {
        static void Main(String[] args) => new Program().RunAsync().GetAwaiter().GetResult();

        public async Task RunAsync()
        {
            using (var client = new SpectrumClient(@"https://ptu.cloudimperiumgames.com"))
            {
                var signin = await client.LoginAsync(ConfigurationManager.AppSettings["Spectrum.Username"], ConfigurationManager.AppSettings["Spectrum.Password"]);
                var identify = await client.IdentifyAsync();

                // var identify = await client.IdentifyAsync(ConfigurationManager.AppSettings["Token.User"], "x-rsi-ptu-token"); // Alternate way to log in directly - tokens expire

                // client.FrameReceived += Client_OnFrameReceived; // Catch raw frame
                client.MessageReceived += Client_MessageReceived; // Catch new messages

                await client.ConnectAsync(); // Connect to spectrum - MUST call client.IdentifyAsync first

                await client.SubscribeAsync(identify.Data.Communities.SelectMany(c => c.Lobbies.Select(l => l.SubscriptionKey)).ToArray()); // Subscribe to all lobbies

                var history = await client.LoadMessagesAsync(1); // Load messages from main lobby

                var sendMessage = await client.SendMessageAsync(new Message
                {
                    ContentState = new ContentState
                    {
                        Blocks = new ContentBlock[]
                        {
                            new ContentBlock
                            {
                                Key = ContentBlock.NewKey(),
                                Text = "Test Two",
                            }
                        },
                    },
                    LobbyId = 2573,
                }); // Send Message

                var softErase = await client.SoftEraseAsync(sendMessage.Data.Id); // Delete Message

                await Task.Delay(-1);

                await client.DisconnectAsync(); // Disconnect WebSocket
            }
        }

        private void Client_MessageReceived(MessagePayload payload)
        {
            Console.WriteLine($"{payload.Message.Member.DisplayName}: {payload.Message.PlainText}"); // Write Message
        }

        private void Client_OnFrameReceived(String frame)
        {
            // Console.WriteLine(frame);
        }
    }
}
