using System;
using System.Collections.Generic;
using System.Web;

namespace AnySqlDataFeed
{


    public class SQL
    {


        public static string GetConnectionString()
        {
            System.Data.SqlClient.SqlConnectionStringBuilder sb = new System.Data.SqlClient.SqlConnectionStringBuilder();
            sb.DataSource = Environment.MachineName;
            sb.InitialCatalog = "COR_Basic_Demo";
            sb.IntegratedSecurity = true;
            sb.MultipleActiveResultSets = true;
            sb.PersistSecurityInfo = false;
            sb.Pooling = true;
            sb.PacketSize = 4096;
            sb.ApplicationName = "ODataTest";


            Npgsql.NpgsqlConnectionStringBuilder csb = new Npgsql.NpgsqlConnectionStringBuilder();
            csb.Host = "127.0.0.1";
            csb.Port = 5432;
            csb.Database = "blogz";

            // csb.IntegratedSecurity = false;
            // CREATE ROLE alibaba LOGIN PASSWORD 'OpenSesame' SUPERUSER INHERIT CREATEDB CREATEROLE REPLICATION;
            csb.UserName = "alibaba";
            csb.Password = "OpenSesame";
            csb.CommandTimeout = 300;
            csb.ApplicationName = "ODataTest";

            System.Data.Common.DbConnectionStringBuilder dbcs = sb;
            if (System.Environment.OSVersion.Platform == PlatformID.Unix)
                dbcs = csb;

            return dbcs.ConnectionString;
        }


        public static System.Data.Common.DbDataAdapter GetAdapter(string strSQL)
        {
            if (System.Environment.OSVersion.Platform == PlatformID.Unix)
                return new Npgsql.NpgsqlDataAdapter(strSQL, GetConnectionString());

            return new System.Data.SqlClient.SqlDataAdapter(strSQL, GetConnectionString());
        }


        public static System.Data.DataTable GetDataTable(string SQL)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            using (System.Data.Common.DbDataAdapter da = GetAdapter(SQL))
            {
                da.Fill(dt);
            }

            return dt;
        }


    }
}