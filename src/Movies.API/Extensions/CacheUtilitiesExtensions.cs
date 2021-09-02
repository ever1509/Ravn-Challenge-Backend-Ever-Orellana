using Microsoft.AspNetCore.Http;
using Movies.Application.Common.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Movies.API.Extensions
{
    public static class CacheUtilitiesExtensions
    {
        public static Dictionary<string, dynamic> GetDictionaryParams(this HttpRequest request)
        {

            if (request.Method.Equals("GET", StringComparison.InvariantCultureIgnoreCase))
            {
                var dictionary = request.Query.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => (dynamic)x.Value.ToString(), StringComparer.InvariantCultureIgnoreCase);
                return dictionary;
            }
            else
            {
                using var reader = new StreamReader(request.Body);
                reader.BaseStream.Seek(0, SeekOrigin.Begin);
                // You now have the body string raw
                var body = reader.ReadToEnd();
                // As well as a bound model
                var dictionary = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(body);
                return new Dictionary<string, dynamic>(dictionary.OrderBy(x => x.Key), StringComparer.InvariantCultureIgnoreCase);
            }
        }

        public static string GenerateCacheKey(this HttpRequest request, Dictionary<string, string> dictParams)
        {
            var path = request.Path;
            var builder = new StringBuilder();
            builder.Append(path);
            foreach (var (key, value) in dictParams)
            {
                string s = CacheUtilities.ParseValue(value);
                builder.Append($"-{key.ToLower()}-{s}|");
            }
            return builder.ToString();
        }
    }
}
