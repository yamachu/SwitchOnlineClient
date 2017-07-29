using System;
using System.Linq;
using SwitchOnlineClient.Network.NintendoAccount;
using SwitchOnlineClient.Models.NintendoAccount;
using SwitchOnlineClient.Network.SwitchOnlineAccount;
using SwitchOnlineClient.Network.GameWebService;

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

            var sessionTokenModel = NintendoOAuthService.GetSessionToken(sessionTokenCode, sessionTokenCodeVerifier).Result;
            var sessionToken = sessionTokenModel.SessionToken;
            Console.WriteLine($"Session Token is {sessionToken}");
            Console.WriteLine("Please save and reuse it");

            var tokens = NintendoOAuthService.GetTokens(sessionToken).Result;

            var accountToken = SwitchAccountService.GetAccountToken(tokens).Result;
            var gameServices = SwitchAccountService.GetGameService().Result;

            var spl2 = gameServices.GameServiceList.First(g => g.Name == "スプラトゥーン2");

            var gameServiceToken = SwitchAccountService.GetGameWebServiceToken(spl2.ID).Result;

            var client = new WebServiceClient(spl2.URI, gameServiceToken.Body.AccessToken);
            client.Initialize().Wait();

            var timeline = client.GetAPIResponse("/api/timeline").Result;

            Console.WriteLine(timeline);

            var iksm = client.GetCookieSession("iksm_session");
            Console.WriteLine($"iksm_session: {iksm}");

            Console.ReadKey();
        }
    }
}
