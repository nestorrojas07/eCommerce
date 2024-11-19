using Microsoft.AspNetCore.Mvc;
using Shared.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace Order.API.Middlewares;

public class GloblalExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<GloblalExceptionHandlingMiddleware> _logger;

    public GloblalExceptionHandlingMiddleware(ILogger<GloblalExceptionHandlingMiddleware> logger) => _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            string body = "";
            switch(e)
            {
                case KeyNotFoundException err:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    body = e.Message;
                    break;
                case DomainException ex:
                    
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    ProblemDetails problemDom = new()
                    {
                        Status = (int)HttpStatusCode.InternalServerError,
                        Type = "Internal Error",
                        Title = "Process Error",
                        Detail = ex.Message
                    };
                    body = JsonSerializer.Serialize(problemDom);
                    context.Response.ContentType = "application/json";
                    break;
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                    ProblemDetails problem = new()
                    {
                        Status = (int)HttpStatusCode.InternalServerError,
                        Type = "Server Error",
                        Title = "Server Error",
                        Detail = "An internal server has ocurred."
                    };
                    body = JsonSerializer.Serialize(problem);
                    context.Response.ContentType = "application/json";
                    break;
            } 

            

            await context.Response.WriteAsync(body);
        }
    }
}
