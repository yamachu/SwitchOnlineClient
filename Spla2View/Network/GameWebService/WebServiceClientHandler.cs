using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Spla2View.Network.GameWebService
{
    public class WebServiceClientHandler : DelegatingHandler
    {
        public WebServiceClientHandler(HttpClientHandler handler) : base(handler)
        {
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("Accept", "application/json");
            return base.SendAsync(request, cancellationToken);
        }
    }
}
