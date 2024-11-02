
using App.DTO;

namespace App.Api;

public class JsonDataService : IDataService
{
    private readonly string _path = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\favorite-colors.json";
    public bool CheckForJsonDataFile() => File.Exists(_path);
    public void CreateJsonDataFile() => File.Create(_path);
    public int GetMaxFriendId() => throw new NotImplementedException();
    public FriendDTO CreateFriend(FriendDTO newFriend) => throw new NotImplementedException();
    public FriendDTO[] GetFriendSearch(string searchString) => throw new NotImplementedException();
    public FriendDTO[] GetAllFriends() => throw new NotImplementedException();
}
