
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
        bool isFirstRun = true;
        while (!validInputs.Contains(userInput))
        {
            if (!isFirstRun)
            {
                Console.WriteLine("\nError: Invalid menu selection.\n");
            }

            ShowMainMenu();
            Console.WriteLine("\nEnter your menu selection, a digit between one(1) and three(3), or X to quit...\n");
            ConsoleKeyInfo key = Console.ReadKey();
            userInput = key.KeyChar;
            isFirstRun = false;
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
            bool isFirstRunName = true;
            while (!IsValidFirstName(firstName))
            {
                if (!isFirstRunName)
                {
                    Console.WriteLine("\nError: first name input is invalid.\nFirst name must be between one(1) and one hundred(100) characters in length.");
                }
                Console.WriteLine("\nEnter friend's first name:\n");
                firstName = Console.ReadLine() ?? string.Empty;
                isFirstRunName = false;
            }

            string favColor = string.Empty;
            bool isFirstRunColor = true;
            while (!Enum.GetNames<ConsoleColor>().Select(c => c.ToLowerInvariant()).Contains(favColor.ToLowerInvariant()))
            {
                if (!isFirstRunColor)
                {
                    Console.WriteLine("\nError: color input is invalid.");
                }
                Console.WriteLine("\nWhat is your friend's favorite color? Choose from these colors:\n");
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
                isFirstRunColor = false;
            }

            AddFriend(firstName, favColor);

            bool IsValidFirstName(string firstName) => !string.IsNullOrWhiteSpace(firstName) && firstName.Length <= 100;
        }
        void GoToSearchForFriends()
        {
            string searchTerm = string.Empty;
            bool isFirstRun = true;
            while (!IsValidSearchTerm(searchTerm))
            {
                if (!isFirstRun)
                {
                    Console.WriteLine("\nError: Invalid search characters. Search characters must be fewer than one hundred(100).");
                }
                Console.WriteLine("\nEnter search characters:\n");
                searchTerm = Console.ReadLine() ?? string.Empty;
                isFirstRun = false;
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
                Console.WriteLine("\nALL FRIENDS\n");
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

            int nextFriendId = AllFriends.Count > 0 ? AllFriends.Select(f => f.Id).Max() + 1 : 1;
            AllFriends.Add(new() { Id = nextFriendId, FirstName = firstName, FavoriteColor = c.ToString() });

            Console.WriteLine("\nFriend added sucessfully!!");
        }
        void DisplayFriends(IEnumerable<Friend> friendsToDisplay)
        {
            Console.Write("\n\nFirst Name\t");
            Console.Write("Favorite Color\n\n");
            foreach (Friend friend in friendsToDisplay)
            {
                ConsoleColor c = Enum.GetValues<ConsoleColor>().SingleOrDefault(c => c.ToString().Equals(friend.FavoriteColor, StringComparison.CurrentCultureIgnoreCase));
                Console.Write($"{friend.FirstName}\t\t");
                Console.ForegroundColor = c;
                Console.Write($"{friend.FavoriteColor}\n");
                Console.ForegroundColor = ConsoleColor.White;
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
            string jsonString = JsonSerializer.Serialize<List<Friend>>(AllFriends);
            using StreamWriter sw = new(FullPath, false);
            sw.Write(jsonString);
            Console.WriteLine("\nData saved successfully!! Goodbye!!)");
        }
        catch (Exception) { Console.Write("\nUnknown error saving data."); AppEnd(); } //TODO >> Is this a good idea? What is the right way to handle file write errors?
    }
}
