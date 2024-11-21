
namespace FavoriteColors.App.Files;

public interface IFileHandler
{
    string TryReadFile();
    bool TryWriteFile(string data);
}