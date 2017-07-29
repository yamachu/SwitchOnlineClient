using System;
using Newtonsoft.Json;

namespace Spla2View.Models.NintendoAccount
{
    public class NintendoSessionToken
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("session_token")]
        public string SessionToken { get; set; }
    }
}
