
using System;
using System.Collections.Generic;
using System.Web;


namespace AnySqlFormsDataFeed.ajax
{


    /// <summary>
    /// Zusammenfassungsbeschreibung für ExcelDataFeed
    /// </summary>
    public class ExcelDataFeed : IHttpHandler
    {


        public void ProcessRequest(HttpContext context)
        {
            const string handlerName = "/ExcelDataFeed.ashx";

            int pos = System.Globalization.CultureInfo.InvariantCulture.CompareInfo.IndexOf(
                 context.Request.Url.OriginalString
                ,handlerName, System.Globalization.CompareOptions.IgnoreCase
            );

            string table_name = "";
            if(pos != -1)
                table_name = context.Request.Url.OriginalString.Substring(pos + handlerName.Length);

            if (table_name.StartsWith("?"))
                table_name = table_name.Substring(1);

            if (table_name.StartsWith("/"))
                table_name = table_name.Substring(1);

            AnySqlDataFeed.DataFeed.SendFeed(table_name);
        } // End Sub ProcessRequest


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


    } // End Class ExcelDataFeed : IHttpHandler


} // End Namespace AnySqlFormsDataFeed.ajax 
