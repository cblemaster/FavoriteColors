
namespace FavoriteColors.Console;

internal record MenuOption(string OptionLabel, int SortOrder, ConsoleKey[] SelectKeys, Action MenuAction)
{
    public override string ToString() => OptionLabel;
}
