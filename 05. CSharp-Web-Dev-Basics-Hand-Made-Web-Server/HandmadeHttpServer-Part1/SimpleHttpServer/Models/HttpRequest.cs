﻿namespace SimpleHttpServer.Models
{
    using Enums;

    public class HttpRequest
    {
        public HttpRequest()
        {
            this.Header = new Header(HeaderType.HttpRequest);

        }

        public RequestMethod Method { get; set; }

        public string Url { get; set; }

        public string Content { get; set; }

        public Header Header { get; set; }

        public override string ToString()
        {
            string httpRequest = $"{this.Method} {this.Url} HTTP/1.0\r\n{this.Header}{this.Content}";

            return httpRequest;
        }
    }
}
