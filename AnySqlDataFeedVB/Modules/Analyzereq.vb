

Namespace AnySqlDataFeed.Modules


    Public Class AnalyzeRequestModule
        Implements System.Web.IHttpModule


        Public Shared Function InitTable() As System.Data.DataTable
            Dim dt As New System.Data.DataTable()
            dt.Columns.Add("Method", GetType(String))
            dt.Columns.Add("URL", GetType(String))
            dt.Columns.Add("Params", GetType(String))

            Return dt
        End Function


        Public Shared dt As System.Data.DataTable = InitTable()


        Public ReadOnly Property ModuleName() As String
            Get
                Return Me.[GetType]().Name
            End Get
        End Property


        ' In the Init function, register for HttpApplication events by adding your handlers.
        Private Sub Init(application As HttpApplication) Implements IHttpModule.Init
            AddHandler application.BeginRequest, New System.EventHandler(AddressOf Me.Application_BeginRequest)
            AddHandler application.EndRequest, New System.EventHandler(AddressOf Me.Application_EndRequest)
        End Sub


        ' Your BeginRequest event handler.
        Private Sub Application_BeginRequest(source As Object, e As System.EventArgs)
            Dim application As System.Web.HttpApplication = DirectCast(source, System.Web.HttpApplication)
            Dim context As System.Web.HttpContext = application.Context


            Dim dr As System.Data.DataRow = dt.NewRow()
            dr("Method") = context.Request.HttpMethod
            dr("URL") = context.Request.Url.OriginalString
            ' dr["Params"] = context.Request.Params.ToString();


            Dim sb As New System.Text.StringBuilder()

            Dim lsExclude As New System.Collections.Generic.List(Of String)()
            lsExclude.Add("SERVER_SOFTWARE")
            lsExclude.Add("SERVER_PROTOCOL")
            lsExclude.Add("SERVER_NAME")
            lsExclude.Add("SCRIPT_NAME")
            lsExclude.Add("SERVER_PORT")
            lsExclude.Add("GATEWAY_INTERFACE")
            lsExclude.Add("SERVER_PORT_SECURE")
            lsExclude.Add("HTTPS")
            lsExclude.Add("HTTP_HOST")

            lsExclude.Add("HTTP_CONNECTION")
            lsExclude.Add("REQUEST_METHOD")

            lsExclude.Add("REMOTE_HOST")
            lsExclude.Add("REMOTE_PORT")
            lsExclude.Add("REMOTE_ADDR")

            lsExclude.Add("APPL_PHYSICAL_PATH")
            lsExclude.Add("APPL_MD_PATH")
            lsExclude.Add("PATH_INFO")
            lsExclude.Add("PATH_TRANSLATED")
            lsExclude.Add("LOCAL_ADDR")

            lsExclude.Add("INSTANCE_META_PATH")
            lsExclude.Add("INSTANCE_ID")

            lsExclude.Add("CONTENT_LENGTH")

            lsExclude.Add("ALL_HTTP")
            lsExclude.Add("ALL_RAW")
            lsExclude.Add("URL")


            For Each key As String In context.Request.Params.AllKeys
                Dim value As String = context.Request.Params(key)
                If Not String.IsNullOrEmpty(value) Then
                    If Not lsExclude.Contains(key) Then
                        sb.AppendLine(Convert.ToString(key & Convert.ToString(": ")) & value)
                    End If
                End If
            Next

            dr("Params") = sb.ToString()
            sb.Length = 0
            sb = Nothing


            dt.Rows.Add(dr)

            'context.Response.Write("<h1><font color=red>HelloWorldModule: Beginning of Request</font></h1><hr>");
        End Sub


        ' Your EndRequest event handler.
        Private Sub Application_EndRequest(source As Object, e As System.EventArgs)
            Dim application As System.Web.HttpApplication = DirectCast(source, System.Web.HttpApplication)
            Dim context As System.Web.HttpContext = application.Context
            ' context.Response.Write("<hr><h1><font color=red>HelloWorldModule: End of Request</font></h1>");
        End Sub


        Private Sub Dispose() Implements IHttpModule.Dispose
            Throw New NotImplementedException()
        End Sub
    End Class


End Namespace
