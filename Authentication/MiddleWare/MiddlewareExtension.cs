using System.Runtime.CompilerServices;

namespace Authentication.MiddleWare
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder UseCustomMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionMiddleware>();
            app.UseMiddleware<RequestLoggingMiddleware>();
            return app;
        }
    }
}
