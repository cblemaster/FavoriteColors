namespace FavoriteColors.App.Models;

internal record MenuOptionDefinition
(
    string OptionLabel,
    int SortOrder,
    ConsoleKey[] SelectKeys,
    Action MenuAction
)
{
    public override string ToString() => OptionLabel;
}
