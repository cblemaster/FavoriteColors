
namespace FavoriteColors.App.Files;

public class FileHandler(string fileDir, string filePath) : IFileHandler
{
    private readonly string _fileDir = fileDir;
    private readonly string _filePath = filePath;

    public string TryReadFile()
    {
        if (!File.Exists(_filePath))
        {
            return string.Empty;
        }
        try
        {
            using StreamReader sr = new(_filePath);
            string s = sr.ReadToEnd();
            return s;
        }
        catch (IOException)
        {
            return "Error: Unable to read from data file!";
        }
    }
    public bool TryWriteFile(string data)
    {
        try
        {
            _ = Directory.CreateDirectory(_fileDir);
            using FileStream fs = File.Create(_filePath);
            fs.Dispose();

            using StreamWriter sw = new(_filePath, false);
            sw.Write(data);
            return true;
        }
        catch (IOException)
        {
            return false;
        }
    }
}
