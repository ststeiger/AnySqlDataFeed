
Imports System.Xml.Serialization
Imports System.Collections.Generic
Imports AnySqlDataFeedVB.AnySqlDataFeed.XML


Namespace AnySqlDataFeed.Feed


    Public Class Test


        Public Shared Function GetSerializationData() As AnySqlDataFeed.XML.TableList.Service
            Dim ser As TableList.Service = New TableList.Service()
            ser.Base = "http://localhost:5570/ExcelDataFeed.svc/"
            ser.Base = "http://localhost:54129/DataFeed"

            Dim url As System.Uri = System.Web.HttpContext.Current.Request.Url
            ser.Base = url.Scheme + "://" + url.Authority + (System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath + "/").Replace("//", "/") + "ajax/ExcelDataFeed.ashx"

            If Environment.OSVersion.Platform <> PlatformID.Unix Then
                ser.Xmlns = "http://www.w3.org/2007/app"
            End If

            ser.Atom = "http://www.w3.org/2005/Atom"

            ser.Workspace = New TableList.Workspace()
            ser.Workspace.Title = "Default"
            ser.Workspace.Collection = New List(Of TableList.Collection)()


            Dim strSQL As String = vbCr & vbLf & "SELECT " & vbCr & vbLf & "     TABLE_SCHEMA AS table_schema " & vbCr & vbLf & "    ,TABLE_NAME AS table_name " & vbCr & vbLf & "FROM INFORMATION_SCHEMA.TABLES " & vbCr & vbLf & "WHERE (1=1) " & vbCr & vbLf & "AND table_schema NOT IN( 'information_schema', 'pg_catalog')" & vbCr & vbLf & "AND TABLE_TYPE = 'BASE TABLE'" & vbCr & vbLf & "AND TABLE_NAME LIKE 't\_%' ESCAPE '\'" & vbCr & vbLf & vbCr & vbLf & "ORDER BY TABLE_SCHEMA, TABLE_NAME " & vbCr & vbLf
            Using dt As System.Data.DataTable = SQL.GetDataTable(strSQL)
                For Each dr As System.Data.DataRow In dt.Rows
                    Dim tableName As String = System.Convert.ToString(dr("table_name"))

                    ser.Workspace.Collection.Add(New TableList.Collection() With {.Title = tableName, .Href = tableName})
                Next dr
            End Using ' System.Data.DataTable dt 

            Return ser
        End Function


        Public Shared Function Serialize() As String
            Dim ser As TableList.Service = GetSerializationData()
            Return Tools.XML.Serialization.SerializeToXml(ser)
        End Function


        Public Shared Sub SerializeToFile()
            Dim ser As TableList.Service = GetSerializationData()
            Tools.XML.Serialization.SerializeToXml(ser, "d:\myatomtext.xml")
        End Sub


    End Class


End Namespace
