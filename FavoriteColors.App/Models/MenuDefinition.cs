
namespace FavoriteColors.App.Models;

public record MenuDefinition
(
    string Title,
    IEnumerable<MenuOptionDefinition> MenuOptionDefinitions,
    IEnumerable<MenuDefinition> SubMenuDefinitions,
    string Prompt,
    string InputValidationError,
    ConsoleKey ExitKey,
    string ExitKeyString
);
