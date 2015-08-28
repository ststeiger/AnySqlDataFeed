
namespace System.Web.Mvc
{
    
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;


    public class Equal : IRouteConstraint
    {
        private string m_Word;

        public Equal(string matches)
        {
            m_Word = matches;
        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            string value = System.Convert.ToString(values[parameterName]);
            if (System.StringComparer.OrdinalIgnoreCase.Equals(m_Word, value))
                return true;

            return false;
        }

    } // End Class Equal : IRouteConstraint


    public class NotEqual : IRouteConstraint
    {
        //private readonly List<string> _matches;
        private string m_Word;

        public NotEqual(string matches)
        {
            // _matches = matches.Split(',').ToList();
            m_Word = matches;
        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            string value = System.Convert.ToString(values[parameterName]);
            if (!System.StringComparer.OrdinalIgnoreCase.Equals(m_Word, value))
                return true;

            return false;
        }

    } // End Class NotEqual : IRouteConstraint 



    public class IsNotInList : IRouteConstraint
    {
        private readonly string[] m_Matches;

        public IsNotInList(string matches)
        {
            m_Matches = matches.Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
        }

        public IsNotInList(string[] matches)
        {
            m_Matches = matches;
        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            string value = System.Convert.ToString(values[parameterName]);

            for (int i = 0; i < m_Matches.Length; ++i)
            {
                if (System.StringComparer.OrdinalIgnoreCase.Equals(value, m_Matches[i]))
                    return false;
            }

            return true;
        }

    } // End Class IsNotInList : IRouteConstraint


    public class IsInList : IRouteConstraint
    {
        private readonly string[] m_Matches;

        public IsInList(string matches)
        {
            m_Matches = matches.Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
        }

        public IsInList(string[] matches)
        {
            m_Matches = matches;
        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            string value = System.Convert.ToString(values[parameterName]);

            for (int i = 0; i < m_Matches.Length; ++i)
            {
                if (System.StringComparer.OrdinalIgnoreCase.Equals(value, m_Matches[i]))
                    return true;
            }

            return false;
        }

    } // End Class IsInList : IRouteConstraint


}

