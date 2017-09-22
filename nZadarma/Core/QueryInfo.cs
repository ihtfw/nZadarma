using System;
using System.Collections.Generic;
using System.Linq;

namespace nZadarma.Core
{
    public class QueryInfo
    {
        private readonly Dictionary<string, string> parameters = new Dictionary<string, string>(); 
        
        public string Build()
        {
            if (!parameters.Any())
                return string.Empty;

            return string.Join("&", parameters.OrderBy(p => p.Key).Select(p => $"{p.Key}={p.Value}"));
        }

        public QueryInfo Add(string key, object value)
        {
            parameters.Add(key, value.ToString().ToLower());

            return this;
        }
    }
}