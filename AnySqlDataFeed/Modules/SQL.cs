
using System;
using System.Collections.Generic;
using System.Web;


namespace AnySqlDataFeed
{


    public class SQL
    {
        

        private static System.Data.Common.DbProviderFactory GetFactory(System.Type type)
        {
            if (type != null && type.IsSubclassOf(typeof(System.Data.Common.DbProviderFactory)))
            {
                // Provider factories are singletons with Instance field having
                // the sole instance
                System.Reflection.FieldInfo field = type.GetField("Instance"
                    , System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static
                );

                if (field != null)
                {
                    return (System.Data.Common.DbProviderFactory)field.GetValue(null);
                    //return field.GetValue(null) as DbProviderFactory;
                } // End if (field != null)

            } // End if (type != null && type.IsSubclassOf(typeof(System.Data.Common.DbProviderFactory)))

            throw new System.Configuration.ConfigurationErrorsException("DataProvider is missing!");
            //throw new System.Configuration.ConfigurationException("DataProvider is missing!");
        } // End Function GetFactory


        private static System.Data.Common.DbProviderFactory GetFactory()
        {
            if (System.Environment.OSVersion.Platform != PlatformID.Unix)
                return GetFactory(typeof(System.Data.SqlClient.SqlClientFactory));
            
            return GetFactory(typeof(Npgsql.NpgsqlFactory));
        }


        private static System.Data.Common.DbProviderFactory fac = GetFactory();


        public static string GetConnectionString()
        {
            if (fac is System.Data.SqlClient.SqlClientFactory)
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
                return sb.ConnectionString;
            }


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

            return csb.ConnectionString;
        }


        public static System.Data.Common.DbDataAdapter GetAdapter(string strSQL)
        {
            System.Data.Common.DbDataAdapter ada = fac.CreateDataAdapter();
            ada.SelectCommand = fac.CreateCommand();
            ada.SelectCommand.Connection = fac.CreateConnection();
            ada.SelectCommand.Connection.ConnectionString = GetConnectionString();
            ada.SelectCommand.CommandText = strSQL;

            return ada;
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