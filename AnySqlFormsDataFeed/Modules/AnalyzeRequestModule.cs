
namespace AnySqlDataFeed.Modules
{


    public class AnalyzeRequestModule : System.Web.IHttpModule
    {


        public static System.Data.DataTable dt = InitTable();


        public string ModuleName
        {
            get 
            {
                return this.GetType().Name; 
            }
        }


        public static System.Data.DataTable InitTable()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("Method", typeof(string));
            dt.Columns.Add("URL", typeof(string));
            dt.Columns.Add("Params", typeof(string));

            return dt;
        }


        // In the Init function, register for HttpApplication events by adding your handlers.
        public void Init(System.Web.HttpApplication application)
        {
            application.BeginRequest += (new System.EventHandler(this.Application_BeginRequest));
            application.EndRequest += (new System.EventHandler(this.Application_EndRequest));
        }


        // Your BeginRequest event handler.
        private void Application_BeginRequest(object source, System.EventArgs e)
        {
            System.Web.HttpApplication application = (System.Web.HttpApplication)source;
            System.Web.HttpContext context = application.Context;


            System.Data.DataRow dr = dt.NewRow();
            dr["Method"] = context.Request.HttpMethod;
            dr["URL"]=context.Request.Url.OriginalString;
            // dr["Params"] = context.Request.Params.ToString();


            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            System.Collections.Generic.List<string> lsExclude = new System.Collections.Generic.List<string>();
            lsExclude.Add("SERVER_SOFTWARE");
            lsExclude.Add("SERVER_PROTOCOL");
            lsExclude.Add("SERVER_NAME");
            lsExclude.Add("SCRIPT_NAME");
            lsExclude.Add("SERVER_PORT");
            lsExclude.Add("GATEWAY_INTERFACE");
            lsExclude.Add("SERVER_PORT_SECURE");
            lsExclude.Add("HTTPS");
            lsExclude.Add("HTTP_HOST");
            
            lsExclude.Add("HTTP_CONNECTION");
            lsExclude.Add("REQUEST_METHOD");
            
            lsExclude.Add("REMOTE_HOST");
            lsExclude.Add("REMOTE_PORT");
            lsExclude.Add("REMOTE_ADDR");

            lsExclude.Add("APPL_PHYSICAL_PATH");
            lsExclude.Add("APPL_MD_PATH");
            lsExclude.Add("PATH_INFO");
            lsExclude.Add("PATH_TRANSLATED");
            lsExclude.Add("LOCAL_ADDR");
            
            lsExclude.Add("INSTANCE_META_PATH");
            lsExclude.Add("INSTANCE_ID");
            
            lsExclude.Add("CONTENT_LENGTH");
            
            lsExclude.Add("ALL_HTTP");
            lsExclude.Add("ALL_RAW");
            lsExclude.Add("URL");


            foreach (string key in context.Request.Params.AllKeys)
            {
                string value = context.Request.Params[key];
                if (!string.IsNullOrEmpty(value))
                {
                    if(!lsExclude.Contains(key))
                        sb.AppendLine(key + ": " + value);
                }
            }

            dr["Params"] = sb.ToString();
            sb.Length = 0;
            sb = null;


            dt.Rows.Add(dr);

            //context.Response.Write("<h1><font color=red>HelloWorldModule: Beginning of Request</font></h1><hr>");
        }


        // Your EndRequest event handler.
        private void Application_EndRequest(object source, System.EventArgs e)
        {
            System.Web.HttpApplication application = (System.Web.HttpApplication)source;
            System.Web.HttpContext context = application.Context;
            // context.Response.Write("<hr><h1><font color=red>HelloWorldModule: End of Request</font></h1>");
        }


        public void Dispose()
        {
        }


    }


}
