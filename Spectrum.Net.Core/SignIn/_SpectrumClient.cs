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
        public async Task<Result<Session.Response>> LoginAsync(String username, String password, String tokenName = "x-rsi-token")
        {
            Result<Session.Response> identify = null;

            if (File.Exists(TOKEN_CACHE))
            {
                this._rsiToken = File.ReadAllText(TOKEN_CACHE);

                identify = await this.IdentifyAsync(this._rsiToken, tokenName);
            }

            if (String.IsNullOrWhiteSpace(identify?.Data?.Token))
            {
                this._apiClient.DefaultRequestHeaders.Remove("Cookie");
                this._apiClient.DefaultRequestHeaders.Add("Cookie", $"_rsi_device={this._deviceId}");

                var payload = new SignIn.Request { Username = username, Password = password, Remember = 0 };
                var container = new ObjectContent<SignIn.Request>(payload, this._mediaTypeFormatter);

                var result = await this._apiClient.PostAsync("/api/account/signin", container);

                var str_result = await result.Content.ReadAsStringAsync();

                var signin = str_result.FromJSON<Result<SignIn.Response>>();

                if (signin.Data != null)
                {
                    File.WriteAllText(TOKEN_CACHE, signin.Data.SessionId);

                    this._rsiTokenName = $"x-{signin.Data.SessionName}";
                    this._rsiToken = signin.Data.SessionId;
                }

                identify = await this.IdentifyAsync(this._rsiToken, tokenName);
            }

            return identify;
        }
    }
}