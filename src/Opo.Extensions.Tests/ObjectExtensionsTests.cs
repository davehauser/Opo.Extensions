using System;
using Shouldly;
using Xunit;

namespace Opo.Extensions
{
    public class ObjectHelperTests
    {
        [Fact]
        public void ToDictionary_MapsAllProperties()
        {
            // arrange
            var input = new TestClass();

            // act
            var result = input.ToPropertiesDictionary();

            // assert
            result["IntProperty"].ShouldBe(1);
            result["StringProperty"].ShouldBe("abc");
            result["DateTimeProperty"].ShouldBe(new DateTime(2000, 1, 1, 0, 0, 0));
            result["NullProperty"].ShouldBeNull();
			result["NullableIntProperty"].ShouldBeNull();
			var childProperty = result["ClassProperty"] as TestClass.ChildClass;
            childProperty.IntProperty.ShouldBe(1);
            childProperty.StringProperty.ShouldBe("abc");
        }

        [Fact]
        public void ToDictionary_OmitsNullValues()
        {
            // arrange
            var input = new TestClass();

            // act
            var result = input.ToPropertiesDictionary(omitNullValues: true);

            // assert
            result["IntProperty"].ShouldBe(1);
            result["StringProperty"].ShouldBe("abc");
            result["DateTimeProperty"].ShouldBe(new DateTime(2000, 1, 1, 0, 0, 0));
            result.Keys.ShouldNotContain("NullProperty");
            var childProperty = result["ClassProperty"] as TestClass.ChildClass;
            childProperty.IntProperty.ShouldBe(1);
            childProperty.StringProperty.ShouldBe("abc");
        }

        [Fact]
        public void If_ReturnsEqualObject_IfConditionIsFalse()
        {
            //Given
            var i = 0;

            //When
            var actual = i.If(false, x => x + 1);

            //Then
            actual.ShouldBe(i);
        }

        [Fact]
        public void If_ReturnsUpdatedObject_IfConditionIsTrue()
        {
            //Given
            var i = 0;
            var expected = i + 1;

            //When
            var actual = i.If(true, x => x + 1);

            //Then
            actual.ShouldBe(expected);
        }

        [Fact]
        public void If_ThrowsArgumentException_IfFunctionIsNull()
        {
            //Given
            var i = 0;

            //When/Then
            Should.Throw(() => i.If(true, null), typeof(ArgumentNullException));
        }

        [Fact]
        public void If_ReturnsSameObject_IfReferenceType()
        {
            //Given
            var testClass = new TestClass
            {
                IntProperty = 1,
                StringProperty = "A"
            };

            //When
            var actual = testClass.If<TestClass>(true, x =>
            {
                x.IntProperty = 2;
                return x;
            });

            //Then
            actual.ShouldBeSameAs(testClass);
            actual.IntProperty.ShouldBe(2);
        }
    }

    public class TestClass
    {
        public int IntProperty { get; set; } = 1;
        public string StringProperty { get; set; } = "abc";
        public DateTime DateTimeProperty { get; set; } = new DateTime(2000, 1, 1, 0, 0, 0);
        public object NullProperty { get; set; } = null;
		public int? NullableIntProperty { get; set; }= null;
		public ChildClass ClassProperty { get; set; } = new ChildClass();

        public class ChildClass
        {
            public int IntProperty { get; set; } = 1;
            public string StringProperty { get; set; } = "abc";
        }
    }
}