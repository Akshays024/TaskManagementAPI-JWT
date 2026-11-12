namespace Authentication.MiddleWare
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        public RequestLoggingMiddleware (RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext ctx)
        {
            Console.WriteLine($"Request: {ctx.Request.Method} {ctx.Request.Path}");
            ctx.Response.OnCompleted(() =>
            {
                Console.WriteLine($"Response:{ctx.Response.StatusCode}");
                return Task.CompletedTask;
            });
            await _next(ctx);
        }
    }
}