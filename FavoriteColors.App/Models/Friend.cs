
namespace FavoriteColors.App.Models;

public record Friend(uint FriendId, string Name, ConsoleColor FavoriteColor)
{
    public bool NameIsValid() => !string.IsNullOrWhiteSpace(Name) && Name.Length <= 15;
    public bool FavColorIsValid() => Enum.GetValues<ConsoleColor>().Contains(FavoriteColor);
    public bool FriendIsValid() => NameIsValid() && FavColorIsValid();
    public override string ToString() => string.Format("{0, -15 } {1, -20 }", Name, FavoriteColor);
}
