
namespace System.Web.Mvc
{
    
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;


    public class Equal : IRouteConstraint
    {
        private string m_Word;
        private System.StringComparison m_CompareOption;


        public Equal(string input) :this(input, System.StringComparison.InvariantCultureIgnoreCase)
        {}

        public Equal(string input, System.StringComparison compareOption)
        {
            this.m_Word = input;
            this.m_CompareOption = compareOption;
        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            string value = System.Convert.ToString(values[parameterName]);
            if (string.Equals(m_Word, value, this.m_CompareOption))
                return true;

            return false;
        } // End Function Match

    } // End Class Equal : IRouteConstraint


    public class NotEqual : IRouteConstraint
    {
        private string m_Word;
        private System.StringComparison m_CompareOption;

        public NotEqual(string input) :this(input, System.StringComparison.InvariantCultureIgnoreCase)
        {}

        public NotEqual(string input, System.StringComparison compareOption)
        {
            this.m_Word = input;
            this.m_CompareOption = compareOption;
        }


        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            string value = System.Convert.ToString(values[parameterName]);
            if (string.Equals(m_Word, value, this.m_CompareOption))
                return false;

            return true;
        } // End Function Match

    } // End Class NotEqual : IRouteConstraint 



    public class IsNotInList : IRouteConstraint
    {
        private readonly string[] m_Matches;
        private System.StringComparison m_CompareOption;

        public IsNotInList(string matches):this(
            matches.Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries)
            , System.StringComparison.InvariantCultureIgnoreCase
        )
        { }


        public IsNotInList(string[] matches) : this(matches, System.StringComparison.InvariantCultureIgnoreCase)
        { }

        public IsNotInList(string[] matches, System.StringComparison compareOption)
        {    
            this.m_Matches = matches;
            this.m_CompareOption = compareOption;
        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            string value = System.Convert.ToString(values[parameterName]);

            for (int i = 0; i < this.m_Matches.Length; ++i)
            {
                if (string.Equals(value, this.m_Matches[i], this.m_CompareOption))
                    return false;
            }

            return true;
        } // End Function Match

    } // End Class IsNotInList : IRouteConstraint


    public class IsInList : IRouteConstraint
    {
        private readonly string[] m_Matches;
        private System.StringComparison m_CompareOption;


        public IsInList(string matches):this(
            matches.Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries)
            , System.StringComparison.InvariantCultureIgnoreCase
        )
        { }

        public IsInList(string[] matches) : this(matches, System.StringComparison.InvariantCultureIgnoreCase)
        { }

        public IsInList(string[] matches, System.StringComparison compareOption)
        {    
            this.m_Matches = matches;
            this.m_CompareOption = compareOption;
        }


        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            string value = System.Convert.ToString(values[parameterName]);

            for (int i = 0; i < this.m_Matches.Length; ++i)
            {
                if (string.Equals(value, this.m_Matches[i], this.m_CompareOption))
                    return true;
            }

            return false;
        } // End Function Match

    } // End Class IsInList : IRouteConstraint


} 
