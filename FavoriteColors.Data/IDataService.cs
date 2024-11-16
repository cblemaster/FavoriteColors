
using FavoriteColors.Data.Models;

namespace FavoriteColors.Data;

interface IDataService
{
    IReadOnlyCollection<Friend> AllFriends { get; }
    int GetIdForNewFriend();
    void AddFriend(string firstName, string favColor);
    IReadOnlyCollection<Friend> SearchFriends(string searchTerm);
    bool TryLoadData();
    bool TrySaveData();
}
