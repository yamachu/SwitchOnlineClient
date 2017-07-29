using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SwitchOnlineClient.Models.SwitchOnlineAccount
{
    public class GameWebServices
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("correlationId")]
        public string CorrelationID { get; set; }

        [JsonProperty("result")]
        public IEnumerable<GameService> GameServiceList { get; set; }

        public class GameService
        {
            [JsonProperty("id")]
            public long ID { get; set; }

            [JsonProperty("uri")]
            public string URI { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("whiteList")]
            public IEnumerable<string> WhiteList { get; set; }

            [JsonProperty("imageUri")]
            public string ImageURI { get; set; }
        }
    }
}
