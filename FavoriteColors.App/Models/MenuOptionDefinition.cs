namespace FavoriteColors.App.Models;

public record MenuOptionDefinition
(
    uint MenuOptionId,
    string OptionLabel,
    int SortOrder,
    ConsoleKey[] SelectKeys,
    Action MenuAction
)
{
    public Action MenuAction { get; private set; } = MenuAction;
    public void SetMenuAction(Action action) => MenuAction = action;
    public override string ToString() => OptionLabel;
}
