
using FavoriteColors.App.Models;
using System.Text.Json;

namespace FavoriteColors.App.Data;

public static class FriendsData
{
    public static IReadOnlyCollection<Friend> AllFriendsReadOnly => AllFriends.OrderBy(f => f.Name).ToList().AsReadOnly();
    private static List<Friend> AllFriends { get; set; } = [];

    public static void SetAllFriends(IEnumerable<Friend> friends) => AllFriends = friends.ToList();
    public static void AddFriend(Friend friend) => AllFriends.Add(friend);
    public static string ToJson(IReadOnlyCollection<Friend> friends) => JsonSerializer.Serialize<IEnumerable<Friend>>(friends);
    public static IReadOnlyCollection<Friend> FromJson(string json) => JsonSerializer.Deserialize<IReadOnlyCollection<Friend>>(json) ?? [];
}
