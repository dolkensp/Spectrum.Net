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
    public partial class SpectrumClient
    {
        public async Task<Result<Session.Response>> IdentifyAsync(String token, String tokenName = "x-rsi-token")
        {
            this._rsiToken = token;
            this._rsiTokenName = tokenName;

            var container = new ObjectContent<Object>(new Object { }, this._mediaTypeFormatter);

            this._apiClient.DefaultRequestHeaders.Clear();

            this._apiClient.DefaultRequestHeaders.Add("User-Agent", $"Spectrum.Net/{Assembly.GetExecutingAssembly().GetName().Version.Major}.{Assembly.GetExecutingAssembly().GetName().Version.Minor}.{Assembly.GetExecutingAssembly().GetName().Version.MajorRevision} Spectrum/{SpectrumClient.SPECTRUM_VERSION}");
            this._apiClient.DefaultRequestHeaders.Add("Origin", this._baseAddress);
            this._apiClient.DefaultRequestHeaders.Add("Referer", $"{this._baseAddress}/Spectrum");
            this._apiClient.DefaultRequestHeaders.Add("Cookie", $"_rsi_device={this._deviceId}; Rsi-Token={this._rsiToken}");
            this._apiClient.DefaultRequestHeaders.Add(this._rsiTokenName, this._rsiToken);
            this._apiClient.DefaultRequestHeaders.Add("x-tavern-id", SpectrumClient.TAVERN_ID);

            var result = await this._apiClient.PostAsync("/api/spectrum/auth/identify", container);

            var str_result = await result.Content.ReadAsStringAsync();

            var obj_result = str_result.FromJSON<Result<Session.Response>>();

            this._wsToken = obj_result.Data.Token;
            this._wsRoot = obj_result.Data.Config.BroadcasterWebsocketUrl;

            return obj_result;
        }
    }
}