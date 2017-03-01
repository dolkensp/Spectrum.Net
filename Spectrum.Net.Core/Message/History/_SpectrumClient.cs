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
        /// <summary>
        /// Load messages from a lobby, up to 100 at a time.
        /// </summary>
        /// <param name="lobbyId">The lobby to load messages from.</param>
        /// <param name="before">The ID of the message to load messages before, or null for the most recent messages.</param>
        /// <param name="batchSize">The number of messages to retrieve at once, up to 100.</param>
        /// <returns>A list of messages sent immediately before the indicated message id.</returns>
        public async Task<Result<Message.History.HistoryResponse>> LoadMessagesAsync(UInt64 lobbyId, UInt64? before = null, Byte batchSize = 50)
        {
            // Validate parameters
            if (batchSize > 100) throw new ArgumentOutOfRangeException("batchSize", batchSize, "Parameter 'batchSize' must be no larger than 100");

            // Subtract 1 from 'before to ensure' we're actually retrieving messages BEFORE the supplied value
            if (before.HasValue) before--;

            var payload = new { lobby_id = lobbyId, before = before, size = batchSize };

            var container = new ObjectContent(payload.GetType(), payload, this._mediaTypeFormatter);

            var result = await this._apiClient.PostAsync("/api/spectrum/message/history", container);

            var str_result = await result.Content.ReadAsStringAsync();

            return str_result.FromJSON<Result<Message.History.HistoryResponse>>();
        }
    }
}