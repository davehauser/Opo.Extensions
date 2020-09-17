using System.Reflection;

namespace Opo.Extensions
{
  public static class PropertyInfoExtensions
  {
    public static bool HasPublicSetter(this PropertyInfo prop)
    {
      return prop.GetSetMethod() != null;
    }
  }
}