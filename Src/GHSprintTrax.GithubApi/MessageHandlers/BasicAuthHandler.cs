using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GHSprintTrax.GithubApi.MessageHandlers
{
    public class BasicAuthHandler : DelegatingHandler
    {
        private readonly string authHeaderValue;

        public BasicAuthHandler(string username, string password, HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
            authHeaderValue = CreateAuthHeader(username, password);
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
            return base.SendAsync(request, cancellationToken);
        }

        private static string CreateAuthHeader(string username, string password)
        {
            var bytes = Encoding.ASCII.GetBytes(username + ":" + password);
            return Convert.ToBase64String(bytes);
        }
    }
}