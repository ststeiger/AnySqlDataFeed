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
            string url = context.Request.Url.OriginalString;
            const string handlerName = "/ExcelDataFeed.ashx";
            int pos = System.Globalization.CultureInfo.InvariantCulture.CompareInfo.IndexOf(url, handlerName, System.Globalization.CompareOptions.IgnoreCase);

            string table_name = url.Substring(pos + handlerName.Length);

            if (table_name.StartsWith("?"))
                table_name = table_name.Substring(1);

            if (table_name.StartsWith("/"))
                table_name = table_name.Substring(1);

            AnySqlDataFeed.DataFeed.SendFeed(table_name);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}