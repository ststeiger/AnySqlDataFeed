
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

        If table_name.StartsWith("?") Then
            table_name = table_name.Substring(1)
        End If

        If table_name.StartsWith("/") Then
            table_name = table_name.Substring(1)
        End If

        AnySqlDataFeed.DataFeed.SendFeed(table_name)
    End Sub ' ProcessRequest


    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property


End Class
