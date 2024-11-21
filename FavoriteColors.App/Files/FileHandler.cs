
namespace FavoriteColors.App.Files;

public class FileHandler(string fullPath) : IFileHandler
{
    private readonly string _fullPath = fullPath;

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
