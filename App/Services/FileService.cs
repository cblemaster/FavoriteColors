
namespace App.Services;

internal class FileService
{
    private readonly string _path = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\favorite-colors";
    private readonly string _fileName = "favorite-colors.txt";
    private string FullPath => $"{_path}\\{_fileName}";

    internal FileService()
    {
        if (!Directory.Exists(_path))
        {
            Directory.CreateDirectory(_path);
        }
        if (!File.Exists($"{_path}\\{_fileName}"))
        {
            File.Create($"{_path}\\{_fileName}");
        }
    }

    internal string TryReadFile()
    {
        try
        {
            using StreamReader sr = new(FullPath);
            return sr is null ? throw new IOException("Error occurred reading data file.") : sr.ReadToEnd();
        }
        catch (IOException) { throw; }
    }
    internal void TryWriteFile(string fileContents)
    {
        try
        {
            using StreamWriter sw = new(FullPath, true);
            sw.Write(fileContents);
        }
        catch (IOException) { throw; }
    }
}
