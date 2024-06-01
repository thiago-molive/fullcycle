using Microsoft.AspNetCore.Diagnostics;

namespace EDA_FC3.Midlewares;

public class ExceptionMidleare : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var problemDetails = new
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Server Error",
            Message = exception.Message
        };

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
