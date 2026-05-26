using ComputerConfiguration.Models.Enums;
using ComputerConfiguration.Utilities;
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
    [NotMapped]
    public bool InStock => Count > 0;
    private int _count;
    public int Count
    {
        get => _count;
        set
        {
            if (_count == value) return;
            _count = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(InStock));
        }
    }
    public string Description { get; set; }
    public byte[] ImageData { get; set; }
    public ComponentCategory ComponentCategory { get; set; }
    public List<string> Tags { get; set; } = new();

    [NotMapped]
    public ImageSource ImageSource
    {
        get => ImageHelper.ByteToImage(ImageData);
    }
}
