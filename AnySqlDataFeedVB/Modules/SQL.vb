
Imports System.Collections.Generic
Imports System.Web


Namespace AnySqlDataFeed


    Public Class SQL


        Public Shared Function GetConnectionString() As String
            Dim sb As New System.Data.SqlClient.SqlConnectionStringBuilder()
            sb.DataSource = Environment.MachineName
            sb.InitialCatalog = "COR_Basic_Demo"
            sb.IntegratedSecurity = True
            sb.MultipleActiveResultSets = True
            sb.PersistSecurityInfo = False
            sb.Pooling = True
            sb.PacketSize = 4096
            sb.ApplicationName = "ODataTest"


            Dim csb As New Npgsql.NpgsqlConnectionStringBuilder()
            csb.Host = "127.0.0.1"
            csb.Port = 5432
            csb.Database = "blogz"

            ' csb.IntegratedSecurity = false;
            ' CREATE ROLE alibaba LOGIN PASSWORD 'OpenSesame' SUPERUSER INHERIT CREATEDB CREATEROLE REPLICATION;
            csb.UserName = "alibaba"
            csb.Password = "OpenSesame"
            csb.CommandTimeout = 300
            csb.ApplicationName = "ODataTest"

            Dim dbcs As System.Data.Common.DbConnectionStringBuilder = sb
            If System.Environment.OSVersion.Platform = PlatformID.Unix Then
                dbcs = csb
            End If

            Return dbcs.ConnectionString
        End Function


        Public Shared Function GetAdapter(strSQL As String) As System.Data.Common.DbDataAdapter
            If System.Environment.OSVersion.Platform = PlatformID.Unix Then
                Return New Npgsql.NpgsqlDataAdapter(strSQL, GetConnectionString())
            End If

            Return New System.Data.SqlClient.SqlDataAdapter(strSQL, GetConnectionString())
        End Function


        Public Shared Function GetDataTable(SQL As String) As System.Data.DataTable
            Dim dt As New System.Data.DataTable()

            Using da As System.Data.Common.DbDataAdapter = GetAdapter(SQL)
                da.Fill(dt)
            End Using

            Return dt
        End Function


    End Class


End Namespace
