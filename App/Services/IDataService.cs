using App.DTO;

namespace App.Services;

public interface IDataService
{
    uint GetNewFriendId();
    void CreateFriend(FriendDTO newFriend);
    FriendDTO[] GetFriendSearch(string searchString);
    FriendDTO[] GetAllFriends();
}
