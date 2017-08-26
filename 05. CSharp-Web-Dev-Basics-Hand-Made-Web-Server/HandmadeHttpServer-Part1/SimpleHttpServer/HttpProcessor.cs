using System.IO;

namespace SimpleHttpServer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Sockets;
    using System.Text;
    using System.Text.RegularExpressions;
    using Enums;
    using Models;

    public class HttpProcessor
    {
        private IList<Route> Routes;
        private HttpRequest Request;
        private HttpResponse Response;

        public HttpProcessor(IEnumerable<Route> routes)
        {
            this.Routes = new List<Route>(routes);
        }

        public void HandleClient(TcpClient tcpClient)
        {
            using (var networkStream = tcpClient.GetStream())
            {
                this.Request = this.GetRequest(networkStream);
                this.Response = this.RouteRequest();
                StreamUtils.WriteResponse(networkStream, this.Response);
            }
        }

        private HttpResponse RouteRequest()
        {
            var routes = this.Routes
               .Where(x => Regex.Match(Request.Url, x.UrlRegex).Success)
               .ToList();

            if (!routes.Any())
            {
                return HttpResponseBuilder.NotFound();
            }
                
            var route = routes.SingleOrDefault(x => x.Method == this.Request.Method);

            if (route == null)
            {
                return new HttpResponse()
                {
                    StatusCode = ResponseStatusCode.MethodNotAllowed
                };
            }
            
            try
            {
                return route.Callable(Request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);

                return HttpResponseBuilder.InternalServerError();
            }

        }

        private HttpRequest GetRequest(Stream inputStream)
        {
            string requestLine = StreamUtils.ReadLine(inputStream);
            string[] data = requestLine.Split(' ');
            if (data.Length != 3)
            {
                throw new Exception("invalid http request line");
            }

            RequestMethod method = (RequestMethod)Enum.Parse(typeof(RequestMethod), data[0].ToUpper());
            string url = data[1];
            string protocolVersion = data[2];

            Header header = new Header(HeaderType.HttpRequest);
            string line;
            while ((line = StreamUtils.ReadLine(inputStream)) != null)
            {
                if (line.Equals(""))
                {
                    break;
                }

                if (!line.Contains(':'))
                {
                    throw new Exception("invalid http header line: " + line);
                }

                var headerData = line.Split(':');
                var name = headerData[0].Trim();
                var value = headerData[1].Trim();
                if (headerData[0] == "Cookie")
                {
                    string[] cookies = value.Split(';');
                    foreach (var cookieData in cookies)
                    {
                        var pair = cookieData.Split('=').Select(e => e.Trim()).ToArray();
                        var cookie = new Cookie(pair[0], pair[1]);
                        header.Cookies.Add(cookie);
                    }
                }
                //int separator = line.IndexOf(':');
                //if (separator == -1)
                //{
                //    throw new Exception("invalid http header line: " + line);
                //}
                //string name = line.Substring(0, separator);
                //int pos = separator + 1;
                //while ((pos < line.Length) && (line[pos] == ' '))
                //{
                //    pos++;
                //}

                //string value = line.Substring(pos, line.Length - pos);
                //if (name == "Cookie")
                //{
                //    string[] cookieSaves = value.Split(';');
                //    foreach (var cookieSave in cookieSaves)
                //    {
                //        string[] cookiePair = cookieSave.Split('=').Select(x => x.Trim()).ToArray();
                //        var cookie = new Cookie(cookiePair[0], cookiePair[1]);
                //        header.AddCookie(cookie);
                //    }
                //}
                else if (name == "Content-Length")
                {
                    header.ContentLength = value;
                }
                else
                {
                    header.OtherParameters.Add(name, value);
                }
            }

            string content = null;
            if (header.ContentLength != null)
            {
                int totalBytes = Convert.ToInt32(header.ContentLength);
                int bytesleft = totalBytes;
                byte[] bytes = new byte[totalBytes];

                while (bytesleft > 0)
                {
                    byte[] buffer = new byte[bytesleft > 1024 ? 1024 : bytesleft];
                    int n = inputStream.Read(buffer, 0, buffer.Length);
                    buffer.CopyTo(bytes, totalBytes - bytesleft);

                    bytesleft -= n;
                }

                content = Encoding.ASCII.GetString(bytes);
            }

            var request = new HttpRequest()
            {
                Method = method,
                Url = url,
                Header = header,
                Content = content
            };

            Console.WriteLine("-REQUEST-----------------------------");
            Console.WriteLine(request);
            Console.WriteLine("------------------------------");

            return request;
        }
    }
}
