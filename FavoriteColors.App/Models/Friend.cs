
namespace FavoriteColors.App.Models;

public record Friend(uint FriendId, string Name, ConsoleColor FavoriteColor)
{
    public override string ToString() => string.Format("{0, -15 } {1, -20 }", Name, FavoriteColor);
}
