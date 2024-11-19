
using FavoriteColors.API.Models;

namespace FavoriteColors.API.Services;

public interface IJsonService
{
    IEnumerable<Friend> DeserializeJsonToCollection(string json);
    string SerializeCollectionToJson(IEnumerable<Friend> friends);
}
