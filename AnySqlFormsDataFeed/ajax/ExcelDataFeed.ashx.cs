
// #define USE_BASIC_AUTH 

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

            // {Authorization=Basic+YWJjOmRlZg%3d%3d&Host=localhost%3a3830&User-Agent=PowerPivot}
            // HTTP Basic Authentication
            // HTTP Digest Authentication
            // HTTPS Client Authentication
            // Form Based Authentication

        
#if USE_BASIC_AUTH

	        if (!Authenticate(context)) 
            {
		        context.Response.Status = "401 Unauthorized";
		        context.Response.StatusCode = 401;
		        context.Response.AddHeader("WWW-Authenticate", "Basic");

		        //// context.CompleteRequest(); 
		        context.Response.Flush();
		        context.Response.End();
		        return;
	        }
#endif

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


        private static bool TryGetPrincipal(string[] creds, out System.Security.Principal.IPrincipal principal)
        {
            if (creds[0] == "Administrator" && creds[1] == "SecurePassword")
            {
                principal = new System.Security.Principal.GenericPrincipal(
                   new System.Security.Principal.GenericIdentity("Administrator"),
                   new string[] { "Administrator", "User" }
                );
                return true;
            }
            else if (creds[0] == "JoeBlogs" && creds[1] == "Password")
            {
                principal = new System.Security.Principal.GenericPrincipal(
                   new System.Security.Principal.GenericIdentity("JoeBlogs"),
                   new string[] { "User" }
                );
                return true;
            }
            else if (!string.IsNullOrEmpty(creds[0]) && !string.IsNullOrEmpty(creds[1]))
            {
                // GenericPrincipal(GenericIdentity identity, string[] Roles)
                principal = new System.Security.Principal.GenericPrincipal(new System.Security.Principal.GenericIdentity(creds[0]), 
                    new string[] { "Administrator", "User"}
                );
                return true;
            } 
            else
            {
                principal = null;
            }

            return false;
        } // End Function TryGetPrincipal 


        private static string[] ParseAuthHeader(string authHeader)
        {
            // Check if this is a Basic Auth header 
            if (
                authHeader == null ||
                authHeader.Length == 0 ||
                !authHeader.StartsWith("Basic", System.StringComparison.InvariantCultureIgnoreCase)
            ) return null;

            // Pull out the Credentials with are seperated by ':' and Base64 encoded 
            string base64Credentials = authHeader.Substring(6);
            string[] credentials = System.Text.Encoding.ASCII.GetString(
                  System.Convert.FromBase64String(base64Credentials)
            ).Split(':');

            if (credentials.Length != 2 || 
                string.IsNullOrEmpty(credentials[0]) || string.IsNullOrEmpty(credentials[0])
            ) 
                return null;

            return credentials;
        } // End Function ParseAuthHeader


        // http://blogs.msdn.com/b/odatateam/archive/2010/07/21/odata-and-authentication-part-6-custom-basic-authentication.aspx
        public static bool Authenticate(HttpContext context)
        {
            // One should be able to test on a developer system without https
            // if (!context.Request.IsSecureConnection) return false;

            string authHeader = context.Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(authHeader))
                return false;
            
            string[] credentials = ParseAuthHeader(authHeader);
            System.Console.WriteLine(credentials);

            System.Security.Principal.IPrincipal principal = default(System.Security.Principal.IPrincipal);
            if (TryGetPrincipal(credentials, out principal))
            {
                HttpContext.Current.User = principal;
                return true;
            }

            return false;
        } // End Function Authenticate 


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


    } // End Class ExcelDataFeed : IHttpHandler


} // End Namespace AnySqlFormsDataFeed.ajax 
