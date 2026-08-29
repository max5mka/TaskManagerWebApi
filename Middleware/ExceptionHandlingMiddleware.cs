using TaskManagerWebApi.Exceptions;

namespace TaskManagerWebApi.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                Console.WriteLine("TRY BEFORE INSIDE");
                await _next(context);
                Console.WriteLine("TRY AFTER INSIDE");
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine("1ST CATCH INSIDE");
                context.Response.StatusCode = 404;
                await context.Response.WriteAsJsonAsync(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine("2ND CATCH INSIDE");
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new { message = "Internal server error" });
            }
        }
    }
}
