using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Movies.Application.Common.Utilities
{
    public static class CacheUtilities
    {
        public static string ParseValue(object value)
        {
            switch (value)
            {
                case string v:
                    return v;
                case IEnumerable<object> enumerable:
                    return string.Join(",", enumerable.OrderBy(Comparer).Select(Parser));
                case IEnumerable enumerable:
                    return string.Join(",", enumerable.Cast<object>().OrderBy(Comparer).Select(Parser));
                default:
                    return value.ToString();
            }
        }

        private static string Parser(object arg)
        {
            if (arg is JObject jObject) return jObject.ToString(Formatting.None).Replace("\"", string.Empty);
            return arg.ToString();
        }

        private static object Comparer(object arg)
        {
            if (arg is IComparable comp) return comp;
            return arg.GetHashCode();
        }
    }
}
