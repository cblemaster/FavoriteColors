
namespace FavoriteColors.Console;

/// <summary>
/// As configured, provides a static method that returns a Main Menu with three (3) SubMenus
/// This factory class can be altered or extended to create other menu/submenu patterns
/// </summary>
internal static class MenuFactory
{
    //internal static Menu GetMainMenu()
    internal static void GetMainMenu()
    {
        //string mainTitle = "*** MAIN MENU ***";
        //string prompt = "Select a menu option:";
        //string error = "Error: Invalid menu option selection.";

        //MenuOption sub1opt1 = new("1 = option 1", 1, [ConsoleKey.D1, ConsoleKey.NumPad1], () => Console.WriteLine("SubMenu 1 option 1 selected"));
        //MenuOption sub1opt2 = new("2 = option 2", 2, [ConsoleKey.D2, ConsoleKey.NumPad2], () => Console.WriteLine("SubMenu 1 option 2 selected"));
        //MenuOption sub1opt3 = new("3 = option 3", 3, [ConsoleKey.D3, ConsoleKey.NumPad3], () => Console.WriteLine("SubMenu 1 option 3 selected"));
        //MenuOption[] sub1Options = [sub1opt1, sub1opt2, sub1opt3];

        //MenuOption sub2opt1 = new("1 = option 1", 1, [ConsoleKey.D1, ConsoleKey.NumPad1], () => Console.WriteLine("SubMenu 2 option 1 selected"));
        //MenuOption sub2opt2 = new("2 = option 2", 2, [ConsoleKey.D2, ConsoleKey.NumPad2], () => System.Console.WriteLine("SubMenu 2 option 2 selected"));
        //MenuOption sub2opt3 = new("3 = option 3", 3, [ConsoleKey.D3, ConsoleKey.NumPad3], () => System.Console.WriteLine("SubMenu 2 option 3 selected"));
        //MenuOption[] sub2Options = [sub2opt1, sub2opt2, sub2opt3];

        //MenuOption sub3opt1 = new("1 = option 1", 1, [ConsoleKey.D1, ConsoleKey.NumPad1], () => Console.WriteLine("SubMenu 3 option 1 selected"));
        //MenuOption sub3opt2 = new("2 = option 2", 2, [ConsoleKey.D2, ConsoleKey.NumPad2], () => Console.WriteLine("SubMenu 3 option 2 selected"));
        //MenuOption sub3opt3 = new("3 = option 3", 3, [ConsoleKey.D3, ConsoleKey.NumPad3], () => Console.WriteLine("SubMenu 3 option 3 selected"));
        //MenuOption[] sub3Options = [sub3opt1, sub3opt2, sub3opt3];

        //Menu sub1 = new("** SubMenu 1 **", sub1Options, [], prompt, error, ConsoleKey.Escape, "{esc} = Return to main menu", ConsoleKey.None);
        //Menu sub2 = new("** SubMenu 2 **", sub2Options, [], prompt, error, ConsoleKey.Escape, "{esc} = Return to main menu", ConsoleKey.None);
        //Menu sub3 = new("** SubMenu 3 **", sub3Options, [], prompt, error, ConsoleKey.Escape, "{esc} = Return to main menu", ConsoleKey.None);

        //MenuOption main1 = new("1 = SubMenu 1", 1, [ConsoleKey.D1, ConsoleKey.NumPad1], () => sub1.Run());
        //MenuOption main2 = new("2 = SubMenu 2", 2, [ConsoleKey.D2, ConsoleKey.NumPad2], () => sub2.Run());
        //MenuOption main3 = new("3 = SubMenu 3", 3, [ConsoleKey.D3, ConsoleKey.NumPad3], () => sub3.Run());
        //MenuOption[] mainOptions = [main1, main2, main3];

        //Menu main = new(mainTitle, mainOptions, [sub1, sub2, sub3], prompt, error, ConsoleKey.Escape, "{esc} = End program", ConsoleKey.None);

        //return main;
    }
}
