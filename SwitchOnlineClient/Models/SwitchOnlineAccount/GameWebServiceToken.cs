using System;
using Newtonsoft.Json;

namespace SwitchOnlineClient.Models.SwitchOnlineAccount
{
    public class GameWebServiceToken
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("correlationId")]
        public string CorrelationID { get; set; }

        [JsonProperty("result")]
        public GameWebServiceTokenBody Body { get; set; }

        public class GameWebServiceTokenBody
        {
            [JsonProperty("expiresIn")]
            public int Expires { get; set; }

            [JsonProperty("accessToken")]
            public string AccessToken { get; set; }
        }
	}
}
