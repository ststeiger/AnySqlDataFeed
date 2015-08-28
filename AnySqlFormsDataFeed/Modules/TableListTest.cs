
namespace AnySqlDataFeed.Feed 
{

    using System;
    using System.Xml.Serialization;
    using System.Collections.Generic;
    using AnySqlDataFeed.XML;

    public class Test
    {


        public static TableList.Service GetSerializationData()
        {
            TableList.Service ser = new TableList.Service();
            ser.Base = "http://localhost:5570/ExcelDataFeed.svc/";
            ser.Base = "http://localhost:54129/DataFeed";

            System.Uri url = System.Web.HttpContext.Current.Request.Url;
            ser.Base = url.Scheme + "://" + url.Authority + (System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath + "/").Replace("//", "/") + "ajax/ExcelDataFeed.ashx";



            if (Environment.OSVersion.Platform != PlatformID.Unix)
                ser.Xmlns = "http://www.w3.org/2007/app";

            ser.Atom = "http://www.w3.org/2005/Atom";

            ser.Workspace = new TableList.Workspace();
            ser.Workspace.Title = "Default";
            ser.Workspace.Collection = new List<TableList.Collection>();



            string strSQL = @"
SELECT 
     TABLE_SCHEMA AS table_schema 
    ,TABLE_NAME AS table_name 
FROM INFORMATION_SCHEMA.TABLES 
WHERE (1=1) 
AND table_schema NOT IN( 'information_schema', 'pg_catalog')
AND TABLE_TYPE = 'BASE TABLE'
AND TABLE_NAME LIKE 't\_%' ESCAPE '\'

ORDER BY TABLE_SCHEMA, TABLE_NAME 
";
            using (System.Data.DataTable dt = SQL.GetDataTable(strSQL))
            {
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    string tableName = System.Convert.ToString(dr["table_name"]);

                    ser.Workspace.Collection.Add(
                            new TableList.Collection()
                            {
                                Title = tableName
                                ,
                                Href = tableName
                            }
                    );
                } // Next dr 

            } // End Using System.Data.DataTable dt 

            return ser;
        }


        public static string Serialize()
        {
            TableList.Service ser = GetSerializationData();
            return Tools.XML.Serialization.SerializeToXml(ser);
        }


        public static void SerializeToFile()
        {
            TableList.Service ser = GetSerializationData();
            Tools.XML.Serialization.SerializeToXml(ser, @"d:\myatomtext.xml");
        }


    }
    

}
