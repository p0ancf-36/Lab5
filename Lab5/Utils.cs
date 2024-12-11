namespace Lab5;

using ConsolePosition = (int Left, int Top);

public static class Utils
{
    public static int Mod(int x, int m)
    {
        return (x % m + m) % m;
    }

    public static void Clear(ConsolePosition from, ConsolePosition to)
    {
        GoTo(from);
        Console.Write(new string(' ', Console.BufferWidth * (to.Top - from.Top + 1) - from.Left - to.Left));
        GoTo(from);
    }

    public static void GoTo(ConsolePosition position)
    {
        Console.SetCursorPosition(position.Left, position.Top);
    }
}