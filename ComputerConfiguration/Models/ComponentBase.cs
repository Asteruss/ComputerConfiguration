using ComputerConfiguration.Models.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ComputerConfiguration.Models;

public abstract class ComponentBase : ComponentDTO, IComponent
{
    public int Id { get; set; } 
    public string Name { get; set; }
    public string Manufacturer { get; set; }
    public double BasePrice { get; set; }
    public double Rating { get; set; }
    public bool InStock { get; set; }
    public string Description { get; set; }
    public byte[] ImageData { get; set; }
    public ComponentCategory ComponentCategory { get; set; }
    public List<string> Tags { get; set; } = new();
    [NotMapped]

    private ImageSource _imageSource;
    [NotMapped]
    public ImageSource ImageSource
    {
        get
        {
            if (_imageSource == null && ImageData != null && ImageData.Length > 0)
            {
                using var stream = new MemoryStream(ImageData);
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = stream;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                bitmap.Freeze(); 
                _imageSource = bitmap;
            }
            return _imageSource;
        }
    }
}
