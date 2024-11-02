using App.Models;
using System.Collections.ObjectModel;

namespace App.Services;

public class JsonDataService
{
    private readonly FileService _fileService = new();
    private IEnumerable<Friend> _allFriends;
    
    public ReadOnlyCollection<Friend> AllFriends => _allFriends.ToList().AsReadOnly();
    private uint NextFriendId => AllFriends.Select(f => f.Id).Max() + 1;
    
    public void CreateFriend(Friend newFriend) => _allFriends.Append(newFriend);  // TODO>> may need to set id here
    public Friend[] GetFriendSearch(string searchString) => AllFriends.Where(f => f.FirstName.Contains(searchString)).ToArray();
}
