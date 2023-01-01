using System.Text.Json;
using Example.BookStore.Catalog;

namespace Example.BookStore.WebApi;

public class ExceptionHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException validationException)
        {
            Console.WriteLine(validationException);
            var statusCode = StatusCodes.Status400BadRequest;
            var response = new
            {
                title = validationException.GetType().Name,
                status = statusCode,
                detail = validationException.Message,
                errors = validationException.Errors
            };
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            var statusCode = StatusCodes.Status400BadRequest;
            var response = new
            {
                title = e.GetType().Name,
                status = statusCode,
                detail = e.Message
            };
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}