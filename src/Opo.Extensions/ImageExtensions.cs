
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Opo.Extensions
{
  public static class ImageExtensions
  {
    public static Stream ToStream(this Image image, ImageFormat imageFormat = null)
    {
      var stream = new MemoryStream();
      image.Save(stream, imageFormat ?? image.RawFormat);
      stream.Position = 0;
      return stream;
    }
  }
}