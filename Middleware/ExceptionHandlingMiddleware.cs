using System.Security.Authentication;
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
                await _next(context);
            }
            catch (InvalidCredentialException ex)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsJsonAsync(new { message = ex.Message });
            }
            catch (UnauthorizedException ex)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsJsonAsync(new { message = ex.Message });
            }
            catch (NotFoundException ex)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsJsonAsync(new { message = ex.Message });
            }
            catch (AlreadyExistsException ex)
            {
                context.Response.StatusCode = 409;
                await context.Response.WriteAsJsonAsync(new { message = ex.Message });
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.GetType().Name} - {ex.Message}");
                Console.WriteLine(ex.StackTrace);

                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new { message = ex.Message });
            }
        }
    }
}
