using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Opo.Extensions
{
  public static class UriExtensions
  {
    public static Uri SignUri(this Uri uri, string secret, int expirationTime = 300)
    {
      var nonce = Guid.NewGuid().ToString(); // random string - To prevent replay attacks.
      double timeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(); // current time as UNIX time stamp

      string signatureData = $"{nonce}:{timeStamp}:{expirationTime}";

      var hashedSignatureData = CreateHash(signatureData, secret);
      var signature = $"{signatureData}:{hashedSignatureData}".ToBase64();

      var query = uri.GetQueryParamsCollection();
      query.Add("signature", signature);
      var signedUrl = uri.GetLeftPart(UriPartial.Path) + query.ToQueryString();
      return new Uri(signedUrl);
    }

    public static bool VerifySignature(this string signatureBase64, string secret)
    {
      try
      {
        var signature = signatureBase64.FromBase64();
        var lastIndexOfSeparator = signature.LastIndexOf(':');
        var signatureData = signature.Substring(0, lastIndexOfSeparator);
        var hashedSignatureData = signature.Substring(lastIndexOfSeparator + 1);
        if (CreateHash(signatureData, secret).Equals(hashedSignatureData))
        {
          var dataParts = signatureData.SplitString(":");
          var timeStamp = long.Parse(dataParts[1]);
          var expirationTime = int.Parse(dataParts[2]);
          if (timeStamp + expirationTime > DateTimeOffset.UtcNow.ToUnixTimeSeconds())
          {
            return true;
          }
        }
      }
      catch
      {
        return false;
      }
      return false;
    }

    public static bool VerifySignedUri(this Uri uri, string secret)
    {
      var query = uri.GetQueryParamsCollection();

      query.TryGetValue("signature", out var signature);
      if (signature != null)
      {
        return signature.VerifySignature(secret);
      }
      return false;
    }

    private static string CreateHash(string signatureData, string secret)
    {
      var encoding = new UTF8Encoding();
      var keyBytes = encoding.GetBytes(secret);
      var messageBytes = encoding.GetBytes(signatureData.ToLower());
      using (var hmacsha1 = new HMACSHA256(keyBytes))
      {
        var hashMessage = hmacsha1.ComputeHash(messageBytes);
        return Convert.ToBase64String(hashMessage);
      }
    }

    public static QueryParamsCollection GetQueryParamsCollection(this Uri uri)
    {
      var queryString = uri.Query.StartsWith("?")
        ? uri.Query.Substring(1)
        : uri.Query;
      return new QueryParamsCollection(queryString.SplitString("&").Select(x =>
        {
          var indexOfSeparator = x.IndexOf('=');
          return (x.Substring(0, indexOfSeparator), x.Substring(indexOfSeparator + 1));
        }).ToDictionary(x => x.Item1, x => x.Item2));
    }

    public class QueryParamsCollection : Dictionary<string, string>
    {
      public QueryParamsCollection(IDictionary<string, string> queryParams)
      {
        foreach (var param in queryParams)
        {
          Add(param.Key, param.Value);
        }
      }

      public string ToQueryString(bool includeQuestionMark = true)
      {
        var queryString = string.Join("&", this.Select(x => $"{x.Key}={x.Value}"));
        return includeQuestionMark
          ? $"?{queryString}"
          : queryString;
      }
    }
  }
}