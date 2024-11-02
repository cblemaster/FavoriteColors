
using App.DTO;

namespace App.Api;

public class JsonDataService : IDataService
{
    private readonly string _path = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\favorite-colors";
    private readonly string _fileName = "favorite-colors.json";

    public bool JsonDataDirectoryExists() => Directory.Exists(_path);
    public void CreateJsonDataDirectory() => Directory.CreateDirectory(_path);
    public bool JsonDataFileExists() => File.Exists($"{_path}\\{_fileName}");
    public void CreateJsonDataFile() => File.Create($"{_path}\\{_fileName}");
    public int GetMaxFriendId() => throw new NotImplementedException();
    public FriendDTO CreateFriend(FriendDTO newFriend) => throw new NotImplementedException();
    public FriendDTO[] GetFriendSearch(string searchString) => throw new NotImplementedException();
    public FriendDTO[] GetAllFriends() => throw new NotImplementedException();
}
