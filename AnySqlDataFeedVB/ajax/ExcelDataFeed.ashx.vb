
Imports System.Web
Imports System.Web.Services


Public Class ExcelDataFeed
    Implements System.Web.IHttpHandler


    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Const handlerName As String = "/ExcelDataFeed.ashx"

        Dim pos As Integer = System.Globalization.CultureInfo.InvariantCulture.CompareInfo.IndexOf(context.Request.Url.OriginalString, handlerName, System.Globalization.CompareOptions.IgnoreCase)

        Dim table_name As String = ""
        If pos <> -1 Then
            table_name = context.Request.Url.OriginalString.Substring(pos + handlerName.Length)
        End If

        ' {Authorization=Basic+YWJjOmRlZg%3d%3d&Host=localhost%3a3830&User-Agent=PowerPivot}
        ' HTTP Basic Authentication
        ' HTTP Digest Authentication
        ' HTTPS Client Authentication
        ' Form Based Authentication

        ' How does basic auth work?
        ' Basic authentication is a very simple authentication scheme, 
        ' that should only be used in conjunction with SSL 
        ' or in scenarios where security isn’t paramount.

        ' If you look at how a basic authentication header is fabricated, you can see why it is NOT secure by itself:

        ' string creds = "user" + ":" + "password";
        ' byte[] bcreds = System.Text.Encoding.ASCII.GetBytes(creds);
        ' string base64Creds = System.Convert.ToBase64String(bcreds); 
        ' string authorizationHeader = "Basic " + base64Creds;

#Const USE_BASIC_AUTH = False


#If USE_BASIC_AUTH Then

        If Not Authenticate(context) Then
            context.Response.Status = "401 Unauthorized"
            context.Response.StatusCode = 401
            context.Response.AddHeader("WWW-Authenticate", "Basic")

            '// context.CompleteRequest(); 
            context.Response.Flush()
            context.Response.End()
            Return
        End If
#End If

        If table_name.StartsWith("?") Then
            table_name = table_name.Substring(1)
        End If

        If table_name.StartsWith("/") Then
            table_name = table_name.Substring(1)
        End If

        AnySqlDataFeed.DataFeed.SendFeed(table_name)
    End Sub ' ProcessRequest


    Private Shared Function TryGetPrincipal(creds As String(), ByRef principal As System.Security.Principal.IPrincipal) As Boolean
        If creds(0) = "Administrator" AndAlso creds(1) = "SecurePassword" Then
            principal = New System.Security.Principal.GenericPrincipal(New System.Security.Principal.GenericIdentity("Administrator"), New String() {"Administrator", "User"})
            Return True
        ElseIf creds(0) = "JoeBlogs" AndAlso creds(1) = "Password" Then
            principal = New System.Security.Principal.GenericPrincipal(New System.Security.Principal.GenericIdentity("JoeBlogs"), New String() {"User"})
            Return True
        ElseIf Not String.IsNullOrEmpty(creds(0)) AndAlso Not String.IsNullOrEmpty(creds(1)) Then
            ' GenericPrincipal(GenericIdentity identity, string[] Roles)
            principal = New System.Security.Principal.GenericPrincipal(New System.Security.Principal.GenericIdentity(creds(0)), New String() {"Administrator", "User"})
            Return True
        Else
            principal = Nothing
        End If

        Return False
    End Function ' TryGetPrincipal


    ' http://blogs.msdn.com/b/odatateam/archive/2010/07/21/odata-and-authentication-part-6-custom-basic-authentication.aspx
    Public Shared Function Authenticate(context As HttpContext) As Boolean
        ' One should be able to test on a developer system without https
        'If Not context.Request.IsSecureConnection Then
        '    Return False
        'End If

        Dim authHeader As String = context.Request.Headers("Authorization")

        If String.IsNullOrEmpty(authHeader) Then
            Return False
        End If


        Dim credentials As String() = ParseAuthHeader(authHeader)
        ' System.Console.WriteLine(credentials)

        Dim principal As System.Security.Principal.IPrincipal = Nothing
        If TryGetPrincipal(credentials, principal) Then
            HttpContext.Current.User = principal
            Return True
        End If

        Return False
    End Function ' Authenticate 


    Private Shared Function ParseAuthHeader(authHeader As String) As String()
        ' Check if this is a Basic Auth header 
        If authHeader Is Nothing OrElse authHeader.Length = 0 OrElse Not authHeader.StartsWith("Basic", System.StringComparison.InvariantCultureIgnoreCase) Then
            Return Nothing
        End If

        ' Pull out the Credentials with are seperated by ':' and Base64 encoded 
        Dim base64Credentials As String = authHeader.Substring(6)
        Dim credentials As String() = System.Text.Encoding.ASCII.GetString(System.Convert.FromBase64String(base64Credentials)).Split(":"c)

        If credentials.Length <> 2 OrElse String.IsNullOrEmpty(credentials(0)) OrElse String.IsNullOrEmpty(credentials(0)) Then
            Return Nothing
        End If

        Return credentials
    End Function ' ParseAuthHeader 


    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property


End Class
