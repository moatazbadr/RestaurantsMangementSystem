
using Restaurant.Domain.Exceptions;

namespace Restaurants.API.MiddleWares;

public class ErrorMiddleWare : IMiddleware
{
    private readonly ILogger<ErrorMiddleWare> _logger;

    public ErrorMiddleWare(ILogger<ErrorMiddleWare> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (NotFoundException NotFoundException)
        {
            _logger.LogWarning(NotFoundException, NotFoundException.Message);
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsync(NotFoundException.Message);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync("An unexpected error occurred. Please try again later.");
        }
    }
}
