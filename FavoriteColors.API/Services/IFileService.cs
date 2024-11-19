namespace FavoriteColors.API.Services;

public interface IFileService
{
    bool TryCreateDirectoryIfNotExists();
    bool TryCreateFileIfNotExists();
    string TryReadFile();
    bool TryWriteFile(string text);
}
