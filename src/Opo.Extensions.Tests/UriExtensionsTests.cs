using System;
using System.Linq;
using Shouldly;
using Xunit;

namespace Opo.Extensions
{
    public class UriExtensionsTests
    {
        
        [Fact]
        public void VerifySignedUri_Verifies()
        {
            // arrange
            var uri = new Uri("https://example.com");
            var secret = "secretcode";

            // act 
            var signedUri = uri.SignUri(secret);
            var actual = signedUri.VerifySignedUri(secret);

            // assert
            actual.ShouldBeTrue();
        }

        [Fact]
        public void VerifySignedUri_DoesNotVerifyWhenExpired()
        {
            // arrange
            var uri = new Uri("https://example.com");
            var secret = "secretcode";
            var expirationTime = -1;

            // act 
            var signedUri = uri.SignUri(secret, expirationTime);
            var actual = signedUri.VerifySignedUri(secret);

            // assert
            actual.ShouldBeFalse();
        }

        [Theory]
        [InlineData("https://example.com")]
        [InlineData("https://example.com/")]
        [InlineData("https://example.com/?")]
        public void GetQueryParamsCollection_ReturnsEmptyCollection_WhenNoQueryString(string uriString)
        {
            // arrange
            var uri = new Uri(uriString);

            // act
            var actual = uri.GetQueryParamsCollection();

            // assert
            actual.Count.ShouldBe(0);
        }

        [Fact]
        public void GetQueryParamsCollection_ReturnsCollectionOfQueryParams()
        {
            // arrange 
            var uri = new Uri("https://example.com/?key1=value1&key2=value2&key3=value3");

            // act
            var actual = uri.GetQueryParamsCollection();

            // assert
            actual.Count.ShouldBe(3);
            actual.ContainsKey("key1").ShouldBeTrue();
            actual.ContainsKey("key2").ShouldBeTrue();
            actual.ContainsKey("key3").ShouldBeTrue();
            actual.Values.Contains("value1").ShouldBeTrue();
            actual.Values.Contains("value2").ShouldBeTrue();
            actual.Values.Contains("value3").ShouldBeTrue();
        }
    }
}
