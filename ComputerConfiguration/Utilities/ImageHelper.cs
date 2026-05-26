using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ComputerConfiguration.Utilities;

public class ImageHelper
{
    public static ImageSource ByteToImage(byte[] image)
    {
        if (image != null && image.Length > 0)
        {
            using var stream = new MemoryStream(image);
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = stream;
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            bitmap.Freeze();
            return bitmap;
        }
        return new BitmapImage();
    }
}
