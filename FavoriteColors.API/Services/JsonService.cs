
using FavoriteColors.API.Models;
using System.Text.Json;

namespace FavoriteColors.API.Services
{
    public class JsonService : IJsonService
    {
        public IEnumerable<Friend> DeserializeJsonToCollection(string json) =>
            JsonSerializer.Deserialize<IEnumerable<Friend>>(json) ?? [];
        public string SerializeCollectionToJson(IEnumerable<Friend> friends) =>
            JsonSerializer.Serialize<IEnumerable<Friend>>(friends);
    }
}
