using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MinecraftSkins.Domain.Exceptions;

namespace MinecraftSkins.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,Exception exception,CancellationToken cancellationToken)
        {
            var (statusCode, title) = exception switch
            {
                NotFoundException => (StatusCodes.Status404NotFound, "Resource Not Found"),

                UnavailableException => (StatusCodes.Status409Conflict, "Conflict"),

                BusinessException => (StatusCodes.Status400BadRequest, "Business Rule Violation"),

                UnauthenticatedException => (StatusCodes.Status401Unauthorized, "Unauthorized"),

                ExternalServiceException => (StatusCodes.Status502BadGateway, "Service Unavailable"),

                DomainException => (StatusCodes.Status422UnprocessableEntity, "Request validation failed"),

                _ => (StatusCodes.Status500InternalServerError, "Internal Server Error")

            };

            string detail;
            if (statusCode == StatusCodes.Status500InternalServerError)
            {
                detail = "An unexpected error occurred on the server.";
            }
            else
            {
                detail = exception.Message;
            }

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = detail,
                Instance = httpContext.Request.Path 
            };

            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true; 
        }
    }
}
