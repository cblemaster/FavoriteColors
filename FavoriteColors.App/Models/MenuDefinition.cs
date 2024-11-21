using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
