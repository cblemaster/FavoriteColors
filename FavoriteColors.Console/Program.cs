
using FavoriteColors.Console.Extensions;
using FavoriteColors.Console.Models;
using FavoriteColors.Data;
using FavoriteColors.Data.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Services.AddSingleton<IDataService, JsonDataService>();
using IHost host = builder.Build();
IDataService _dataService = host.Services.GetService<IDataService>();

ShowIntro();
LoadData();
Menu menu = MenuFactory.GetMenu();
AssignMenuOptionActions(menu);
menu.Run();
SaveData();
ShowOutro();


void ShowIntro()
{
    string stars = new('*', count: 80);
    string intro = "Welcome to Favorite Colors!!";
    string desc = "This app makes it easy to keep track of the favorite colors of your friends!";
    stars.WriteToTerminal(0, 1);
    intro.WriteToTerminal(1, 1);
    desc.WriteToTerminal(1, 1);
    stars.WriteToTerminal(1, 0);
}
void ShowOutro()
{

}
void LoadData()
{
    string dataLoading = "Loading data...";
    string dataSuccess = "Data loaded successfully!";
    string dataError = "Error loading data! The program will now end.";

    dataLoading.WriteToTerminal(1, 1);
    if (!_dataService.TryLoadData())
    {
        dataError.WriteToTerminal(1, 1);
        ShowOutro();
    }
    else
    {
        dataSuccess.WriteToTerminal(1, 1);
    }
}
void SaveData()
{

}
void AssignMenuOptionActions(Menu menu)
{
    // TODO>> this is bug prone...
    menu.MenuOptions.SingleOrDefault(o => o.SortOrder == 1).MenuAction = new Action(() => AddFriend());
    menu.MenuOptions.SingleOrDefault(o => o.SortOrder == 2).MenuAction = new Action(() => SeeAllFriends());
    menu.MenuOptions.SingleOrDefault(o => o.SortOrder == 3).MenuAction = new Action(() => SearchFriends());
}
void AddFriend()
{
    string namePrompt = "Enter friend's first name, or X to return to menu:";
    string name = string.Empty;
    string nameValidationError = string.Empty;
    while (!IsValidFirstName(name))
    {
        if (!string.IsNullOrWhiteSpace(nameValidationError))
        {
            nameValidationError.WriteToTerminal(1, 1);
            nameValidationError = "Error: first name input is invalid.\nFirst name must be between one(1) and fifteen(15) characters in length.";
        }
        namePrompt.WriteToTerminal(1, 1);
        name = Console.ReadLine()?.Trim() ?? string.Empty;
    }
    if (IsExitInput(name))
    {
        return;
    }

    string favColorPrompt = "What is your friend's favorite color? Choose from these colors, or X to return to menu:";
    string favColor = string.Empty;
    string favColorValidationError = string.Empty;
    while (!IsValidColor(favColor))
    {
        if (!string.IsNullOrWhiteSpace(favColorValidationError))
        {
            favColorValidationError.WriteToTerminal(1, 1);
            favColorValidationError = "Error: color input is invalid.";
        }
        favColorPrompt.WriteToTerminal(1, 1);
        List<ConsoleColor> availableColors = new((ConsoleColor[])Enum.GetValues(typeof(ConsoleColor)));
        availableColors.Remove(ConsoleColor.Black);
        foreach (ConsoleColor color in availableColors)
        {
            Console.ForegroundColor = color;
            color.ToString().WriteToTerminal(1, 0);
            Console.ForegroundColor = ConsoleColor.White;
        }
        favColor = Console.ReadLine()?.Trim() ?? string.Empty;
    }
    if (IsExitInput(favColor))
    {
        return;
    }

    _dataService.AddFriend(name, favColor);

    bool IsExitInput(string input) => input.Equals("x", StringComparison.InvariantCultureIgnoreCase);
    bool IsValidFirstName(string firstName) => !string.IsNullOrWhiteSpace(firstName) && firstName.Length <= 15;
    bool IsValidColor(string favColor) => Enum.GetNames<ConsoleColor>().Select(c => c.ToLowerInvariant()).Contains(favColor.ToLowerInvariant());
}
void SeeAllFriends()
{

}
void SearchFriends()
{
    string searchPrompt = "Enter search characters, or X to return to menu:";
    string search = string.Empty;
    string searchValidationError = string.Empty;
    while (!IsValidSearch(search))
    {
        if (!string.IsNullOrWhiteSpace(searchValidationError))
        {
            searchValidationError.WriteToTerminal(1, 1);
            searchValidationError = "Error: search characters input is invalid.\nSearch characters must be between one(1) and fifteen(15) in length.";
        }
        searchPrompt.WriteToTerminal(1, 1);
        search = Console.ReadLine()?.Trim() ?? string.Empty;
    }
    if (IsExitInput(search))
    {
        return;
    }
    
    // TODO >> this coupling is weird - using a data project model?
    IEnumerable<Friend> foundFriends = _dataService.SearchFriends(search);
    if (foundFriends.Any())
    {
        string header = "SEARCH RESULTS";
        header.WriteToTerminal(1, 1);
        DisplayFriends(foundFriends.ToArray());
    }
    else
    {
        string header = $"\nUh oh! No friends matching search term '{search}' were found";
        header.WriteToTerminal(1, 1);
    }
    
    bool IsValidSearch(string searchTerm) => !string.IsNullOrWhiteSpace(searchTerm) && searchTerm.Length <= 100;
    void DisplayFriends(Friend[] friends)
    {

    }
}










//        }
//        void GoToSeeAllFriends()
//        {
//            if (AllFriends.Count > 0)
//            {
//                Console.WriteLine("\nALL FRIENDS\n");
//                DisplayFriends(GetAllFriends());
//            }
//            else
//            {
//                Console.WriteLine("\nUh oh! No friends found. Add some friends from the main menu!");
//            }
//        }

//        FriendDTO[] SearchFriends(string searchTerm) => GetAllFriends().Where(f => f.FirstName.Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase)).ToArray();
//        FriendDTO[] GetAllFriends() => [.. AllFriends.OrderBy(f => f.FirstName)];
//        void AddFriend(string firstName, string favColor)
//        {
//            ConsoleColor c = Enum.GetValues<ConsoleColor>().SingleOrDefault(c => c.ToString().Equals(favColor, StringComparison.InvariantCultureIgnoreCase));

//            int nextFriendId = AllFriends.Count > 0 ? AllFriends.Select(f => f.Id).Max() + 1 : 1;
//            //AllFriends.Add(new() { Id = nextFriendId, FirstName = firstName, FavoriteColor = c.ToString() });

//            Console.WriteLine("\nFriend added sucessfully!!");
//        }
//        void DisplayFriends(IEnumerable<FriendDTO> friendsToDisplay)
//        {
//            Console.Write("\n\nFirst Name\t");
//            Console.Write("Favorite Color\n\n");
//            foreach (FriendDTO friend in friendsToDisplay)
//            {
//                ConsoleColor c = Enum.GetValues<ConsoleColor>().SingleOrDefault(c => c.ToString().Equals(friend.FavoriteColor, StringComparison.InvariantCultureIgnoreCase));
//                Console.Write($"{friend.FirstName}\t\t");
//                Console.ForegroundColor = c;
//                Console.Write($"{friend.FavoriteColor}\n");
//                Console.ForegroundColor = ConsoleColor.White;
//            }
//        }
//    }
//}
//void AppEnd()
//{
//    if (AllFriends.Count > 0)
//    {
//        try
//        {
//            string jsonString = JsonSerializer.Serialize<List<FriendDTO>>(AllFriends);
//            using StreamWriter sw = new(FullPath, false);
//            sw.Write(jsonString);
//            Console.WriteLine("\nData saved successfully!! Goodbye!!)");
//        }
//        catch (Exception) { Console.Write("\nUnknown error saving data."); AppEnd(); } //TODO >> Is this a good idea? What is the right way to handle file write errors?
//    }
//}
