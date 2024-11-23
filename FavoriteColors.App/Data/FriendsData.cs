
using FavoriteColors.App.Extensions;
using FavoriteColors.App.Models;
using System.Text.Json;

namespace FavoriteColors.App.Data;

public static class FriendsData
{
    public static void AddFriend()
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
        AllFriends.Add(new(NewFriendId, nameInput, ColorFromString(favColorInput)));
        friendAdded.WriteToTerminal();
    }

    public static IReadOnlyCollection<Friend> AllFriendsReadOnly => AllFriends.OrderBy(f => f.Name).ToList().AsReadOnly();
    private static List<Friend> AllFriends { get; set; } = [];
    public static void SearchFriends()
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

        AllFriends.Where(f => f.Name.Contains(input)).WriteToTerminal();
    }

    private static uint NewFriendId => AllFriendsReadOnly.Count > 0 ? AllFriendsReadOnly.Select(f => f.FriendId).Max() + 1 : 1;
    private static ConsoleColor ColorFromString(string s) => ConsoleColor.GetValues<ConsoleColor>().SingleOrDefault(c => c.ToString().Equals(s, StringComparison.CurrentCultureIgnoreCase));
    public static void SetAllFriends(IEnumerable<Friend> friends) => AllFriends = friends.ToList();
    public static string ToJson(IReadOnlyCollection<Friend> friends) => JsonSerializer.Serialize<IEnumerable<Friend>>(friends);
    public static IReadOnlyCollection<Friend> FromJson(string json) => JsonSerializer.Deserialize<IReadOnlyCollection<Friend>>(json) ?? [];
}
