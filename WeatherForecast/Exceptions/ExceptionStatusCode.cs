using System.Net;

namespace WeatherForecastAPI.Exceptions
{
    public class ExceptionStatusCode
    {
        private static Dictionary<Type, HttpStatusCode> exceptionStatusCodes = new Dictionary<Type, HttpStatusCode>
        {
            {typeof(Exception), HttpStatusCode.InternalServerError},
            {typeof(CityNotFoundException), HttpStatusCode.NotFound},
            {typeof(InvalidDateException), HttpStatusCode.BadRequest}
        };

        public static HttpStatusCode GetExceptionStatusCode(Exception ex)
        {
            bool exceptionFound = exceptionStatusCodes.TryGetValue(ex.GetType(), out var statusCode);
            return exceptionFound ? statusCode : HttpStatusCode.InternalServerError;
        }
    }
}
