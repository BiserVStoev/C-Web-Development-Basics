namespace SimpleHttpServer
{
    using System.Collections.Generic;
    using System.Net;

    public class QueryStringParser
    {
        public static IDictionary<string, string> Parse(string queryString)
        {
            queryString = WebUtility.UrlDecode(queryString);
            Dictionary<string, string> variables = new Dictionary<string, string>();

            string[] vars = queryString.Split('&');
            foreach (var variable in vars)
            {
                string[] tokens = variable.Split('=');
                variables.Add(tokens[0], tokens[1]);
            }

            return variables;
        }
    }
}
