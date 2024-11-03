
using FavoriteColors.Console.Models;
using System.Text.Json;

string Path = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\favorite-colors";
string FileName = "favorite-colors.txt";
string FullPath = $"{Path}\\{FileName}";

List<Friend> AllFriends = [];

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
            AllFriends = JsonSerializer.Deserialize<List<Friend>>(s) ?? [];
        }
    }
    catch (Exception)
    {
        Console.WriteLine("Error: Unable to load friends.");
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
        Console.WriteLine('\n');
    }
    void ShowMainMenu()
    {
        Console.WriteLine("\n ***MAIN MENU***\n");
        Console.WriteLine("1) Add friend");
        Console.WriteLine("2) See all friends");
        Console.WriteLine("3) Search for friends");
        Console.WriteLine("X) Quit");
        Console.WriteLine('\n');
    }
    char PromptForMenuSelection()
    {
        char[] validInputs = ['1', '2', '3', 'x', 'X'];
        char userInput = '0';
        bool isFirstRun = false;
        while (!validInputs.Contains(userInput))
        {
            if (!isFirstRun)
            {
                isFirstRun = true;
                Console.WriteLine("Error: Invalid menu selection.\n");
            }

            ShowMainMenu();
            Console.WriteLine("\nEnter your menu selection, a digit between one(1) and three(3), or X to quit...\n");
            ConsoleKeyInfo key = Console.ReadKey();
            userInput = key.KeyChar;
        }
        return userInput;
    }
    void GoToMenuSelection(char selection)
    {
        switch (selection)
        {
            case '1':
                GoToAddFriend();
                break;
            case '2':
                GoToSeeAllFriends();
                break;
            case '3':
                GoToSearchForFriends();
                break;
            case 'x':
            case 'X':
                return;
            default:
                break;
        }

        void GoToAddFriend()
        {
            string firstName = string.Empty;
            bool isFirstRunName = false;
            while (!IsValidFirstName(firstName))
            {
                if (!isFirstRunName)
                {
                    isFirstRunName = true;
                    Console.WriteLine("\nError: first name input is invalid.\nFirst name must be between one(1) and one hundred(100) characters in length.");
                }
                Console.WriteLine("\nEnter friend's first name:\n");
                firstName = Console.ReadLine() ?? string.Empty;
            }

            string favColor = string.Empty;
            bool isFirstRunColor = false;
            while (!ConsoleColor.GetNames<ConsoleColor>().Contains(favColor))
            {
                if (!isFirstRunColor)
                {
                    isFirstRunColor = true;
                    Console.WriteLine("\nError: color input is invalid.");
                }
                Console.WriteLine("\nWhat is your friend's favotite color? Choose from these colors:\n");
                List<ConsoleColor> availableColors = new((ConsoleColor[])Enum.GetValues(typeof(ConsoleColor)));
                availableColors.Remove(ConsoleColor.Black);

                foreach (ConsoleColor color in availableColors)
                {
                    Console.ForegroundColor = color;
                    Console.Write($"{color}\t");
                    Console.Write('\n');
                    Console.ForegroundColor = ConsoleColor.White;
                }
                favColor = Console.ReadLine() ?? string.Empty;
            }

            AddFriend(firstName, favColor);

            bool IsValidFirstName(string firstName) => !string.IsNullOrWhiteSpace(firstName) && firstName.Length <= 100;
        }
        void GoToSearchForFriends()
        {
            string searchTerm = string.Empty;
            bool isFirstRun = false;
            while (!IsValidSearchTerm(searchTerm))
            {
                if (!isFirstRun)
                {
                    isFirstRun = true;
                    Console.WriteLine("\nError: Invalid search characters. Search characters must be fewer than one hundred(100).");
                }
                Console.WriteLine("\nEnter search characters:\n");
                searchTerm = Console.ReadLine() ?? string.Empty;
            }

            Friend[] foundFriends = SearchFriends(searchTerm);
            if (foundFriends.Length > 0)
            {
                Console.WriteLine("\nSEARCH RESULTS\n");
                DisplayFriends(foundFriends);
            }
            else
            {
                Console.WriteLine($"\nUh oh! No friends matching search term '{searchTerm}' were found");
            }
            bool IsValidSearchTerm(string searchTerm) => !string.IsNullOrWhiteSpace(searchTerm) && searchTerm.Length <= 100;
        }
        void GoToSeeAllFriends()
        {
            if (AllFriends.Count > 0)
            {
                DisplayFriends(GetAllFriends());
            }
            else
            {
                Console.WriteLine("\nUh oh! No friends found. Add some friends from the main menu!");
            }
        }

        Friend[] SearchFriends(string searchTerm) => GetAllFriends().Where(f => f.FirstName.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase)).ToArray();
        Friend[] GetAllFriends() => [.. AllFriends.OrderBy(f => f.FirstName)];
        void AddFriend(string firstName, string favColor)
        {
            ConsoleColor c = Enum.GetValues<ConsoleColor>().SingleOrDefault(c => c.ToString().Equals(favColor, StringComparison.CurrentCultureIgnoreCase));

            uint nextFriendId = AllFriends.Count > 0 ? AllFriends.Select(f => f.Id).Max() + 1 : 1;
            AllFriends.Add(new() { Id = nextFriendId, FirstName = firstName, FavoriteColor = c });
        }
        void DisplayFriends(IEnumerable<Friend> friendsToDisplay)
        {
            Console.Write("\nFirst Name\t");
            Console.Write("Favorite Color\n\n");
            foreach (Friend friend in friendsToDisplay)
            {
                Console.Write($"{friend.FirstName}\t\t");
                Console.Write($"{friend.FavoriteColor}\n");
            }
        }
    }
}
void AppEnd()
{
    if (AllFriends.Count > 0)
    {
        try
        {
            string fileContents = JsonSerializer.Serialize<List<Friend>>(AllFriends);
            using StreamWriter sw = new(FullPath, false);
            sw.Write(fileContents);
            Console.WriteLine("Data saved successfully!! Goodbye :\\)");
        }
        catch (Exception) { Console.Write("Unknown error saving data."); return; }
    }
}
