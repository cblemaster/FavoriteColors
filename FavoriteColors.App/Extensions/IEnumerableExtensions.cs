
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

        string heading = string.Format("{0, -15 } {1, -20 }", "Friend name", "FavoriteColor");

        heading.WriteToTerminal(2, 1);
        friends.ToList().ForEach(friend =>
        {
            Console.ForegroundColor = friend.FavoriteColor;
            friend.ToString().WriteToTerminal(0, 0);
            Console.ForegroundColor = ConsoleColor.White;
        });
    }
}
