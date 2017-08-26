namespace SimpleHttpServer.Models
{
    using Enums;
    using System;

    public class Route
    {
        public string Name { get; set; }
        public string UrlRegex { get; set; }
        public RequestMethod Method { get; set; }
        public Func<HttpRequest, HttpResponse> Callable { get; set; }
    }
}
