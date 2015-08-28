using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;


using AnySqlDataFeed.Models;
using System.Web.Http;
using System.Web.Http.OData.Builder;



namespace AnySqlDataFeed
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Heben Sie die Kommentierung der folgenden Codezeile auf, um Abfrageunterstützung für Aktionen mit dem Rückgabetyp "IQueryable" oder "IQueryable<T>" zu aktivieren.
            // Damit die Verarbeitung unerwarteter oder böswilliger Abfragen vermieden wird, verwenden Sie die Überprüfungseinstellungen für "QueryableAttribute" zum Überprüfen eingehender Abfragen.
            // Weitere Informationen finden Sie unter "http://go.microsoft.com/fwlink/?LinkId=279712".
            //config.EnableQuerySupport();

            // Wenn Sie Ablaufverfolgung in Ihrer Anwendung deaktivieren möchten, kommentieren Sie die folgende Codezeile aus, oder entfernen Sie sie.
            // Weitere Informationen finden Sie unter: http://www.asp.net/web-api
            config.EnableSystemDiagnosticsTracing();


            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Product>("Products");
            // builder.EntitySet<System.Data.DataRow>("Products");
            config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
        }


    }


}
