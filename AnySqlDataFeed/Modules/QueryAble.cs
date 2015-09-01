
using System.Collections.Generic;


namespace AnySqlDataFeed.Query 
{


    // https://msdn.microsoft.com/en-us/library/Gg309461.aspx
    // http://www.asp.net/web-api/overview/odata-support-in-aspnet-web-api/using-$select,-$expand,-and-$value

    // http://www.codeproject.com/Articles/393623/OData-Services
    ^// http://www.codeproject.com/Articles/514598/Understanding-OData-v-and-WCF-Data-Services-x


    // http://blog.falafel.com/consuming-an-odata-feed-in-net-2-0/
    // http://www.odata.org/blog/how-to-use-web-api-odata-to-build-an-odata-v4-service-without-entity-framework/
    // http://iswwwup.com/t/e8cb4dd0d396/webapi-odata-without-entity-framework.html
    // https://msdn.microsoft.com/en-us/library/cc716729(v=vs.110).aspx
    // http://www.c-sharpcorner.com/UploadFile/dacca2/expose-odata-endpoint-without-entity-framework-and-perform-c/


    // http://www.asp.net/web-api/overview/odata-support-in-aspnet-web-api
    // http://www.asp.net/web-api/overview/odata-support-in-aspnet-web-api/odata-v4/create-an-odata-v4-endpoint
    // http://www.asp.net/web-api/overview/odata-support-in-aspnet-web-api/odata-v3/creating-an-odata-endpoint
    // http://www.asp.net/web-api/overview/odata-support-in-aspnet-web-api/odata-security-guidance
    public class QueryAble
    {


        // http://blogs.msdn.com/b/webdev/archive/2013/02/25/translating-odata-queries-to-hql.aspx
        public class ODataQueryOptions
        {
            // .. other stuff ..

            //public ODataQueryContext Context { get; }
            public string TableName;

            //public FilterQueryOption Filter { get; }
            public string Filter;

            //public OrderByQueryOption OrderBy { get; }
            public string OrderBy;

            //public SkipQueryOption Skip { get; }
            public ulong Skip;

            //public TopQueryOption Top { get; }
            public ulong Top;
        }


        // OrderByQueryOption Class
        public class OrderByQuery
        {
            // Context

            public List<OrderByNode> OrderByNodes;

            // public OrderByClause OrderByClause { get; }

            // RawValue

            // Validator
        }


        public class OrderByNode
        {
            public string Name;// Property.Name
            public OrderByDirection Direction; // ASC: 0 / DESC: 1
        }


        // Microsoft.Data.OData.Query.OrderByDirection
        public enum OrderByDirection
        {
             Ascending = 0 
            ,Descending = 1 
        }


        // $skip and $top
        // query.SetMaxResults(topQuery.Value);
        // query.SetFirstResult(skipQuery.Value); 


        // SELECT {FIRST N SKIP M / TOP N} 
        //     Field1, Field2, ..., FieldN
        // FROM {table}
        // {ORDER BY sortorder}
        // {OFFSET, LIMIT}

        // $orderby
        // https://msdn.microsoft.com/en-us/library/system.web.http.odata.query.orderbyqueryoption.orderbynodes(v=vs.118).aspx
        public void OrderBy(System.Text.StringBuilder stringBuilder, OrderByQuery orderByQuery)
        {
            stringBuilder.Append("ORDER BY ");

            int numOrderByNodes = orderByQuery.OrderByNodes.Count;
            int maxNode = orderByQuery.OrderByNodes.Count - 1;

            for (int i = 0; i < numOrderByNodes; ++i)
            {
                if (orderByQuery.OrderByNodes[i] != null)
                {
                    stringBuilder.Append(orderByQuery.OrderByNodes[i].Name);
                    stringBuilder.Append(orderByQuery.OrderByNodes[i].Direction == OrderByDirection.Ascending ? " ASC" : " DESC");

                    if (i != maxNode)
                        stringBuilder.Append(", ");
                }
                else
                {
                    throw new System.Exception("Only ordering by properties is supported");
                }
            }

        }


        // $filter
        //private string BindAllNode(AllNode allNode)
        //{
        //    string innerQuery = "not exists ( from " + Bind(allNode.Source) + " " + allNode.RangeVariables.First().Name;
        //    innerQuery += " where NOT(" + Bind(allNode.Body) + ")";
        //    return innerQuery + ")";
        //}


    } // End Class QueryAble


} // End Namespace AnySqlDataFeed.Query 
