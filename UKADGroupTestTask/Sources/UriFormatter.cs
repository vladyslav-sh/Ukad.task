using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;

namespace UKADGroupTestTask.Sources
{
    public class UriFormatter
    {
        public string ToProperFormat(string uri)
        {
            if (uri.EndsWith("/"))
                return uri.TrimEnd('/');

            return uri;
        }
    }
}