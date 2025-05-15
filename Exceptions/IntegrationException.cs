using System.Net;

namespace TrueStoryCodeTask.Errors
{
    public class IntegrationException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public IntegrationException(HttpStatusCode statusCode, string message)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
