using System.Net;
using WeatherForecastAPI.Exceptions;

namespace WeatherForecastAPI.Middleware
{
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                httpContext.Response.StatusCode = (int)ExceptionStatusCode.GetExceptionStatusCode(ex);
                httpContext.Response.ContentType = "text/plain";
                string message = ex.Message;
                if(httpContext.Response.StatusCode == 500)
                {
                    message = "Server error";
                }
                await httpContext.Response.WriteAsync(message);
            }
        }
    }
}
