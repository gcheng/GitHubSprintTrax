using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace GHSprintTrax.GithubApi.MessageHandlers
{
    public class OAuth2Handler : DelegatingHandler
    {
        private readonly Authorization authorization;

        public OAuth2Handler(Authorization authorization, HttpMessageHandler innerHandler = null)
            : base(innerHandler ?? new HttpClientHandler())
        {
            this.authorization = authorization;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("token", authorization.Token);
            return base.SendAsync(request, cancellationToken);
        }
    }
}