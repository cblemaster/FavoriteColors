
using FavoriteColors.Console.Models;
using System.Text.Json;

string Path = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\favorite-colors";
string FileName = "favorite-colors.txt";
string FullPath = $"{Path}\\{FileName}";

IEnumerable<Friend> AllFriends = [];

InitData();
RunUI();
AppEnd();

void InitData()
{
    try
    {
        if (!Directory.Exists(Path)) { Directory.CreateDirectory(Path); }
        if (!File.Exists($"{Path}\\{FileName}")) { File.Create($"{Path}\\{FileName}"); }

        using StreamReader sr = new(FullPath);
        string s = sr.ReadToEnd();
        if (!string.IsNullOrEmpty(s))
        {
            AllFriends = JsonSerializer.Deserialize<IEnumerable<Friend>>(s) ?? [];
        }
    }
    catch (Exception)
    {
        Console.WriteLine("Error loading friends.");
        AppEnd();
    }
}
void RunUI()
{
    ShowIntro();
    
    char menuSelection = 'z';
    while (!menuSelection.Equals('x') && !menuSelection.Equals('X'))
    {
        menuSelection = PromptForMenuSelection();
        GoToMenuSelection(menuSelection);
    }    

    void ShowIntro()
    {
        string stars = new('*', count: 80);        
        string intro = "Welcome to Favorite Colors!!";
        string desc = "\nThis app makes it easy to keep track of the favorite colors of your friends!";
        Console.WriteLine('\n' + stars);
        Console.WriteLine(intro);
        Console.WriteLine(desc);
        Console.WriteLine(stars);
    }
    void ShowMainMenu()
    {
        Console.WriteLine("\n ***MAIN MENU***\n");
        Console.WriteLine("1) Search for friends");
        Console.WriteLine("2) Show all friends");
        Console.WriteLine("3) Add friend");
        Console.WriteLine("X) Quit");
    }
    char PromptForMenuSelection()
    {
        char[] validInputs = ['1', '2', '3', 'x', 'X'];
        char userInput = '0';
        while (!validInputs.Contains(userInput))
        {
            ShowMainMenu();
            Console.WriteLine("\nEnter your menu selection, a digit between one(1) and three(3), or X to quit...");
            ConsoleKeyInfo key = Console.ReadKey(intercept: true);
            userInput = key.KeyChar;
        }
        return userInput;
    }
    void GoToMenuSelection(char selection)
    {
        switch (selection)
        {
            case '1':
                Console.WriteLine("Search for friends (coming soon)");
                break;
            case '2':
                Console.WriteLine("Show all friends (coming soon)");
                break;
            case '3':
                Console.WriteLine("Add friend (coming soon)");
                break;
            case 'x':
            case 'X':
                return;
            default:
                break;
        }
    }
}
void AppEnd()
{
    if (AllFriends.Any())
    {
        try
        {
            string fileContents = JsonSerializer.Serialize(AllFriends);
            using StreamWriter sw = new(FullPath, false);
            sw.Write(fileContents);
            Console.WriteLine("Data saved successfully!! Goodbye :\\)");
        }
        catch (Exception) { Console.Write("Unknown error saving data."); return; }
    }
}
