using SharpStore.Services;

namespace SharpStore
{
    using System.Collections.Generic;
    using System.IO;
    using SimpleHttpServer.Models;
    using System.Linq;
    using Razor2;
    using SimpleHttpServer;
    using System;
    using System.Reflection;
    
    public class RouteConfig
    {
        public static IList<Route> GetRoutes()
        {
            var routes = new List<Route>()
            {
                 new Route()
                {
                    Name = "ThemeChange",
                    Method = SimpleHttpServer.Enums.RequestMethod.GET,
                    UrlRegex = "^/.+?\\?theme=.+$",
                    Callable = (request) =>
                    {
                        if (request.Header.Cookies.Count != 0)
                        {
                            Cookies.cookies["theme"] = request.Header.Cookies["theme"].Value;
                        }
                       
                        var indexOfQuestion = request.Url.IndexOf('?');
                        var themeDict = QueryStringParser.Parse(request.Url.Substring(indexOfQuestion + 1));
                        var htmlFileName = request.Url.Substring(1, indexOfQuestion - 1);
                        var page = new Page($"../../content/{htmlFileName}.html");
                        var typeOfWantedPage = Assembly.GetAssembly(typeof(Page))
                            .GetTypes()
                            .FirstOrDefault(type =>
                                type.Name.Contains(
                                    htmlFileName[0].ToString().ToUpper()
                                    + htmlFileName.Substring(1)));    
                                            
                        Page instance = (Page) Activator.CreateInstance(typeOfWantedPage);
                        instance.AddStyleByPath($"../../content/css/{themeDict["theme"]}.css");

                        var response = new HttpResponse()
                        {
                            StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.OK,
                            ContentAsUTF8 = instance.ToString()
                        };

                        response.Header.Cookies.Add(new Cookie("theme", themeDict["theme"]));

                        return response;
                    }
                },
                 new Route()
                {
                    Name = "ContactsGET",
                    Method = SimpleHttpServer.Enums.RequestMethod.GET,
                    UrlRegex = "^/contacts$",
                    Callable = (request) =>
                    {
                        var page = new ContactsPage();
                        page.request = request;

                        var response = new HttpResponse()
                        {
                            StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.OK,
                            ContentAsUTF8 = page.ToString()
                        };

                        return response;
                    }
                },
                 new Route()
                {
                    Name = "ContactsPOST",
                    Method = SimpleHttpServer.Enums.RequestMethod.POST,
                    UrlRegex = "^/contacts$",
                    Callable = (request) =>
                    {
                        var page = new ContactsPage();
                        page.request = request;

                        string queryString = request.Content;
                        IDictionary<string, string> variables = QueryStringParser.Parse(queryString);
                        MessagesService service = new MessagesService();
                        service.AddMessageFromPostVars(variables);

                        return new HttpResponse()
                        {
                            StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.OK,
                            ContentAsUTF8 = page.ToString()
                        };
                    }
                },
                 new Route()
                {
                    Name = "Products",
                    Method = SimpleHttpServer.Enums.RequestMethod.GET,
                    UrlRegex = "^/products.*$",
                    Callable = (request) =>
                    {
                        var knives = new KnivesService().GetAllKnivesFromUrl(request.Url);
                        var page = new ProductsPage(knives);
                        page.request = request;

                        return new HttpResponse()
                        {
                            StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.OK,
                            ContentAsUTF8 = page.ToString()
                        };
                    }
                },
                 new Route()
                {
                    Name = "About Us",
                    Method = SimpleHttpServer.Enums.RequestMethod.GET,
                    UrlRegex = "^/about$",
                    Callable = (request) =>
                    {
                        var page = new AboutPage();
                        page.request = request;

                        return new HttpResponse()
                        {
                            StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.OK,
                            ContentAsUTF8 = page.ToString()
                        };
                    }
                },
                new Route()
                {

                    Name = "CSS",
                    Method = SimpleHttpServer.Enums.RequestMethod.GET,
                    UrlRegex = "^/content/css/.+.css$",
                    Callable = (request) =>
                    {
                        string fileName = request.Url.Substring(request.Url.LastIndexOf('/') + 1);

                        var response = new HttpResponse()
                        {
                            StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.OK,
                            ContentAsUTF8 = File.ReadAllText($"../../content/css/{fileName}")
                        };

                        response.Header.ContentType = "text/css";

                        return response;
                    }
                },
                new Route()
                {
                    Name = "Home Directory",
                    Method = SimpleHttpServer.Enums.RequestMethod.GET,
                    UrlRegex = "^/home$",
                    Callable = (request) =>
                    {
                        var page = new HomePage();
                        page.request = request;

                        return new HttpResponse()
                        {
                            StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.OK,
                            ContentAsUTF8 = page.ToString()
                        };
                    }
                },
                new Route()
                {
                    Name = "Carousel CSS",
                    Method = SimpleHttpServer.Enums.RequestMethod.GET,
                    UrlRegex = "^/bootstrap/css/bootstrap.min.css$",
                    Callable = (request) =>
                    {
                        var response = new HttpResponse()
                        {
                            StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.OK,
                            ContentAsUTF8 = File.ReadAllText("../../content/bootstrap/css/bootstrap.min.css")
                        };

                        response.Header.ContentType = "text/css";

                        return response;
                    }
                },
                new Route()
                {
                    Name = "Bootstrap JS",
                    Method = SimpleHttpServer.Enums.RequestMethod.GET,
                    UrlRegex = "^/bootstrap/js/bootstrap.min.js$",
                    Callable = (request) =>
                    {
                        var response = new HttpResponse()
                        {
                            StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.OK,
                            ContentAsUTF8 = File.ReadAllText("../../content/bootstrap/js/bootstrap.min.js")
                        };

                        response.Header.ContentType = "application/x-javascript";

                        return response;
                    }
                }
            };
            
            return routes;
        }
    }
}