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
        public async Task<Result<Search.AutoComplete<Search.Member>>> SearchMembersAsync(UInt64 communityId, String searchTerm = "")
        {
            var payload = new { community_id = communityId, text = searchTerm };

            var container = new ObjectContent(payload.GetType(), payload, this._mediaTypeFormatter);

            var result = await this._apiClient.PostAsync("/api/spectrum/search/member/autocomplete", container);

            var str_result = await result.Content.ReadAsStringAsync();

            return str_result.FromJSON<Result<Search.AutoComplete<Search.Member>>>();
        }
    }
}

namespace Spectrum.Net.Core.Search
{
    public class AutoComplete<TResult>
    {
        [JsonProperty("hits")]
        public Hits<TResult> Hits { get; internal set; }
        [JsonProperty("timed_out")]
        public Boolean TimedOut { get; internal set; }
        [JsonProperty("took")]
        public UInt64 Took { get; internal set; }
    }

    public class Hits<TResult>
    {
        [JsonProperty("max_score")]
        public Double MaxScore { get; internal set; }
        [JsonProperty("total")]
        public UInt64 Total { get; internal set; }
        [JsonProperty("hits")]
        public IEnumerable<SearchResult<TResult>> Results { get; internal set; }
    }

    public class SearchResult<TResult>
    {
        [JsonProperty("_id")]
        public UInt64 Id { get; internal set; }
        [JsonProperty("_index")]
        public String Index { get; internal set; }
        [JsonProperty("_score")]
        public Double Score { get; internal set; }
        [JsonProperty("_source")]
        public TResult Source { get; internal set; }
        [JsonProperty("_type")]
        public String Type { get; internal set; }
    }

    public class Member
    {
        [JsonProperty("id")]
        public UInt64 Id { get; internal set; }
        [JsonProperty("avatar")]
        public String Avatar { get; internal set; }
        [JsonProperty("displayname")]
        public String DisplayName { get; internal set; }
        [JsonProperty("nickname")]
        public String Nickname { get; internal set; }
    }
}