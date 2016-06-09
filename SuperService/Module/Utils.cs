using System.Collections.Generic;

namespace Test
{
    public static class Utils
    {
        public static object GetValueOrDefault(this IDictionary<string, object> dictionary, string key,
            object @default = null)
        {
            object res;
            return dictionary.TryGetValue(key, out res) ? res : @default;
        }
    }
}