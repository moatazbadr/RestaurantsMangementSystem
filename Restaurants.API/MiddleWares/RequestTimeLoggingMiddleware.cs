using System.Diagnostics;

namespace Restaurants.API.MiddleWares
{
    public class RequestTimeLoggingMiddleware : IMiddleware
    {
        private readonly ILogger<RequestTimeLoggingMiddleware> _logger;

        public RequestTimeLoggingMiddleware(ILogger<RequestTimeLoggingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var stopwatch = Stopwatch.StartNew();

            await next(context);

            stopwatch.Stop();
            var elapsedMs = stopwatch.ElapsedMilliseconds;

            if (elapsedMs > 4000) // only log if request took more than 4 seconds
            {
                _logger.LogWarning(
                    "⚠️ Slow Request Detected: {Method} {Path} took {ElapsedMilliseconds} ms",
                    context.Request.Method,
                    context.Request.Path,
                    elapsedMs
                );
            }
        }
    }
}
