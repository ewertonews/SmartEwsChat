using EwsChat.Data.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace EwsChat.Api.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleException(httpContext, ex);
            }
        }

        private async Task HandleException(HttpContext context, Exception error)
        {
            context.Response.ContentType = "application/json";

            switch (error)
            {
                case UserNotFoundException _:
                case RoomNotFoundException _:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;

                case InvalidMessageException _:
                case UserAlreadyExistsException _:
                case NullReferenceException _:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;

            }
            _logger.LogError(error.Message);
            var result = JsonSerializer.Serialize(new { message = error?.Message });
            await context.Response.WriteAsync(result);

        }
    }
}
