namespace WebApplication1.GlobalUtils;

public static class Debug
{
    public static void LogColor(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ForegroundColor = ConsoleColor.Gray;
    }
    
    public static void LogError(string text)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(text);
        Console.ForegroundColor = ConsoleColor.Gray;
    }
    
    public static void Log(string text)
    {
        Console.WriteLine(text);
    }
}