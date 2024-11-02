
using App.DTO;

namespace App.Api;

public interface IDataService
{
    bool CheckForJsonDataFile();
    void CreateJsonDataFile();
    int GetMaxFriendId();
    FriendDTO CreateFriend(FriendDTO newFriend);
    FriendDTO[] GetFriendSearch(string searchString);
    FriendDTO[] GetAllFriends();
}
