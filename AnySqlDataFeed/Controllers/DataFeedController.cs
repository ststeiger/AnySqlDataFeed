
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace AnySqlDataFeed.Controllers
{


    public class DataFeedController : Controller
    {

        // GET: /DataFeed/
        //public ContentResult Index(string id)
        public XmlResult Index(string id)
        {
            // application/xml;charset=utf-8
            // return Content("Id (" + this.Request.HttpMethod + "): " + id,"application/xml");
            // if (StringComparer.OrdinalIgnoreCase.Equals(this.Request.HttpMethod, "HEAD")) return Content("");

            this.Response.Headers.Add("Cache-Control", "no-cache");
            this.Response.Headers.Add("DataServiceVersion", "1.0;");
            this.Response.Headers.Add("Date", "Thu, 27 Aug 2015 20:16:35 GMT");
            this.Response.Headers.Add("X-Content-Type-Options", "nosniff");

            // string foo = XML.Test.Serialize();
            // return Content(foo, "application/xml");

            if(string.IsNullOrEmpty(id))
                return new XmlResult(OData.TableListFeed.GetSerializationData());

            return new XmlResult(id, AnySqlDataFeed.OData.TableDataFeed.GetSerializationData);
        }


        public class XmlResult : ActionResult
        {
            public delegate void callback_t(string table_name, System.IO.TextWriter strm);

            private object m_objectToSerialize;
            private string m_TableName;
            private callback_t m_CallBack;


            /// <summary>
            /// Initializes a new instance of the <see cref="XmlResult"/> class.
            /// </summary>
            /// <param name="objectToSerialize">The object to serialize to XML.</param>
            public XmlResult(object objectToSerialize)
            {
                this.m_objectToSerialize = objectToSerialize;
            }


            public XmlResult(string table_name, callback_t callbackFunction)
            {
                this.m_TableName = table_name;
                this.m_CallBack = callbackFunction;
            }


            /// <summary>
            /// Gets the object to be serialized to XML.
            /// </summary>
            public object ObjectToSerialize
            {
                get { return this.m_objectToSerialize; }
            }


            /// <summary>
            /// Serialises the object that was passed into the constructor to XML and writes the corresponding XML to the result stream.
            /// </summary>
            /// <param name="context">The controller context for the current request.</param>
            public override void ExecuteResult(ControllerContext context)
            {
                context.HttpContext.Response.Clear();
                
                // Because we have references to disposable tables, we can't get the object
                if (this.m_CallBack != null)
                {
                    context.HttpContext.Response.ContentType = "application/atom+xml;type=feed";
                    this.m_CallBack(this.m_TableName, context.HttpContext.Response.Output);
                }


                if (this.m_objectToSerialize != null)
                {
                    context.HttpContext.Response.ContentType = "application/xml";
                    Tools.XML.Serialization.SerializeToXml(this.m_objectToSerialize, context.HttpContext.Response.Output);
                }
                    
            } // End Sub ExecuteResult 


        } // End Class XmlResult 


    }


}