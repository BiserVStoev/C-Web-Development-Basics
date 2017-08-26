namespace SimpleHttpServer.Models
{
    using Enums;
    using System;
    using System.Text;

    public class HttpResponse
    {
        public ResponseStatusCode StatusCode { get; set; }
        public string StatusMessage
        {
            get
            {
                return Enum.GetName(typeof(ResponseStatusCode), this.StatusCode);
            }
        }
        public byte[] Content { get; set; }

        public Header Header { get; set; }
        public string ContentAsUTF8
        {
            set
            {
                this.Content = Encoding.UTF8.GetBytes(value);
            }
        }

        public HttpResponse()
        {
            this.Header = new Header(HeaderType.HttpResponse);
            this.Content = new byte[] { };
        }
        public override string ToString()
        {
            return string.Format("HTTP/1.0 {0} {1}\r\n{2}",
                (int)this.StatusCode,
                this.StatusMessage,
                this.Header);
        }
    }
}
