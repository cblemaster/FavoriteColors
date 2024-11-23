
using FavoriteColors.App.Models;

namespace FavoriteColors.App.Extensions;

public static class IEnumerableExtensions
{
    public static void WriteToTerminal(this IEnumerable<Friend> friends)
    {
        if (!friends.Any())
        {
            string none = "No friends found!";
            none.WriteToTerminal();
        }
        
        string heading = "Friend name \tFavoriteColor";

        heading.WriteToTerminal();
        friends.ToList().ForEach(friend => friend.ToString().WriteToTerminal());
    }
}
