
using FavoriteColors.App.Models;
using System.Text.Json;

namespace FavoriteColors.App.Data;

public static class FriendsData
{
    private static IEnumerable<Friend> _allFriends = [];

    public static void AddFriend()
    {
        //_allFriends.ToList().Add(new(NewFriendId, name, ColorFromString(favColor)));
    }

    public static IReadOnlyCollection<Friend> AllFriends() => _allFriends.OrderBy(f => f.Name).ToList().AsReadOnly();
    //public static IReadOnlyCollection<Friend> SearchFriends()
    public static void SearchFriends()
    {
        string prompt = "Enter search characters:";
        string error = "Error: Search characters are required and must be twelve (12) or fewer.";
        
        //return _allFriends.Where(f => f.Name.Contains(search)).OrderBy(f => f.Name).ToList().AsReadOnly();
    }

    private static int NewFriendId => AllFriends().Select(f => f.FriendId).Max() + 1;
    private static ConsoleColor ColorFromString(string s) => Enum.GetValues<ConsoleColor>().SingleOrDefault(c => c.ToString().Equals(s, StringComparison.CurrentCultureIgnoreCase));

    public static void SetAllFriends(IEnumerable<Friend> friends) => _allFriends = friends;
    public static string ToJson(IReadOnlyCollection<Friend> friends) => JsonSerializer.Serialize<IEnumerable<Friend>>(friends);
    public static IReadOnlyCollection<Friend> FromJson(string json) => JsonSerializer.Deserialize<IReadOnlyCollection<Friend>>(json) ?? [];
}
