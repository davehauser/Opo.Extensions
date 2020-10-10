using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using Shouldly;
using Xunit;

namespace Opo.Extensions
{
    public class TypeExtensionsTests
    {
        [Theory]
        [InlineData(typeof(IEnumerable<string>), true)]
        [InlineData(typeof(IList<string>), true)]
        [InlineData(typeof(IDictionary<string, string>), true)]
        [InlineData(typeof(Array), true)]
        public void IsGenericColllection_ReturnsTrue_WhenTestingGenericCollection(Type input, bool expected)
        {
            // act
            var actual = input.IsGenericCollection();

            // assert
            actual.ShouldBe(expected);
        }

        [Fact]
        public void IsGenericCollection_ReturnsFalse_WhenTestingNonGenericCollection()
        {
            // act 
            var actual = typeof(IEnumerable).IsGenericCollection();

            // assert
            actual.ShouldBeFalse();
        }

        [Fact]
        public void IsGenericCollection_ReturnsFalse_WhenTestingString()
        {
            // act 
            var actual = typeof(string).IsGenericCollection();

            // assert
            actual.ShouldBeFalse();
        }

        [Theory]
        [InlineData(typeof(IDictionary<string, string>), true)]
        [InlineData(typeof(IDictionary<string, object>), true)]
        [InlineData(typeof(IEnumerable<string>), false)]
        public void IsGenericDictionary_ReturnsTrue_WhenTestingGenericDictionary(Type input, bool expected)
        {
            // act
            var actual = input.IsGenericDictionary();

            // assert
            actual.ShouldBe(expected);
        }


        [Theory]
        [InlineData(typeof(IEnumerable<string>), typeof(string))]
        [InlineData(typeof(IList<int>), typeof(int))]
        [InlineData(typeof(IDictionary<string, string>), typeof(KeyValuePair<string, string>))]
        public void GetCollectionType_ReturnsCollectionType_WhenTypeIsGenericCollection(Type input, Type expected)
        {
            // act
            var actual = input.GetCollectionType();

            // assert
            actual.ShouldBe(expected);
        }

        [Theory]
        [InlineData(typeof(string))]
        [InlineData(typeof(IEnumerable))]
        public void GetCollectionType_ReturnsNull_WheneTypeIsNotGenericCollection(Type input)
        {
            // act
            var actual = input.GetCollectionType();

            // assert
            actual.ShouldBeNull();
        }

        [Fact]
        public void IsAnonymousType_ReturnsTrue_OnAnonymousTypes()
        {
            // arrange
            var input = new { A = "A", B = 2 };

            // act
            var actual = input.IsAnonymousType();

            // assert
            actual.ShouldBeTrue();
        }

        [Theory]
        [InlineData(typeof(string))]
        [InlineData(typeof(IList<string>))]
        [InlineData(null)]
        [InlineData(typeof(int))]
        [InlineData(typeof(ExpandoObject))]
        public void IsAnonymousType_ReturnsFalse_OnOtherTypes(Type input)
        {
            // act
            var actual = input.IsAnonymousType();

            // assert
            actual.ShouldBeFalse();
        }

        [Fact]
        public void IsAnonymousType_ReturnsFalse_WhenTypeIsCalledAnonymousType()
        {
            // arrange
            var input = new AnonymousType();

            // act
            var actual = input.IsAnonymousType();

            // assert
            actual.ShouldBeFalse();
        }
        public class AnonymousType {
            public int Number { get; set; }
            public string Text { get; set; }
        }
    }
}
