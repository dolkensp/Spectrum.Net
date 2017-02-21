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
        public async Task<Result<Message.Create.CreateMessageResponse>> SendMessageAsync(Message.Create.CreateMessageRequest message)
        {
            var container = new ObjectContent<Message.Create.CreateMessageRequest>(message, this._mediaTypeFormatter);
            
            var result = await this._apiClient.PostAsync("/api/spectrum/message/create", container);

            var str_result = await result.Content.ReadAsStringAsync();

            return str_result.FromJSON<Result<Message.Create.CreateMessageResponse>>();
        }
    }
}