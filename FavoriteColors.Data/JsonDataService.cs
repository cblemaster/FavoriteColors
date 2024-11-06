
using FavoriteColors.Data.Models;
using System.Text.Json;

namespace FavoriteColors.Data;

public class JsonDataService : IDataService
{
    private IEnumerable<Friend> _allFriends = [];
    private readonly string Path = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\favorite-colors";
    private readonly string FileName = "favorite-colors.txt";
    
    private string FullPath => $"{Path}\\{FileName}";
    public IEnumerable<Friend> AllFriends => _allFriends.OrderBy(f => f.FirstName);

    public int GetIdForNewFriend() => AllFriends.Any() ? AllFriends.Select(f => f.Id).Max() + 1 : 1;
    public void AddFriend(string firstName, string favColor)
    {
        //TODO >> Validation
        ConsoleColor c = Enum.GetValues<ConsoleColor>().SingleOrDefault(c => c.ToString().Equals(favColor, StringComparison.CurrentCultureIgnoreCase));
        int nextFriendId = GetIdForNewFriend();
        Friend newFriend = new() { Id = nextFriendId, FirstName = firstName, FavoriteColor = c.ToString() };

        _ = _allFriends.Append(newFriend);
    }
    public IEnumerable<Friend> GetAllFriends() => AllFriends;
    public IEnumerable<Friend> SearchFriends(string searchTerm) => AllFriends.Where(f => f.FirstName.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase));
    public bool TryLoadData()
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
                return true;
            }
            return true;
        }
        catch (IOException) { return false; }
    }
    public bool TrySaveData()
    {
        if (!_allFriends.Any()) { return true; }
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
