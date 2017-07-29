using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Spla2View.Network.NintendoAccount
{
    internal class NintendoAccountClientHandler : DelegatingHandler
    {
        // Notice: Versionが上がったら変えないと
        // ToDo: 簡単に変えることが出来るインタフェースの実装
        private const string UserAgent = "OnlineLounge/1.0.4 NASDKAPI Android";

        public NintendoAccountClientHandler() : this(new HttpClientHandler())
        {
        }

        private NintendoAccountClientHandler(HttpClientHandler handler) : base(handler)
        {
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("User-Agent", UserAgent);
            request.Headers.Add("Accept", "application/json");
            return base.SendAsync(request, cancellationToken);
        }
    }
}
