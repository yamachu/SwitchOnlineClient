using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using SwitchOnlineClient.Models.NintendoAccount;
using SwitchOnlineClient.Utils;

namespace SwitchOnlineClient.Network.NintendoAccount
{
    public class NintendoOAuthService
    {
        private const string AndroidInnerClientID = "npf71b963c1b7b6d119";
        private const string AndroidClientID = "71b963c1b7b6d119";
        private const string NintendoOAuthHost = "accounts.nintendo.com";

        private static HttpClient httpClient = new HttpClient(new NintendoAccountClientHandler());

        public static string GenerateOAuthLoginURL(string _redirectURI = "")
        {
            var oauthTryState = Hash.GenerateRandomToken(50);
            var sessionChallengeCode = Hash.String2Hash256(Hash.GenerateRandomToken((50)));

            // Change localhost callback?
            var redirectURI = _redirectURI == ""
                            ? WebUtility.UrlEncode(AndroidInnerClientID) + "://auth"
                            : _redirectURI;

            var clientID = WebUtility.UrlEncode(AndroidClientID);
            var oauthScope = string.Join(" ", new string[]{
                "openid",
                "user",
                "user.birthday",
                "user.mii",
                "user.screenName",
            });
            var responseType = "session_token_code";
            var challengeMethod = "S256";
            var loginTheme = "login_form";

            var query = string.Format("state={0}", oauthTryState);
            query += string.Format("&redirect_uri={0}", redirectURI);
            query += string.Format("&client_id={0}", clientID);
            query += string.Format("&scope={0}", oauthScope);
            query += string.Format("&response_type={0}", responseType);
            query += string.Format("&session_token_code_challenge={0}", sessionChallengeCode);
            query += string.Format("&session_token_code_challenge_method={0}", challengeMethod);
            query += string.Format("&theme={0}", loginTheme);

            var uri = string.Format("https://{0}/connect/1.0.0/authorize?{1}",
                                    NintendoOAuthHost,
                                    query);

            return Uri.EscapeUriString(uri);
        }

        async public static Task<NintendoSessionToken> GetSessionToken(string sessionTokenCode, string sessionTokenCodeVerifier)
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "client_id", AndroidClientID },
                { "session_token_code", sessionTokenCode },
                { "session_token_code_verifier", sessionTokenCodeVerifier },
            });

            var response = await httpClient.PostAsync(string.Format("https://{0}/connect/1.0.0/api/session_token", NintendoOAuthHost),
                                 content).ConfigureAwait(false);
            var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<NintendoSessionToken>(responseBody);
        }

        async public static Task<NintendoToken> GetTokens(string sessionToken)
        {
            var contentBase = new Dictionary<string, string>
            {
                { "client_id", AndroidClientID },
                { "session_token", sessionToken },
                { "grant_type", "urn:ietf:params:oauth:grant-type:jwt-bearer-session-token"}
            };

            var content = JsonConvert.SerializeObject(contentBase);

            var response = await httpClient.PostAsync(string.Format("https://{0}/connect/1.0.0/api/token", NintendoOAuthHost),
                                                      new StringContent(content, Encoding.UTF8, "application/json"));

            var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<NintendoToken>(responseBody);
        }
    }
}
