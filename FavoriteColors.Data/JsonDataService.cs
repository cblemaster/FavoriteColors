
using FavoriteColors.Data.Models;

namespace FavoriteColors.Data;

public class JsonDataService : IDataService
{
    private readonly IEnumerable<Friend> _allFriends = [];
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
}
