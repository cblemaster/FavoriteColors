
using FavoriteColors.App.Models;
using System.Text.Json;

namespace FavoriteColors.App.Data;

public static class FriendsData
{
    private static IEnumerable<Friend> _allFriends = null!;

    public static void AddFriend(string name, string favColor) => _allFriends.ToList().Add(new(NewFriendId, name, ColorFromString(favColor)));
    public static IReadOnlyCollection<Friend> AllFriends() => _allFriends.OrderBy(f => f.Name).ToList().AsReadOnly();
    public static IReadOnlyCollection<Friend> SearchFriends(string search) =>
        _allFriends.Where(f => f.Name.Contains(search)).OrderBy(f => f.Name).ToList().AsReadOnly();

    private static int NewFriendId => AllFriends().Select(f => f.FriendId).Max() + 1;
    private static ConsoleColor ColorFromString(string s) => Enum.GetValues<ConsoleColor>().SingleOrDefault(c => c.ToString().Equals(s, StringComparison.CurrentCultureIgnoreCase));

    public static string ToJson(IReadOnlyCollection<Friend> friends) => JsonSerializer.Serialize<IEnumerable<Friend>>(friends);
    public static IReadOnlyCollection<Friend> FromJson(string json) => JsonSerializer.Deserialize<IReadOnlyCollection<Friend>>(json) ?? [];
}
