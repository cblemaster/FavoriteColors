
using FavoriteColors.Data.Models;

namespace FavoriteColors.Data;

public interface IDataService
{
    IEnumerable<Friend> AllFriends { get; }
    int GetIdForNewFriend();
    void AddFriend(string firstName, string favColor);
    IEnumerable<Friend> GetAllFriends();
    IEnumerable<Friend> SearchFriends(string searchTerm);

    bool TryLoadData();
    bool TrySaveData();
}
