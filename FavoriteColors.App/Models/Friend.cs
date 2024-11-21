
namespace FavoriteColors.App.Models;

public record Friend(int FriendId, string Name, ConsoleColor FavoriteColor)
{
    public bool NameIsValid() => !string.IsNullOrWhiteSpace(Name) && Name.Length <= 15;
    public bool FavColorIsValid() => Enum.GetValues<ConsoleColor>().Contains(FavoriteColor);
    public bool FriendIsValid() => NameIsValid() && FavColorIsValid();
    public override string ToString() => $"{Name}\t{FavoriteColor}";
}
