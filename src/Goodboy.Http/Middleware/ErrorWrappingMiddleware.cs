using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Goodboy.Http
{
    public class ErrorWrappingMiddleware
    {
        readonly RequestDelegate _next;

        public ErrorWrappingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        async static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (!context.Response.HasStarted)
            {
                if (!context.Response.HasStarted)
                {

                    context.Response.ContentType = "application/json";
                    if ("401 Unauthorized".Equals(exception.Message))
                    {
                        var response = new UnauthorizedResponse(exception.Message, "ERROR_UNAUTHORIZED");

                        var result = JsonConvert.SerializeObject(response, new JsonSerializerSettings
                        {
                            ContractResolver = new CamelCasePropertyNamesContractResolver()
                        });
                        context.Response.StatusCode = 401;
                        await context.Response.WriteAsync(result);
                    }
                    else
                    {
                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = 500;

                        var response = new InternalServerErrorResponse(exception.Message, "ERROR_INTERNAL_EXCEPTION");

                        var result = JsonConvert.SerializeObject(response, new JsonSerializerSettings
                        {
                            ContractResolver = new CamelCasePropertyNamesContractResolver()
                        });
                        await context.Response.WriteAsync(result);
                    }
                }
            }
        }
    }
}
