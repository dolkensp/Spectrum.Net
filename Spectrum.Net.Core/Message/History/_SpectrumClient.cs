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
        public async Task<Result<Message.History.HistoryResponse>> LoadMessagesAsync(UInt64 lobbyId, UInt64? before = null, Int32 size = 50)
        {
            var payload = new { lobby_id = lobbyId, before = before, size = size };

            var container = new ObjectContent(payload.GetType(), payload, this._mediaTypeFormatter);

            var result = await this._apiClient.PostAsync("/api/spectrum/message/history", container);

            var str_result = await result.Content.ReadAsStringAsync();

            return str_result.FromJSON<Result<Message.History.HistoryResponse>>();
        }
    }
}