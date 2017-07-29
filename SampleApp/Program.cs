using System;
using System.Linq;
using Spla2View.Network.NintendoAccount;
using Spla2View.Network.SwitchOnlineAccount;
using Spla2View.Network.GameWebService;

namespace SampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var oauthUrl = NintendoOAuthService.GenerateOAuthLoginURL();
            Console.WriteLine($"Access this URL: {oauthUrl}");
            Console.WriteLine("SessionTokenCode ? >");
            var sessionTokenCode = Console.ReadLine().Trim();
            Console.WriteLine("SessionTokenCodeVerifier ? >");
            var sessionTokenCodeVerifier = Console.ReadLine().Trim();

            var sessionToken = NintendoOAuthService.GetSessionToken(sessionTokenCode, sessionTokenCodeVerifier).Result.SessionToken;
            Console.WriteLine($"Session Token is {sessionToken}");
            Console.WriteLine("Please save and reuse it");

            var tokens = NintendoOAuthService.GetTokens(sessionToken).Result;

            var accountToken = SwitchAccountService.GetAccountToken(tokens).Result;
            var gameServices = SwitchAccountService.GetGameService().Result;
            var gameServiceToken = SwitchAccountService.GetGameWebServiceToken(gameServices.GameServiceList.First(g => g.Name == "スプラトゥーン2").ID).Result;

            var client = new WebServiceClient("https://app.splatoon2.nintendo.net/", gameServiceToken.Body.AccessToken);
            client.Initialize().Wait();

            var timeline = client.GetAPIResponse("/api/timeline").Result;

            Console.WriteLine(timeline);

            var iksm = client.GetCookieSession("iksm_session");
            Console.WriteLine($"iksm_session: {iksm}");

            Console.ReadKey();
        }
    }
}
