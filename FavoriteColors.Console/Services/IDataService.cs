
using FavoriteColors.Console.DataTransferObjects;

namespace FavoriteColors.Console.Services
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
