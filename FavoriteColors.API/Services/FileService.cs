
namespace FavoriteColors.API.Services;

public class FileService : IFileService
{
    private readonly string _appDataPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}";

    private string DirName { get; init; }
    private string FileName { get; init; }

    private string DirPath => Path.Combine(_appDataPath, DirName);
    private string FilePath => Path.Combine(DirPath, FileName);

    public FileService(string dir, string file)
    {
        DirName = dir;
        FileName = file;

        TryCreateDirectoryIfNotExists();
        TryCreateFileIfNotExists();
    }

    public bool TryCreateDirectoryIfNotExists()
    {
        try
        {
            if (!Directory.Exists(DirPath))
            {
                _ = Directory.CreateDirectory(DirPath);
            }
            return true;
        }
        catch (IOException)
        {
            return false;
            throw;
        }
    }
    public bool TryCreateFileIfNotExists()
    {
        try
        {
            if (!File.Exists(FilePath))
            {
                using FileStream _ = File.Create(FilePath);
            }
            return true;
        }
        catch (IOException)
        {
            return false;
            throw;
        }
    }
    public string TryReadFile()
    {
        try
        {
            using StreamReader sr = new(FilePath);
            return sr.ReadToEnd();
        }
        catch (IOException)
        {
            return string.Empty;
            throw;
        }
    }
    public bool TryWriteFile(string text)
    {
        try
        {
            using StreamWriter sw = new(FilePath, false);
            sw.Write(text);
            return true;
        }
        catch (IOException)
        {
            return false;
            throw;
        }
    }
}
