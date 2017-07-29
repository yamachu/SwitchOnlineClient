using System;
using Newtonsoft.Json;

namespace Spla2View.Models.NintendoAccount
{
    public class NintendoToken
    {
        public static NintendoToken Generate(string accessToken, string idToken)
        {
            var instance = new NintendoToken();
            instance.AccessToken = accessToken;
            instance.IDToken = idToken;

            return instance;
        }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("id_token")]
        public string IDToken { get; set; }

        [JsonProperty("expires_in")]
        public int Expire { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        // scope not implemented
    }
}
