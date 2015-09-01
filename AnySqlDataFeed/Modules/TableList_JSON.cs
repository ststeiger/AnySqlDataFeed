using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnySqlDataFeed.JSON
{

    //    {"odata.metadata":"http://localhost:5570/Virt_X/ExcelDataFeed.svc/$metadata",
    //    "value":[
    //{"name":"ELMAH_Error","url":"ELMAH_Error"}
    //,{"name":"T_Admin","url":"T_Admin"}
    //,{"name":"T_ALV_Ref_FilterAnzeige","url":"T_ALV_Ref_FilterAnzeige"}
    //,{"name":"T_AP_Anlage","url":"T_AP_Anlage"}
    //,{"name":"T_AP_Anlage_History","url":"T_AP_Anlage_History"}
    //,{"name":"T_AP_Anschluss","url":"T_AP_Anschluss"},


    // http://localhost:5570/Virt_X/ExcelDataFeed.svc/?$format=json
    // http://localhost:5570/Virt_X/ExcelDataFeed.svc/?$format=xml


    public class TableList
    {

        [Newtonsoft.Json.JsonProperty(PropertyName = "odata.metadata")]
        public string metadata;

        // public List<Value> value { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "value")]
        public System.Data.DataTable data;
    }


    
}