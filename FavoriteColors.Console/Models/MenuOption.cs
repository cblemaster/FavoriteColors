
namespace FavoriteColors.Console.Models;

internal record MenuOption(string OptionLabel, int SortOrder, ConsoleKey[] SelectKeys, Action MenuAction)
{
    internal Action MenuAction { get; set; } = MenuAction;
    public override string ToString() => OptionLabel;
}
