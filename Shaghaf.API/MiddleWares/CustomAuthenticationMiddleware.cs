using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class CustomAuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public CustomAuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);

        if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
        {
            context.Response.ContentType = "application/json";
            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Unauthorized access. Please make sure you are logged in."
            };
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
