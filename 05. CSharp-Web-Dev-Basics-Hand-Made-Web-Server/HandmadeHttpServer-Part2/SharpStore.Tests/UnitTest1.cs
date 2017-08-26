namespace SharpStore.Tests
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Razor2;
    using SharpStore.Services;
    using SimpleHttpServer;
    using SimpleHttpServer.Models;

    [TestClass]
    public class UnitTest1
    {
        private IList<Route> routes;

        [TestInitialize]
        public void Init()
        {
            this.routes = new List<Route>()
            {
                new Route()
                {
                    Name = "Home",
                    Method = SimpleHttpServer.Enums.RequestMethod.GET,
                    UrlRegex = "^/home$",
                    Callable = (request) =>
                    {
                         if (request.Header.Cookies.Count != 0)
                        {
                            Cookies.cookies["theme"] = request.Header.Cookies["theme"].Value;
                        }

                        var knives = new KnivesService().GetAllKnivesFromUrl(request.Url);

                        return new HttpResponse()
                        {
                            StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.OK,
                            ContentAsUTF8 = new ProductsPage(knives).ToString()
                        };
                    }
                }
            };
        }

        [TestMethod]
        public void TestMethod1()
        {
            MemoryStream stream = new MemoryStream();
            string httpRequest = "Get /home HTTP/1.1\nHost: localhost:8081";
            byte[] requestBytes = Encoding.UTF8.GetBytes(httpRequest);
            stream.Write(requestBytes, 0, requestBytes.Length);
            stream.Position = 0;
            var processor = new HttpProcessor(this.routes);
            processor.HandleClient(stream);
            stream.Position = requestBytes.Length;
            string line = StreamUtils.ReadLine(stream);
            Assert.AreEqual("HTTP/1.1 200 OK", line);

        }
    }
}
