
using System;
using System.Collections.Generic;
using System.Web;


namespace AnySqlDataFeed
{


    public class DataFeed
    {

        public delegate void callback_t(string table_name, System.IO.TextWriter strm);

        // AnySqlDataFeed.DataFeed.SendFeed(tableName);
        public static void SendFeed(string tableName)
        {
            // application/xml;charset=utf-8
            // return Content("Id (" + this.Request.HttpMethod + "): " + id,"application/xml");
            // if (StringComparer.OrdinalIgnoreCase.Equals(this.Request.HttpMethod, "HEAD")) return Content("");
            
            System.Web.HttpResponse Response = System.Web.HttpContext.Current.Response;


            Response.Headers.Add("Cache-Control", "no-cache");
            Response.Headers.Add("DataServiceVersion", "1.0;");
            Response.Headers.Add("Date", "Thu, 27 Aug 2015 20:16:35 GMT");
            Response.Headers.Add("X-Content-Type-Options", "nosniff");

            // string foo = XML.Test.Serialize();
            // return Content(foo, "application/xml");

            object m_objectToSerialize = null;
            callback_t m_CallBack = null;
            // XmlResult res = null;

            if (string.IsNullOrEmpty(tableName))
                // res = new XmlResult(Feed.Test.GetSerializationData());
                m_objectToSerialize = OData.TableListFeed.GetSerializationData();
            else
                //res = new XmlResult(tableName, Feed.TableDataTest.Test);
                m_CallBack = OData.TableDataFeed.GetSerializationData;


            // res.ExecuteResult();
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            context.Response.Clear();


            // Because we have references to disposable tables, we can't get the object
            if (m_CallBack != null)
            {
                context.Response.ContentType = "application/atom+xml;type=feed";
                m_CallBack(tableName, context.Response.Output);
            }

            if (m_objectToSerialize != null)
            {
                context.Response.ContentType = "application/xml";
                Tools.XML.Serialization.SerializeToXml(m_objectToSerialize, context.Response.Output);
            }

        }


    }


}