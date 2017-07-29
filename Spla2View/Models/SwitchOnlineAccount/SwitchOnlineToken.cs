using System;
using Newtonsoft.Json;

namespace Spla2View.Models.SwitchOnlineAccount
{
    public class SwitchOnlineToken
    {
        [JsonProperty("correlationId")]
        public string CorrelationID { get; set; }

        [JsonProperty("result")]
        public SwitchOnlineTokenBody Body { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        public class SwitchOnlineTokenBody
        {
            [JsonProperty("user")]
            public UserInfo User { get; set; }

            [JsonProperty("webApiServerCredential")]
            public WebAPIServerCredential WebAPICredential { get; set; }

            // firebaseCredential not implemented

            public class UserInfo
            {
                [JsonProperty("id")]
                public long ID { get; set; }

                [JsonProperty("imageUri")]
                public string ImageURI { get; set; }

                [JsonProperty("name")]
                public string Name { get; set; }

                [JsonProperty("supportId")]
                public string SupportID { get; set; }
            }

            public class WebAPIServerCredential
            {
                [JsonProperty("accessToken")]
                public string AccessToken { get; set; }

                [JsonProperty("expiresIn")]
                public int Expires { get; set; }
            }
        }
    }
}
