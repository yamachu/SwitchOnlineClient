using System;
using System.Net.Http;
using System.Threading;

namespace SwitchOnlineClient.Network.SwitchOnlineAccount
{
    internal class SwitchAccountClientHandler : DelegatingHandler
    {
        private string AccessToken;

        public SwitchAccountClientHandler(string accessToken) : this(new HttpClientHandler(), accessToken)
        {
        }

        private SwitchAccountClientHandler(HttpClientHandler handler, string accessToken) : base(handler)
        {
            AccessToken = accessToken;
        }

        protected override System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
			request.Headers.Add("Authorization", $"Bearer {AccessToken}");
			request.Headers.Add("Accept", "application/json");
            return base.SendAsync(request, cancellationToken);
        }
    }
}
