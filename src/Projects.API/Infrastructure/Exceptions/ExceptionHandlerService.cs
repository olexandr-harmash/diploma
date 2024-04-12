using Microsoft.AspNetCore.Diagnostics;

namespace diploma.Projects.API.Infrastructure.Exceptions;

public class ExceptionHandlerService : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        httpContext.Response.ContentType = "application/json";

        var contextFeature = httpContext.Features.Get<IExceptionHandlerFeature>();

        if (contextFeature != null)
        {
            httpContext.Response.StatusCode = contextFeature.Error switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                BadRequestException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError,
            };

            var errorMessage = contextFeature.Error is not (NotFoundException or BadRequestException) ? 
                "Internal server error." : 
                contextFeature.Error.Message;

            await httpContext.Response.WriteAsync(
                new ExceptionDetails(errorMessage, httpContext.Response.StatusCode)
                .ToString());
        }

        return true;
    }
}
