
namespace FavoriteColors.Console;

internal static class MenuFactory
{
    internal static Menu GetMenu()
    {
        string title = "<< MAIN MENU >>";
        string prompt = "Select a menu option:";
        string error = "Error: Invalid menu option selection.";
        string exit = "{esc} = End program";
        ConsoleKey exitKey = ConsoleKey.Escape;
        ConsoleKey selectedKey = ConsoleKey.None;

        MenuOption opt1 = new("1 = Add a friend", 1, [ConsoleKey.D1, ConsoleKey.NumPad1], new(() => { }));
        MenuOption opt2 = new("2 = See all friends", 2, [ConsoleKey.D2, ConsoleKey.NumPad2], new(() => { }));
        MenuOption opt3 = new("3 = Search for friends", 3, [ConsoleKey.D3, ConsoleKey.NumPad3], new(() => { }));

        Menu main = new(title, [opt1,opt2,opt3], [], prompt, error, exitKey, exit, selectedKey);

        return main;
    }
}
