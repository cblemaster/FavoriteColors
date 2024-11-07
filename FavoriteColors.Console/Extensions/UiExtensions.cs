
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace FavoriteColors.Console.Extensions;

internal static class UiExtensions
{
    internal static void WriteToTerminal(this string s, int leadingNewLines = 0, int trailingNewLines = 0)
    {
        string start = new string('\n', leadingNewLines);
        string middle = s;
        string end = new string('\n', trailingNewLines);
        string text = $"{start}{middle}{end}";
        System.Console.WriteLine(text);
    }
    internal static bool IsValidMenuSelection(this char c) => Regex.IsMatch(c.ToString(), "[1 - 3]");
    internal static bool IsValidName(this string s) => !string.IsNullOrWhiteSpace(s) && s.Length <= 15;
    internal static bool IsValidFavColor(this string s) => !Enum.GetNames<ConsoleColor>().Select(c => c.ToLowerInvariant()).Contains(s.ToLowerInvariant());
    internal static bool IsValidSearchTerm(this string s) => s.IsValidName();
    internal static bool IsValidConfirmExit(this char c) => Regex.IsMatch(c.ToString(), "[y,Y,n,N]");
    internal static bool HasLeadingOrTrailingWhiteSpace(this string s) => s.StartsWith(' ') || s.EndsWith(" ");
    internal static string ToTrimmedString(this string s) => s.Trim();
}
