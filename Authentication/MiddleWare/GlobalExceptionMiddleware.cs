using System.Text.Json;

namespace Authentication.MiddleWare
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public GlobalExceptionMiddleware(RequestDelegate next){
            _next = next;
        }
        public async Task InvokeAsync(HttpContext ctx)
        {
            try
            {
                await _next(ctx);
            }
            catch(Exception ex)
            {
                ctx.Response.StatusCode = StatusCodes.Status500InternalServerError;
                ctx.Response.ContentType = "application/json";
                var response = new
                {
                    Message = ex.Message
                };
                await ctx.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
        
    }
}