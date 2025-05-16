using System.Net;
using TrueStoryCodeTask.DTOs;
using TrueStoryCodeTask.Errors;
using TrueStoryCodeTask.Services;

namespace TrueStoryCodeTask.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (InvalidRequestParamsException ex)
            {
                _logger.LogError(ex, "Invalid request parameters");

                context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(new BaseErrorResponse
                {
                    Code = ErrorCodes.InvalidRequestParameters,
                    Message = ex.Message,
                    TraceId = context.TraceIdentifier
                });
            }
            catch (IntegrationException ex)
            {
                _logger.LogError(ex, "Integration error occurred");

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(new BaseErrorResponse
                {
                    Code = ErrorCodes.IntegrationError,
                    TraceId = context.TraceIdentifier
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(new BaseErrorResponse
                {
                    Code = ErrorCodes.UnexpectedError,
                    TraceId = context.TraceIdentifier
                });
            }
        }
    }
}
