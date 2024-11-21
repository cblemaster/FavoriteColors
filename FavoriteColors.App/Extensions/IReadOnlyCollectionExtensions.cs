
using FavoriteColors.App.Models;

namespace FavoriteColors.App.Extensions;

public static class IReadOnlyCollectionExtensions
{
    public static void WriteToTerminal(this IReadOnlyCollection<Friend> friends)
    {
        string heading = "Friend name \tFavoriteColor";

        heading.WriteToTerminal();
        friends.ToList().ForEach(friend => friend.ToString().WriteToTerminal());
    }
}
