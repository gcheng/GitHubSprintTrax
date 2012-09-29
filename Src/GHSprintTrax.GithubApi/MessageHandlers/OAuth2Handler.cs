using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http.Headers;

namespace GHSprintTrax.GithubApi.MessageHandlers
{
    public class OAuth2Handler : DelegatingHandler
    {
        private Authorization authorization;

        public OAuth2Handler(Authorization authorization, HttpMessageHandler innerHandler = null)
            : base(innerHandler ?? new HttpClientHandler())
        {
            this.authorization = authorization;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("token", authorization.Token);
            return base.SendAsync(request, cancellationToken);
        }
    }
}
