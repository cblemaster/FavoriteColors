
using FavoriteColors.App.Data;
using FavoriteColors.App.Extensions;
using FavoriteColors.App.Files;
using FavoriteColors.App.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

string folderName = "favorite-colors";
string fileName = "favorite-colors.txt";

string path = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\{folderName}\\{fileName}";

// set field for file handler
HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
// services.AddSingleton<IFileHandler>(s => new FileHandler(path));
builder.Services.AddSingleton<IFileHandler>(s => new FileHandler(path));
using IHost host = builder.Build();
IFileHandler _fileHandler = host.Services.GetService<IFileHandler>();

// read file
string json = _fileHandler?.TryReadFile() ?? string.Empty;

if (!string.IsNullOrWhiteSpace(json))
{
    // pass string to data.fromjson to set allfriends
    FriendsData.SetAllFriends(FriendsData.FromJson(json));
}

// start ui
// show intro
string stars = new('*', 30);
string intro = "Welcome to Favorite Colors!";

stars.WriteToTerminal();
intro.WriteToTerminal();
stars.WriteToTerminal();

// get menu and options
MenuDefinition menu = new("=== MENU ===", [], [], "Select one of these menu options:", "Error: Invalid menu option selection.", ConsoleKey.Escape, "{esc} = Quit");

MenuOptionDefinition option1 = new(1, "1 = Add a friend", 1, [ConsoleKey.D1, ConsoleKey.NumPad1], () => FriendsData.AddFriend());
MenuOptionDefinition option2 = new(2, "2 = See all friends", 2, [ConsoleKey.D2, ConsoleKey.NumPad2], () => FriendsData.AllFriends().WriteToTerminal());
MenuOptionDefinition option3 = new(3, "3 = Search for friend", 3, [ConsoleKey.D3, ConsoleKey.NumPad3], () => FriendsData.SearchFriends());
MenuOptionDefinition[] options = [option1, option2, option3];

menu.SetMenuOptions(options);

// run menu
RunMenu(menu);

// save data
string savingData = "Saving data...";
savingData.WriteToTerminal();

if (FriendsData.AllFriends().Count > 0)
{
    string saveSuccess = "Data saved ";
    string saveFailure = "Error: Data not saved!\nPrinting data to screen...";

    // pass allfriends to data.tojson
    json = FriendsData.ToJson(FriendsData.AllFriends());
    
    // pass this string to file.writefile
    bool saveSucceeds = _fileHandler.TryWriteFile(json);
    
    if (!saveSucceeds)
    {
        saveFailure.WriteToTerminal();
        FriendsData.AllFriends().WriteToTerminal();
    }
    else
    {
        saveSuccess.WriteToTerminal();
    }
}

// show outro
string outro = "Ending program...Goodbye!";
outro.WriteToTerminal();

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
        
        menu.MenuOptionDefinitions.ToList().ForEach(o => o.ToString().WriteToTerminal());
        menu.ExitKeyString.WriteToTerminal();

        selectedKey = Console.ReadKey().Key;
    }
    if (selectedKey == menu.ExitKey)
    {
        return;
    }

    menu.MenuOptionDefinitions.SingleOrDefault(optionDefinition => optionDefinition.SelectKeys.Contains(selectedKey))?.MenuAction();
    RunMenu(menu);
}
