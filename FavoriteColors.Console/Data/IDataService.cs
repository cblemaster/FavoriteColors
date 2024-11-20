using FavoriteColors.App.Models;

namespace FavoriteColors.App.Data
{
    public interface IDataService
    {
        IReadOnlyCollection<FriendDTO> AllFriends { get; }
        Task<IAsyncEnumerable<FriendDTO?>> GetData();
        Task SaveData();
        void AddFriend(string name, string favoriteColor);
        FriendDTO[] SearchFriends(string search);
    }
}
