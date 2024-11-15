
using FavoriteColors.Console.Models;
using System.Text.RegularExpressions;

namespace FavoriteColors.Console.Extensions;

internal static class UiExtensions
{
    internal static void WriteToTerminal(this string s, int leadingNewLines = 0, int trailingNewLines = 0)
    {
        string start = new('\n', leadingNewLines);
        string middle = s;
        string end = new('\n', trailingNewLines);
        string text = $"{start}{middle}{end}";
        System.Console.Write(text);
    }
    internal static bool IsValidMenuSelection(this ConsoleKey k) =>
        new ConsoleKey[] { 
            ConsoleKey.D1,
            ConsoleKey.NumPad1,
            ConsoleKey.D2,
            ConsoleKey.NumPad2,
            ConsoleKey.D3,
            ConsoleKey.NumPad3,
            ConsoleKey.Escape }
        .Contains(k);
    internal static bool IsValidName(this string s) => !string.IsNullOrWhiteSpace(s) && s.Length <= 15;
    internal static bool IsValidFavColor(this string s) => !Enum.GetNames<ConsoleColor>().Select(c => c.ToLowerInvariant()).Contains(s.ToLowerInvariant());
    internal static bool IsValidSearchTerm(this string s) => s.IsValidName();
    internal static bool IsValidConfirmExit(this char c) => Regex.IsMatch(c.ToString(), "[y,Y,n,N]");
    internal static bool HasLeadingOrTrailingWhiteSpace(this string s) => s.StartsWith(' ') || s.EndsWith(' ');
    internal static string ToTrimmedString(this string s) => s.Trim();
    internal static void ToListOfFriends(this IEnumerable<Friend> friends, string header)
    {
        WriteToTerminal(header, 0 , 2);
        WriteToTerminal("Friend name".PadRight(15) + '\t' + "Fav color", 0 , 1);
        foreach (Friend friend in friends)
        {
            WriteToTerminal(friend.FirstName);
            WriteToTerminal("\t");
            ConsoleColor consoleColor = Enum.GetValues<ConsoleColor>().SingleOrDefault(c => c.ToString().Equals(friend.FavoriteColor, StringComparison.CurrentCultureIgnoreCase));
            System.Console.ForegroundColor = consoleColor;
            WriteToTerminal(friend.FavoriteColor, 0, 1);
            System.Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
