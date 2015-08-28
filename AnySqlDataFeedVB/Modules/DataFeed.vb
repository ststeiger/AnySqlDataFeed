

Imports System.Collections.Generic
Imports System.Web


Namespace AnySqlDataFeed


    Public Class DataFeed

        Public Delegate Sub callback_t(table_name As String, strm As System.IO.TextWriter)

        ' AnySqlDataFeed.DataFeed.SendFeed(tableName);
        Public Shared Sub SendFeed(tableName As String)
            ' application/xml;charset=utf-8
            ' return Content("Id (" + this.Request.HttpMethod + "): " + id,"application/xml");
            ' if (StringComparer.OrdinalIgnoreCase.Equals(this.Request.HttpMethod, "HEAD")) return Content("");

            Dim Response As System.Web.HttpResponse = System.Web.HttpContext.Current.Response


            Response.Headers.Add("Cache-Control", "no-cache")
            Response.Headers.Add("DataServiceVersion", "1.0;")
            Response.Headers.Add("Date", "Thu, 27 Aug 2015 20:16:35 GMT")
            Response.Headers.Add("X-Content-Type-Options", "nosniff")

            ' string foo = XML.Test.Serialize();
            ' return Content(foo, "application/xml");

            Dim m_objectToSerialize As Object = Nothing
            Dim m_CallBack As callback_t = Nothing
            ' XmlResult res = null;

            If String.IsNullOrEmpty(tableName) Then
                ' res = new XmlResult(Feed.Test.GetSerializationData());
                m_objectToSerialize = Feed.Test.GetSerializationData()
            Else
                'res = new XmlResult(tableName, Feed.TableDataTest.Test);
                m_CallBack = AddressOf Feed.TableDataTest.Test
            End If


            ' res.ExecuteResult();
            Dim context As System.Web.HttpContext = System.Web.HttpContext.Current
            context.Response.Clear()


            ' Because we have references to disposable tables, we can't get the object
            If m_CallBack IsNot Nothing Then
                context.Response.ContentType = "application/atom+xml;type=feed"
                m_CallBack(tableName, context.Response.Output)
            End If

            If m_objectToSerialize IsNot Nothing Then
                context.Response.ContentType = "application/xml"
                Tools.XML.Serialization.SerializeToXml(m_objectToSerialize, context.Response.Output)
            End If

        End Sub


    End Class


End Namespace
