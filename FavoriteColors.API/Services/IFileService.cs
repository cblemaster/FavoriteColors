
namespace FavoriteColors.API.Services;

public interface IFileService
{
    void CreateDirectoryIfNotExists();
    void CreateFileIfNotExists();
    string TryReadFile();
    bool TryWriteFile(string data);
}
