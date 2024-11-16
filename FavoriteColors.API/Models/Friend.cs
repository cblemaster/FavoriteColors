
namespace FavoriteColors.API.Models;

public class Friend
{
    public int FriendId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string FavoriteColor { get; set; } = string.Empty;

    internal bool FriendIsValid => NameIsValid && FavColorIsValid;
    internal bool NameIsValid => !string.IsNullOrWhiteSpace(Name) && Name.Length <= 15;
    internal bool FavColorIsValid => Enum.GetNames<ConsoleColor>().Contains(FavoriteColor);
}
