using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AnySqlDataFeed
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapRoute(
                 name: "Default" 
                ,url: "{controller}/{action}/{id}" 
                ,defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                // ,constraints: new { controller = @"^(!DataFeed)" }
                ,constraints: new { controller = new NotEqual("DataFeed")}
            );

            // http://www.babel-lutefisk.net/2011/11/route-constraints-working-with.html
            // http://blogs.microsoft.co.il/bursteg/2009/01/11/aspnet-mvc-route-constraints/

            
            routes.MapRoute(
                 name: "DataFeed" 
                ,url: "{controller}/{id}"
                ,defaults: new { controller = "DataFeed", action = "Index", id = UrlParameter.Optional }
                ,constraints: new { controller = new Equal("DataFeed") }
            );
            
        } // End Sub RegisterRoutes(RouteCollection routes)


    } // End Class RouteConfig


} // End Namespace AnySqlDataFeed
