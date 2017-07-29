using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;

namespace Spla2View.Network.GameWebService
{
    public class WebServiceClient
    {
        private string AccessToken;
        private string ServiceUri;
        private static HttpClient httpClient;
        private HttpClientHandler handler;

        public WebServiceClient(string serviceUri, string accessToken)
        {
            AccessToken = accessToken;
            ServiceUri = serviceUri;
            handler = new HttpClientHandler
            {
                UseCookies = true,
                CookieContainer = new CookieContainer(),
            };

            httpClient = new HttpClient(new WebServiceClientHandler(handler))
            {
                BaseAddress = new Uri(serviceUri)
            };
        }

        async public Task Initialize()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "");
            request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Add("X-gamewebtoken", AccessToken);

            var response = await httpClient.SendAsync(request).ConfigureAwait(false);
            // Do nothing
        }

        async public Task<string> GetAPIResponse(string apiEndpoint)
        {
            var response = await httpClient.GetAsync(apiEndpoint).ConfigureAwait(false);

            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        async public Task<T> GetAPIResponse<T>(string apiEndpoint)
        {
            var responseBody = await GetAPIResponse(apiEndpoint).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(responseBody);
        }

        public string GetCookieSession(string key)
        {
            var cookieCollections = handler.CookieContainer.GetCookies(new Uri(ServiceUri));
            return cookieCollections.Cast<Cookie>().FirstOrDefault(c => c.Name == key)?.Value ?? "";
        }


    }
}
