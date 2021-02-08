using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Opo.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsAssignableToGenericType(this Type givenType, Type genericType)
        {
            if (givenType == null || genericType == null)
            {
                return false;
            }

            return givenType == genericType
              || givenType.MapsToGenericTypeDefinition(genericType)
              || givenType.HasInterfaceThatMapsToGenericTypeDefinition(genericType)
              || givenType.BaseType.IsAssignableToGenericType(genericType);
        }

        private static bool HasInterfaceThatMapsToGenericTypeDefinition(this Type givenType, Type genericType)
        {
            return givenType
              .GetInterfaces()
              .Where(it => it.IsGenericType)
              .Any(it => it.GetGenericTypeDefinition() == genericType);
        }

        private static bool MapsToGenericTypeDefinition(this Type givenType, Type genericType)
        {
            return genericType.IsGenericTypeDefinition
              && givenType.IsGenericType
              && givenType.GetGenericTypeDefinition() == genericType;
        }

        public static Type GetEnumerableType(this Type type)
        {
            foreach (Type intType in type.GetInterfaces())
            {
                if (intType.IsGenericType
                    && intType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    return intType.GetGenericArguments()[0];
                }
            }
            return null;
        }

        public static bool IsGenericCollection(this Type type)
        {
            if (type.Equals(typeof(string)))
            {
                return false;
            }

            return type.GetInterface(nameof(IEnumerable)) != null;
        }
        public static bool IsGenericDictionary(this Type type)
        {
            return type.IsGenericCollection() && type.GetGenericTypeDefinition() == typeof(IDictionary<,>);
        }

        public static Type GetCollectionType(this Type type)
        {
            if (!type.IsGenericCollection())
            {
                return null;
            }

            if (type.IsGenericCollection())
            {
                return typeof(KeyValuePair<,>);
            }

            var genericArguments = type.GetGenericArguments();
            if (genericArguments != null && genericArguments.Any())
            {
                return genericArguments[0];
            }

            return null;
        }

        public static bool IsSimple(this Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                // nullable type, check if the nested type is simple.
                return IsSimple(type.GetGenericArguments()[0]);
            }
            return type.IsPrimitive
              || type.IsEnum
              || type.Equals(typeof(string))
              || type.Equals(typeof(decimal))
              || type.Equals(typeof(DateTime))
              || type.Equals(typeof(TimeSpan))
			  || type.Equals(typeof(Guid));
        }

        public static bool IsAnonymousType(this object obj)
        {
            return obj.GetType().IsAnonymousType();
        }
        public static bool IsAnonymousType(this Type type)
        {
            if (type == null)
            {
                return false;
            }

            // return type.Name.Contains("f__AnonymouseType");
            return Attribute.IsDefined(type, typeof(CompilerGeneratedAttribute), false)
                && type.IsGenericType && type.Name.Contains("AnonymousType")
                && (type.Name.StartsWith("<>") || type.Name.StartsWith("VB$"))
                && type.Attributes.HasFlag(TypeAttributes.NotPublic);
        }
    }
}
