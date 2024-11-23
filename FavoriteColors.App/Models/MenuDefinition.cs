
namespace FavoriteColors.App.Models;

internal record MenuDefinition
(
    string Title,
    IEnumerable<MenuOptionDefinition> MenuOptionDefinitions,
    IEnumerable<MenuDefinition> SubMenuDefinitions,
    string Prompt,
    string InputValidationError,
    ConsoleKey ExitKey,
    string ExitKeyString
)
{
    internal IEnumerable<MenuOptionDefinition> MenuOptionDefinitions { get; private set; } = MenuOptionDefinitions;
    internal void SetMenuOptions(MenuOptionDefinition[] options) => MenuOptionDefinitions = options;
}
