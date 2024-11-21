
namespace FavoriteColors.App.Files;

public class FileHandler
{
    private readonly string _fileDir;
    private readonly string _fullPath;

    public FileHandler(string fileDir, string fullPath)
    {
        _fileDir = fileDir;
        _fullPath = fullPath;

        CreateDirectoryIfNotExists();
        CreateFileIfNotExists();
    }

    public void CreateDirectoryIfNotExists()
    {
        if (!Directory.Exists(_fileDir))
        {
            Directory.CreateDirectory(_fileDir);
        }
    }
    public string TryReadFile()
    {
        try
        {
            using StreamReader sr = new(_fullPath);
            string s = sr.ReadToEnd();
            return s;
        }
        catch (IOException)
        {
            return "Error: Unable to read from data file!";
            throw;
        }
    }
    public bool TryWriteFile(string data)
    {
        try
        {
            using StreamWriter sw = new(_fullPath, false);
            sw.Write(data);
            return true;
        }
        catch (IOException)
        {
            return false;
            throw;
        }
    }
}
