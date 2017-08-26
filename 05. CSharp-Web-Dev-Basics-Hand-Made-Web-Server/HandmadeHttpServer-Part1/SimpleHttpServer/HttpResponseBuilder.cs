namespace SimpleHttpServer
{
    using System.IO;
    using Enums;
    using Models;

    public class HttpResponseBuilder
    {
        public static HttpResponse InternalServerError()
        {
            string content = File.ReadAllText("Resources/Pages/500.html");

            var response = new HttpResponse()
            {
                StatusCode = ResponseStatusCode.InternalServerError,
                ContentAsUTF8 = content
            };

            return response;
        }

        public static HttpResponse NotFound()
        {
            string content = File.ReadAllText("Resources/Pages/404.html");

            var response = new HttpResponse()
            {
                StatusCode = ResponseStatusCode.NotFound,
                ContentAsUTF8 = content
            };

            return response;
        }
    }
}
