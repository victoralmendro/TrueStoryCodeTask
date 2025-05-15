using System.Net;

namespace TrueStoryCodeTask.Errors
{
    public class InvalidRequestParamsException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public InvalidRequestParamsException(HttpStatusCode statusCode, string message)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
