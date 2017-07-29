using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using Spla2View.Models.NintendoAccount;
using Spla2View.Models.SwitchOnlineAccount;

namespace Spla2View.Network.SwitchOnlineAccount
{
    public class SwitchAccountService
    {
        private const string SwitchAccountServiceHost = "api-lp1.znc.srv.nintendo.net";

        private static HttpClient httpClient;

        // https://gbatemp.net/threads/pulling-splatoon2-related-data-from-the-nintendo-switch-online-app.478464/#post-7467483
        async public static Task<SwitchOnlineToken> GetAccountToken(NintendoToken token)
        {
            var client = new HttpClient
            {
                DefaultRequestHeaders = {
                    Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken),
                }
            };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var contentBody = new
            {
                parameter = new {
                    language = "null",
                    naBirthday = "null",
                    naCountry = "null",
                    naIdToken = token.IDToken
                }
            };

            var content = JsonConvert.SerializeObject(contentBody);

            var response = await client.PostAsync(string.Format("https://{0}/v1/Account/GetToken", SwitchAccountServiceHost),
                                                     new StringContent(content, Encoding.UTF8, "application/json"));

            var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            var onlineToken = JsonConvert.DeserializeObject<SwitchOnlineToken>(responseBody);

            httpClient = new HttpClient(new SwitchAccountClientHandler(onlineToken.Body.WebAPICredential.AccessToken));

            return onlineToken;
        }

        async public static Task<GameWebServices> GetGameService()
        {
            var response = await httpClient.PostAsync(string.Format("https://{0}/v1/Game/ListWebServices", SwitchAccountServiceHost), null);

            var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<GameWebServices>(responseBody);
        }

        async public static Task<GameWebServiceToken> GetGameWebServiceToken(long serviceId)
        {
            var contentBody = new
            {
                parameter = new
                {
                    id = serviceId
                }
            };

            var content = JsonConvert.SerializeObject(contentBody);

            var response = await httpClient.PostAsync(string.Format("https://{0}/v1/Game/GetWebServiceToken", SwitchAccountServiceHost),
                                                     new StringContent(content, Encoding.UTF8, "application/json"));

            var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<GameWebServiceToken>(responseBody);
        }
    }
}
