using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MovieSoft
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var defaultController = ConfigurationManager.AppSettings["HomeController"];
            if (string.IsNullOrEmpty(defaultController))
                defaultController = "Home";
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = defaultController, action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
