namespace SharpStore
{
    using SimpleHttpServer;

    public class SharpStore
    {
        public static void Main()
        {
            var routes = RouteConfig.GetRoutes();

            HttpServer httpServer = new HttpServer(8081, routes);
            httpServer.Listen();
        }
    }
}