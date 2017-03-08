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
    public delegate Task MessageReceivedDelegate(Message.New.Payload message, Session.Lobby lobby);

    public partial class SpectrumClient
    {
        public event MessageReceivedDelegate OnMessageReceived;
    }
}