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
        public async Task SubscribeAsync(String[] subscriptionKeys, SubscriptionScope scope = SubscriptionScope.Content)
        {
            await this.SendPayloadAsync(new Subscribe.Payload
            {
                Type = PayloadType.Subscribe,
                SubscriptionScope = scope,
                SubscriptionKeys = subscriptionKeys
            });
        }
    }
}