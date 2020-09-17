using System;
using System.Collections.Generic;

namespace Opo.Extensions
{
  public static class EnumerableExtensions
  {
    public static void Each<T>(this IEnumerable<T> items, Action<T> action)
    {
      foreach (var i in items)
        action(i);
    }
  }
}