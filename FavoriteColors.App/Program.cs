
using FavoriteColors.App.Data;
using FavoriteColors.App.Extensions;
using FavoriteColors.App.Files;
using FavoriteColors.App.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

#region Strings
const string FOLDER_NAME = "favorite-colors";
const string FILE_NAME = "favorite-colors.txt";

string appDataDir = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}";
string fileDir = Path.Combine(appDataDir, FOLDER_NAME);
string filePath = Path.Combine(fileDir, FILE_NAME);
#endregion Strings

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Services.AddSingleton<IFileHandler>(s => new FileHandler(fileDir, filePath));
using IHost host = builder.Build();

IFileHandler? _fileHandler = host.Services.GetService<IFileHandler>();
if (_fileHandler is null)
{
    string err = "Error: Unable to access file system.";
    err.WriteToTerminal();
    ShowOutro();
    return;
}

string json = ReadData();
if (!string.IsNullOrWhiteSpace(json))
{
    SetFriendsData(json);
}

ShowIntro();
MenuDefinition menu = CreateMenu();
RunMenu(menu);
SaveData();
ShowOutro();

string ReadData() => _fileHandler?.TryReadFile() ?? string.Empty;
void SetFriendsData(string json)
{
    if (!string.IsNullOrWhiteSpace(json))
    {
        // pass string to data.fromjson to set allfriends
        FriendsData.SetAllFriends(FriendsData.FromJson(json));
    }
}
void ShowIntro()
{
    string stars = new('*', 30);
    string intro = "Welcome to Favorite Colors!";

    stars.WriteToTerminal();
    intro.WriteToTerminal();
    stars.WriteToTerminal();
}
MenuDefinition CreateMenu()
{
    MenuDefinition menu = new("=== MENU ===", [], [], "Select one of these options:", "Error: Invalid menu option selection.", ConsoleKey.Escape, "{esc} = Quit");

    MenuOptionDefinition option1 = new(1, "1 = Add a friend", 1, [ConsoleKey.D1, ConsoleKey.NumPad1], () => FriendsData.AddFriend());
    MenuOptionDefinition option2 = new(2, "2 = See all friends", 2, [ConsoleKey.D2, ConsoleKey.NumPad2], () => FriendsData.AllFriendsReadOnly.WriteToTerminal());
    MenuOptionDefinition option3 = new(3, "3 = Search for friend", 3, [ConsoleKey.D3, ConsoleKey.NumPad3], () => FriendsData.SearchFriends());
    MenuOptionDefinition[] options = [option1, option2, option3];

    menu.SetMenuOptions(options);
    return menu;
}
void RunMenu(MenuDefinition menu)
{
    menu.Title.WriteToTerminal();

    ConsoleKey selectedKey = ConsoleKey.None;
    string validationError = string.Empty;

    List<ConsoleKey> validKeys = menu.MenuOptionDefinitions.Select(o => o.SelectKeys).SelectMany(o => o).ToList();
    validKeys.Add(menu.ExitKey);

    while (!validKeys.Contains(selectedKey) && selectedKey != menu.ExitKey)
    {
        if (!string.IsNullOrEmpty(validationError))
        {
            validationError.WriteToTerminal();
        }
        validationError = menu.InputValidationError;

        menu.Prompt.WriteToTerminal();

        menu.MenuOptionDefinitions.ToList().ForEach(o => o.ToString().WriteToTerminal(0, 0));
        menu.ExitKeyString.WriteToTerminal();

        selectedKey = Console.ReadKey().Key;
    }
    if (selectedKey == menu.ExitKey)
    {
        return;
    }

    menu.MenuOptionDefinitions.SingleOrDefault(o => o.SelectKeys.Contains(selectedKey))?.MenuAction();
    RunMenu(menu);
}
void SaveData()
{
    string savingData = "Saving data...";
    savingData.WriteToTerminal();

    if (FriendsData.AllFriendsReadOnly.Count > 0)
    {
        string saveSuccess = "Data saved ";
        string saveFailure = "Error: Data not saved!\nPrinting data to screen...";

        // pass allfriends to data.tojson
        json = FriendsData.ToJson(FriendsData.AllFriendsReadOnly);

        // pass this string to file.writefile
        bool saveSucceeds = _fileHandler.TryWriteFile(json);

        if (!saveSucceeds)
        {
            saveFailure.WriteToTerminal();
            FriendsData.AllFriendsReadOnly.WriteToTerminal();
        }
        else
        {
            saveSuccess.WriteToTerminal();
        }
    }
}
void ShowOutro()
{
    string outro = "Ending program...Goodbye!";
    outro.WriteToTerminal();
}
