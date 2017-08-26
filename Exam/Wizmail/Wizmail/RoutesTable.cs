﻿namespace Wizmail
{
    using System.Collections.Generic;
    using System.IO;
    using SimpleHttpServer.Enums;
    using SimpleHttpServer.Models;
    using SimpleMVC.Routers;

    public class RoutesTable
    {
        public static IEnumerable<Route> Routes =>
           new List<Route>()
           {
                new Route()
              {
                 Name = "Bootstrap JS",
                 Method = RequestMethod.GET,
                 UrlRegex = "^/js/bootstrap.min.js$",
                 Callable = request => new HttpResponse
                 {
                     ContentAsUTF8 = File.ReadAllText(Constants.ContentPath + request.Url),
                     Header = {ContentType = "application/javascript"}
                 }
              } ,
              new Route()
              {
                 Name = "JQuery JS",
                 Method = RequestMethod.GET,
                 UrlRegex = "^/jquery/jquery.min.js$",
                 Callable = request => new HttpResponse
                 {
                     ContentAsUTF8 = File.ReadAllText(Constants.ContentPath + request.Url),
                     Header = {ContentType = "application/javascript"}
                 }
              } ,
              new Route()
              {
                 Name = "/CSS",
                 Method = RequestMethod.GET,
                 UrlRegex = "^/css/(.+)$",
                 Callable = request => new HttpResponse
                 {
                     ContentAsUTF8 = File.ReadAllText(Constants.ContentPath + request.Url),
                     Header = {ContentType = "text/css"}
                 }
              } ,
              new Route()
              {
                 Name = "/Controller/Action GET",
                 Method = RequestMethod.GET,
                 UrlRegex = "^/(.+)/(.+)$",
                 Callable = new ControllerRouter().Handle
              },
              new Route()
              {
                 Name = "/Controller/Action POST",
                 Method = RequestMethod.POST,
                 UrlRegex = "^/(.+)/(.+)$",
                 Callable = new ControllerRouter().Handle
              }
           };
    }
}
