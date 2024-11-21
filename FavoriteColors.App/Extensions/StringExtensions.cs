namespace FavoriteColors.App.Extensions;

public static class StringExtensions
{
    public static void WriteToTerminal(this string s, int leadingNewLines = 1, int trailingNewLines = 1)
    {
        string leading = leadingNewLines > 0 ? new('\n', leadingNewLines) : string.Empty;
        string trailing = trailingNewLines > 0 ? new('\n', trailingNewLines) : string.Empty;
        string result = $"{leading}{s}{trailing}";
        Console.WriteLine(result);
    }
}
