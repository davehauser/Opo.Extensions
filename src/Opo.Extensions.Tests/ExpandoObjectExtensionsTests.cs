using System.Collections.Generic;
using System.Dynamic;
using Shouldly;
using Xunit;

namespace Opo.Extensions.Dynamic
{
    public class ExpandoObjectExtensionsTests
    {
        [Fact]
        public void RemoveNullProperties_DoesItsJob()
        {
            // arrange
            dynamic obj = new ExpandoObject();
            obj.A = "A";
            obj.B = "B";
            obj.Null = null;
            ExpandoObject expandoObj = (ExpandoObject)obj;

            // act
            var actual = expandoObj.RemoveNullProperties();

            // assert
            var dict = (IDictionary<string, object>)actual;
            dict.Keys.ShouldNotContain("Null");
        }
    }
}