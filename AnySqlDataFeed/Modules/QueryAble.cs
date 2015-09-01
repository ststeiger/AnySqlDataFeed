
using System.Collections.Generic;


namespace AnySqlDataFeed.Query 
{


    // https://msdn.microsoft.com/en-us/library/Gg309461.aspx
    // http://www.asp.net/web-api/overview/odata-support-in-aspnet-web-api/using-$select,-$expand,-and-$value

    // http://www.codeproject.com/Articles/393623/OData-Services
    // http://www.codeproject.com/Articles/514598/Understanding-OData-v-and-WCF-Data-Services-x


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


        // http://stackoverflow.com/questions/181596/how-to-convert-a-column-number-eg-127-into-an-excel-column-eg-aa
        private string GetExcelColumnName(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }



        // http://stackoverflow.com/questions/667802/what-is-the-algorithm-to-convert-an-excel-column-letter-into-its-number
        public static int ExcelColumnNameToNumber(string columnName)
        {
            if (string.IsNullOrEmpty(columnName)) throw new System.ArgumentNullException("columnName");

            columnName = columnName.ToUpperInvariant();

            int sum = 0;

            for (int i = 0; i < columnName.Length; i++)
            {
                sum *= 26;
                sum += (columnName[i] - 'A' + 1);
            }

            return sum;
        }




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
