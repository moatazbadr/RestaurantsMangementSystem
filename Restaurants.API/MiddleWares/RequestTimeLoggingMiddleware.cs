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

            await next(context); // await the next middleware

            stopwatch.Stop();

            var elapsedMs = stopwatch.ElapsedMilliseconds;

            _logger.LogInformation(
                "Request {Method} {Path} executed in {ElapsedMilliseconds} ms",
                context.Request.Method,
                context.Request.Path,
                elapsedMs
            );
        }
    }
}
