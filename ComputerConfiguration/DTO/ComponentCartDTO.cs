using ComputerConfiguration.Models;
using ComputerConfiguration.Utilities;
using System.Windows.Media;

namespace ComputerConfiguration.DTO;

public class ComponentCartDTO
{
    public string TypeString { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ImageSource Image { get; set; }
    public double Price { get; set; }
    public bool IsFake { get; set; }
    public ComponentCartDTO(string typeString, ComponentBase component)
    {
        TypeString = typeString;
        Name = component.Name;
        Description = component.Description;
        Image = ImageHelper.ByteToImage(component.ImageData);
        Price = component.BasePrice;
        IsFake = component.ComponentStatus == Models.Enums.ComponentStatus.SelectedAsFake || component.ComponentStatus == Models.Enums.ComponentStatus.SelectedManyAsFake;
    }
}
