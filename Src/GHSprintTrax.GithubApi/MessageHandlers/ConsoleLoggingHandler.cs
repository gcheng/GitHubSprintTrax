using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace GHSprintTrax.GithubApi.MessageHandlers
{
    public class ConsoleLoggingHandler : DelegatingHandler
    {
        public ConsoleLoggingHandler(HttpMessageHandler nextHandler = null)
            : base(nextHandler ?? new HttpClientHandler())
        {
            
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return base.SendAsync(request, cancellationToken);
        }
    }
}
