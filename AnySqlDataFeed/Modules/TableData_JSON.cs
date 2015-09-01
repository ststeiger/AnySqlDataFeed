
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace AnySqlDataFeed.JSON
{


    public class TableData
    {

        //   {"odata.metadata":"http://localhost:5698/ExcelDataFeed.svc/$metadata#T_Admin"
        //  ,"value":[
        //      {"AD_UID":"6d12a79a-033d-4ca4-8e48-4a5eaa6f6aad","AD_User":"hbd_cafm","AD_Password":"DrpC0u2ZJp0=","AD_Level":1}
        //     ,{"AD_UID":"58e094e1-7cd6-4930-abb1-6530dce751ea","AD_User":"administrator","AD_Password":"DrvL1V44KXo=","AD_Level":0}
        //  ]}


        [Newtonsoft.Json.JsonProperty(PropertyName = "odata.metadata")]
        public string metadata;

        // public List<Value> value { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "value")]
        public System.Data.DataTable data;

    }


}
