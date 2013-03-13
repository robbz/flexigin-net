using System.Net;

namespace Flexigin.Core
{
    public class ComponentResult
    {
        public string Content { get; set; }
        public string ContentType { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public ComponentResult(HttpStatusCode statusCode, string contentType)
        {
            this.StatusCode = statusCode;
            this.ContentType = contentType;
        }

        public ComponentResult(string content, HttpStatusCode statusCode, string contentType)
        {
            this.Content = content;
            this.StatusCode = statusCode;
            this.ContentType = contentType;
        }
    }
}