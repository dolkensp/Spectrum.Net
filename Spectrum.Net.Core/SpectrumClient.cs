using Newtonsoft.Json;
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
    public partial class SpectrumClient : IDisposable
    {
        private CookieContainer _cookieContainer;
        private HttpClient _apiClient;
        private ClientWebSocket _socketClient = new ClientWebSocket();
        private CancellationTokenSource _cancellationOwner = new CancellationTokenSource { };
        private MediaTypeFormatter _mediaTypeFormatter = new JsonMediaTypeFormatter { };

        private static readonly String TOKEN_CACHE = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "token.cache");
        private const String TAVERN_ID = "il8ce9ddea971"; // 52dzt61b6oyss
        private const String SPECTRUM_VERSION = "2.6.1";

        public static readonly Int32 KEEPALIVE_TIMEOUT = 30000;
        public static readonly ArraySegment<Byte> KEEPALIVE = new ArraySegment<Byte>(new Byte[] { 8 });

        private String _rsiTokenName;
        private String _rsiToken;
        private String _baseAddress;
        private String _deviceId;

        public SpectrumClient(String baseAddress = @"https://robertsspaceindustries.com", String deviceId = null)
        {
            this._baseAddress = baseAddress;
            this._deviceId = deviceId;

            this._cookieContainer = new CookieContainer { };

            var handler = new HttpClientHandler
            {
                CookieContainer = this._cookieContainer
            };

            this._apiClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(this._baseAddress)
            };

            if (!String.IsNullOrWhiteSpace(this._deviceId)) this._cookieContainer.Add(this._apiClient.BaseAddress, new Cookie("_rsi_device", this._deviceId));
        }
        
        public void Dispose()
        {
            if (this._socketClient.State == WebSocketState.Open) this.DisconnectAsync().Wait();

            this._socketClient.Dispose();

            this._apiClient.Dispose();
        }
    }
}