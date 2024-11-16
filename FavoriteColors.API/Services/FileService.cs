
namespace FavoriteColors.API.Services;

internal class FileService : IFileService
{
    private readonly string _fileDir;
    private readonly string _fullPath;

    public FileService(string fileDir, string fullPath)
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
    public void CreateFileIfNotExists()
    {
        if (!File.Exists($"{_fullPath}"))
        {
            File.Create($"{_fullPath}");
        }
    }
    public string TryReadFile()       // TODO>> think about more performant return type
    {
        try
        {
            using StreamReader sr = new(_fullPath);
            string s = sr.ReadToEnd();
            return s;
        }
        catch (IOException) { return "Error: Unable to read from data file!"; }
    }
    public bool TryWriteFile(string data)     // TODO>> think about input param with smaller footprint
    {
        try
        {
            using StreamWriter sw = new(_fullPath, false);
            sw.Write(data);
            return true;
        }
        catch (IOException) { return false; }
    }
}
