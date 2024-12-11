namespace Lab5;

public static class Menu
{
    public static T Choose<T>(string label, Dictionary<string, T> variants)
    {
        Console.WriteLine(label);

        var start = Console.GetCursorPosition();
        ConsoleKey key;
        int active = 0;
        
        Dictionary<int, string> identifiers = new();

        do
        {
            Utils.Clear(start, Console.GetCursorPosition());
            
            foreach (var (variant, i) in variants.Zip(Enumerable.Range(0, variants.Count)))
            {
                if (active == i)
                {
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                
                identifiers[i] = variant.Key;
                Console.WriteLine($"{variant.Key}");
                
                Console.ResetColor();
            }

            key = Console.ReadKey().Key;

            active += key switch
            {
                ConsoleKey.UpArrow => -1,
                ConsoleKey.DownArrow => 1,
                _ => 0
            };

            active = Utils.Mod(active, variants.Count);

        } while (key != ConsoleKey.Enter);

        return variants[identifiers[active]];
    }
}