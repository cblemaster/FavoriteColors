
using FavoriteColors.App.Models;

namespace FavoriteColors.App.Extensions;

internal static class UIExtensions
{
    internal static void WriteToTerminal(this string s, uint leadingNewLines = 1, uint trailingNewLines = 1)
    {
        string leading = leadingNewLines > 0 ? new('\n', (int)leadingNewLines) : string.Empty;
        string trailing = trailingNewLines > 0 ? new('\n', (int)trailingNewLines) : string.Empty;
        string result = $"{leading}{s}{trailing}";
        Console.WriteLine(result);
    }
    internal static void WriteToTerminal(this IEnumerable<Friend> friends)
    {
        if (!friends.Any())
        {
            string none = "No friends found!";
            none.WriteToTerminal(2, 1);
        }
        else
        {
            string heading = string.Format("{0, -15 } {1, -20 }", "Friend name", "FavoriteColor");

            heading.WriteToTerminal(2, 1);
            friends.ToList().ForEach(f =>
            {
                Console.ForegroundColor = f.FavoriteColor;
                f.ToString().WriteToTerminal(0, 0);
                Console.ForegroundColor = ConsoleColor.White;
            });
        }
    }
}
