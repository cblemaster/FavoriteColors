using FavoriteColors.App.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace FavoriteColors.App.Data;

public class HttpDataService : IDataService
{
    private const string BASE_URI = "https://localhost:7088/";
    private readonly HttpClient _client;

    private ICollection<FriendDTO> _allFriends = [];
    private int NewFriendId => AllFriends.Select(f => f.FriendId).Max() + 1;
    private ConsoleColor ColorFromString(string s) => Enum.GetValues<ConsoleColor>().SingleOrDefault(c => c.ToString().Equals(s, StringComparison.InvariantCultureIgnoreCase));

    public IReadOnlyCollection<FriendDTO> AllFriends => _allFriends.OrderBy(f => f.Name).ToList().AsReadOnly();

    public HttpDataService() => _client = new HttpClient { BaseAddress = new Uri(BASE_URI) };

    public async Task<IAsyncEnumerable<FriendDTO?>> GetData()
    {
        HttpResponseMessage response = await _client.GetAsync("/read");
        return response.Content.ReadFromJsonAsAsyncEnumerable<FriendDTO>();
    }
    public async Task SaveData()
    {
        StringContent content = new(JsonSerializer.Serialize(AllFriends));
        content.Headers.ContentType = new("application/json");
        await _client.PostAsync("/write", content);
    }

    public void AddFriend(string name, string favoriteColor) => _allFriends.Add(new(NewFriendId, name, ColorFromString(favoriteColor)));
    public FriendDTO[] SearchFriends(string search) => [.. AllFriends.Where(f => f.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)).OrderBy(f => f.Name)];
}
