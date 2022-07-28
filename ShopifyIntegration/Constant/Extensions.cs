using Microsoft.Extensions.Primitives;
using System.Collections.Specialized;

namespace ShopifyIntegration.Constant
{
    public static class Extensions
    {
        public static IEnumerable<KeyValuePair<string, StringValues>> ToKVPairs(this NameValueCollection collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection.Cast<string>().Select(key => new KeyValuePair<string, StringValues>(key, collection[key]));
        }
    }
}
