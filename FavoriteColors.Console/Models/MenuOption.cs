
namespace FavoriteColors.Console.Models;

internal record MenuOption(string OptionLabel, int SortOrder, ConsoleKey[] SelectKeys, Action MenuAction)
{
    public Action MenuAction { get; private set; } = MenuAction;
    public override string ToString() => OptionLabel;
}
