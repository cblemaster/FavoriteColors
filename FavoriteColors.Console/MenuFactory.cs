
using FavoriteColors.Console.Models;

namespace FavoriteColors.Console;

internal static class MenuFactory
{
    internal static Menu GetMainMenu()
    {
        string title = "<< MAIN MENU >>";
        string prompt = "Select a menu option:";
        string error = "Error: Invalid menu option selection.";
        string exitText = "{esc} = End program";
        ConsoleKey exitKey = ConsoleKey.Escape;
        ConsoleKey selectedKey = ConsoleKey.None;

        MenuOption opt1 = new("1 = Add a friend", 1, [ConsoleKey.D1, ConsoleKey.NumPad1], () => { });
        MenuOption opt2 = new("2 = See all friends", 2, [ConsoleKey.D2, ConsoleKey.NumPad2], () => { });
        MenuOption opt3 = new("3 = Search for friends", 3, [ConsoleKey.D3, ConsoleKey.NumPad3], () => { });
        MenuOption[] options = [opt1, opt2, opt3];

        Menu main = new(title, options, [], prompt, error, exitKey, exitText, selectedKey);

        return main;
    }
}
