using System.Diagnostics;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Realtor.Application.Behaviors
{
    public class RequestPerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<TRequest> _logger;
        private readonly Stopwatch _timer;
        private readonly IPrincipal _user;

        public RequestPerformanceBehavior(ILogger<TRequest> logger, IPrincipal user)
        {
            _timer = new Stopwatch();
            _logger = logger;
            _user = user;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();

            TResponse response = await next();

            _timer.Stop();

            if (_timer.ElapsedMilliseconds <= 500)
            {
                return response;
            }

            string name = typeof(TRequest).Name;

            _logger.LogWarning(
                "Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) | Payload: {@Request} | User: {User}", name,
                _timer.ElapsedMilliseconds, request, _user?.Identity?.Name);

            return response;
        }
    }
}
