
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

    MenuOptionDefinition option1 = new("1 = Add a friend", 1, [ConsoleKey.D1, ConsoleKey.NumPad1], () => AddFriend());
    MenuOptionDefinition option2 = new("2 = See all friends", 2, [ConsoleKey.D2, ConsoleKey.NumPad2], () => FriendsData.AllFriendsReadOnly.WriteToTerminal());
    MenuOptionDefinition option3 = new("3 = Search for friend", 3, [ConsoleKey.D3, ConsoleKey.NumPad3], () => SearchFriends());
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

        json = FriendsData.ToJson(FriendsData.AllFriendsReadOnly);

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
void AddFriend()
{
    string nameInput = string.Empty;
    string favColorInput = string.Empty;

    string namePrompt = "Enter friend's name, or X to return to the menu.";
    string favColorPrompt = "Enter friend's favorite color, or X to return to the menu.";
    string favColorOptions = "Choose from these colors:";

    string nameValidationError = string.Empty;
    string favColorValidationError = string.Empty;

    while (string.IsNullOrWhiteSpace(nameInput) || nameInput.Length > 15)
    {
        if (!string.IsNullOrEmpty(nameValidationError))
        {
            nameValidationError.WriteToTerminal();
        }
        nameValidationError = "Error: Invalid friend name. Friend name must be between one(1) and fifteen (15) characters.";

        namePrompt.WriteToTerminal();
        nameInput = Console.ReadLine()?.Trim() ?? string.Empty;
    }

    if (nameInput.Equals("x", StringComparison.CurrentCultureIgnoreCase))
    {
        return;
    }

    string[] validColors = ConsoleColor.GetNames<ConsoleColor>().Where(c => c != ConsoleColor.Black.ToString()).ToArray();

    bool favColorIsEmpty() => string.IsNullOrEmpty(favColorInput);
    bool validColorsContainsFavColor() => validColors.Any(c => c.Equals(favColorInput, StringComparison.CurrentCultureIgnoreCase));
    bool favColorIsX() => favColorInput.Equals("x", StringComparison.CurrentCultureIgnoreCase);

    while ((favColorIsEmpty() || !validColorsContainsFavColor()) && !favColorIsX())
    {
        if (!string.IsNullOrEmpty(favColorValidationError))
        {
            favColorValidationError.WriteToTerminal();
        }
        favColorValidationError = "Error: Invalid color input.";

        favColorPrompt.WriteToTerminal();
        favColorOptions.WriteToTerminal();
        ConsoleColor.GetValues<ConsoleColor>().Where(c => c != ConsoleColor.Black).ToList().ForEach(c =>
        {
            Console.ForegroundColor = c;
            c.ToString().WriteToTerminal(0, 0);
            Console.ForegroundColor = ConsoleColor.White;
        });
        favColorInput = Console.ReadLine()?.Trim() ?? string.Empty;
    }

    if (favColorInput.Equals("x", StringComparison.CurrentCultureIgnoreCase))
    {
        return;
    }

    string friendAdded = "Friend added sucessfully!";
    FriendsData.AddFriend(new(NewFriendId(), nameInput, ColorFromString(favColorInput)));
    friendAdded.WriteToTerminal();

    uint NewFriendId() => FriendsData.AllFriendsReadOnly.Count > 0 ? FriendsData.AllFriendsReadOnly.Select(f => f.FriendId).Max() + 1 : 1;
    ConsoleColor ColorFromString(string s) => ConsoleColor.GetValues<ConsoleColor>().SingleOrDefault(c => c.ToString().Equals(s, StringComparison.CurrentCultureIgnoreCase));
}
void SearchFriends()
{
    string input = string.Empty;

    string prompt = "Enter search characters, or X to return to the menu.";
    string validationError = string.Empty;

    while (string.IsNullOrWhiteSpace(input) || input.Length > 15)
    {
        if (!string.IsNullOrEmpty(validationError))
        {
            validationError.WriteToTerminal();
        }
        validationError = "Error: Search characters are required and must be fifteen (15) or fewer.";

        prompt.WriteToTerminal();
        input = Console.ReadLine()?.Trim() ?? string.Empty;
    }

    if (input.Equals("x", StringComparison.CurrentCultureIgnoreCase))
    {
        return;
    }

    FriendsData.AllFriendsReadOnly.Where(f => f.Name.Contains(input)).WriteToTerminal();
}
