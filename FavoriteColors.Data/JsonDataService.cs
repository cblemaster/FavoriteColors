
using FavoriteColors.Data.Models;
using System.Text.Json;

namespace FavoriteColors.Data;

public class JsonDataService : IDataService
{
    private List<Friend> _allFriends = [];
    private readonly string Path = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\favorite-colors";
    private readonly string FileName = "favorite-colors.txt";
    
    private string FullPath => $"{Path}\\{FileName}";
    public IReadOnlyCollection<Friend> AllFriends => [.. _allFriends.OrderBy(f => f.FirstName).ToList()];

    internal int GetIdForNewFriend() => AllFriends.Count == 0 ? 1 : AllFriends.Select(f => f.Id).Max() + 1;
    internal void AddFriend(string firstName, string favColor)
    {
        //TODO >> Validation
        ConsoleColor color = Enum.GetValues<ConsoleColor>().SingleOrDefault(c => c.ToString().Equals(favColor, StringComparison.InvariantCultureIgnoreCase));
        int nextFriendId = GetIdForNewFriend();
        Friend newFriend = new() { Id = nextFriendId, FirstName = firstName, FavoriteColor = color.ToString() };
        _allFriends.Add(newFriend);
    }
    internal IEnumerable<Friend> SearchFriends(string searchTerm) => AllFriends.Where(f => f.FirstName.Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase));
    internal bool TryLoadData()
    {
        try
        {
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }
            if (!File.Exists($"{Path}\\{FileName}"))
            {
                File.Create($"{Path}\\{FileName}");
                return true;
            }

            using StreamReader sr = new(FullPath);
            string s = sr.ReadToEnd();
            if (!string.IsNullOrEmpty(s))
            {
                _allFriends = JsonSerializer.Deserialize<List<Friend>>(s) ?? [];
            }
            return true;
        }
        catch (IOException) { return false; }
    }
    internal bool TrySaveData()
    {
        if (_allFriends.Count == 0) { return true; }
        try
        {
            string jsonString = JsonSerializer.Serialize<IEnumerable<Friend>>(AllFriends);
            using StreamWriter sw = new(FullPath, false);
            sw.Write(jsonString);
            return true;
        }
        catch (IOException) { return false; }
    }
}
