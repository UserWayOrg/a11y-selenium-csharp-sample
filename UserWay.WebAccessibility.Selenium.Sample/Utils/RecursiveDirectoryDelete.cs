namespace UserWay.WebAccessibility.Selenium.Sample.utils;

public class RecursiveDirectoryDelete
{
    public static void DeleteDirectoryRecursively(string path)
    {
        var directory = new DirectoryInfo(path);

        if (!directory.Exists)
        {
            throw new DirectoryNotFoundException($"Directory '{path}' does not exist or could not be found.");
        }

        foreach (var file in directory.GetFiles())
        {
            file.Delete(); 
        }

        foreach (var subDirectory in directory.GetDirectories())
        {
            DeleteDirectoryRecursively(subDirectory.FullName); 
        }

        directory.Delete();  
    }

    private RecursiveDirectoryDelete() { }
}