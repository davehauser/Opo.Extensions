using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace Opo.Extensions.Dynamic
{
    public static class ExpandoObjectExtensions
    {
        public static dynamic RemoveNullProperties(this ExpandoObject obj)
        {
            if (obj == null) return null;

            var dictionary = obj as IDictionary<string, object>;
            if (dictionary == null) return null;

            return dictionary.Where(x => x.Value != null).ToDictionary(x => x.Key, x => x.Value);
        }
    }
}