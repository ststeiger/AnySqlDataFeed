using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnySqlDataFeed.Controllers
{
    public class JsonController : Controller
    {
        //
        // GET: /Json/

        public ContentResult Index()
        {
            string table_name = "T_Benutzer";
            AnySqlDataFeed.JSON.TableData tableData = new JSON.TableData();
            tableData.metadata = "http://localhost:5698/ExcelDataFeed.svc/$metadata#" + table_name;
            tableData.data = SQL.GetDataTable("SELECT * FROM [" + table_name.Replace("]", "]]") + "]");
            
            // return View();
            // return Content(Newtonsoft.Json.JsonConvert.SerializeObject(tableData));

            string strSQL = @"
SELECT 
	 table_name AS name 
	,table_name AS url 
FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_TYPE = 'BASE TABLE' 
AND table_name LIKE 'T\_%' ESCAPE '\' 
ORDER BY table_name 
";

            AnySqlDataFeed.JSON.TableList tableList = new JSON.TableList();
            tableList.metadata = "http://localhost:5570/Virt_X/ExcelDataFeed.svc/$metadata";
            tableList.data = SQL.GetDataTable(strSQL);


            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(tableList));
        }

    }
}
