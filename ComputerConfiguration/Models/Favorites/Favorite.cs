using ComputerConfiguration.Models.Enums;

namespace ComputerConfiguration.Models.Favorites;

public class Favorite
{
    public int Id { get; set; }
    public int ComponentId { get; set; }
    public ComponentCategory ComponentCategory { get; set; }
    public int UserId { get; set; }
}
