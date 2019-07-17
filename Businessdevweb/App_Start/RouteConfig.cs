using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Businessdevweb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");




            routes.MapRoute(
                name: "contact",
                url: "ارتباط-باما",
                defaults: new { Areas = "", controller = "Home", action = "Contact" },
                namespaces: new[] { string.Format("{0}.Controllers", typeof(RouteConfig).Namespace) }
            );
            routes.MapRoute(
    name: "about",
    url: "درباره-ما",
    defaults: new { Areas = "", controller = "Home", action = "About" },
    namespaces: new[] { string.Format("{0}.Controllers", typeof(RouteConfig).Namespace) }
);
       


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}/{name}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, name = UrlParameter.Optional },
                 namespaces: new[] { string.Format("{0}.Controllers", typeof(RouteConfig).Namespace) }
            );
        }
    }
}
