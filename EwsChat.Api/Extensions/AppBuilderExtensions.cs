using EwsChat.Api.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace EwsChat.Api.Extensions
{
    public static class AppBuilderExtensions
    {
        public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
