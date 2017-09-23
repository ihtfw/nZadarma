using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.SqlServer.Server;

namespace nZadarma.Core
{
    public class QueryInfo
    {
        private readonly Dictionary<string, string> parameters = new Dictionary<string, string>();

        public string Build(bool orderByKeys = false)
        {
            if (!parameters.Any())
                return string.Empty;
            
            if (orderByKeys)
            {
                return string.Join("&", parameters.OrderBy(p => p.Key).Select(p => $"{p.Key}={p.Value}"));
            }
            else
            {
                return string.Join("&", parameters.Select(p => $"{p.Key}={p.Value}"));
            }
        }

        public QueryInfo Add(string key, object value)
        {
            if (value == null)
                return this;
            
            parameters.Add(Uri.EscapeDataString(key), Uri.EscapeDataString(value.ToString().ToLower()));

            return this;
        }
    }
}