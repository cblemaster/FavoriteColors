
using FavoriteColors.Data.Models;

namespace FavoriteColors.Data;

public interface IDataService
{
    IEnumerable<FriendDTO> AllFriends { get; }
    int GetIdForNewFriend();
    void AddFriend(string firstName, string favColor);
    IEnumerable<FriendDTO> SearchFriends(string searchTerm);

    bool TryLoadData();
    bool TrySaveData();
}
