using System;
using System.Collections.Generic;
using System.Linq;

namespace Opo.Extensions
{
    public static class ObjectExtensions
    {
        public static T If<T>(this T t, bool condition, Func<T, T> action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("The action cannot be null.");
			}
			if (condition)
			{
				t = action(t);
			}
			return t;
		}
        
        public static IDictionary<string, object> ToPropertiesDictionary(this object obj, bool omitNullValues = false)
        {
            if(obj is IDictionary<string, object>)
            {
                return (IDictionary<string, object>)obj;
            }

            var properties = obj.GetType()
                .GetProperties()
				.ToDictionary(x => x.Name, x => x.GetValue(obj));

			return omitNullValues
				? properties
                    .Where(x => x.Value != null)
                    .ToDictionary(x => x.Key, x => x.Value)
				: properties;
        }

        public static bool IsSimpleType(this object obj)
        {
            return obj.GetType().IsSimple();
        }
    }
}