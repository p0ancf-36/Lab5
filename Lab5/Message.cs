namespace Lab5;

public static class Message
{
    public static readonly Dictionary<string, Program.InitMatrix> MatrixInitMethods = new()
    {
        {"Случайным образом", Program.InitMatrixRandom},
        {"Вводом с клавиатуры", Program.InitMatrixKeyboard},
    };

    public static readonly Dictionary<string, Program.InitJaggedArray> JaggedArrayInitMethods = new()
    {
        { "Случайным образом", Program.InitJaggedArrayRandom },
        { "Вводом с клавиатуры", Program.InitJaggedArrayKeyboard },
    };

    public static readonly Dictionary<string, Program.ReadString> StringReadMethods = new()
    {
        {"С клавиатуры", Program.ReadStringKeyboard},
        {"Выбрать из стандартного словаря", Program.ReadStringDictionary}
    };

    public static readonly Dictionary<string, int> MainMenu = new()
    {
        {"Вывести двумерный массив (матрицу) на печать", Ids.PrintMatrix},
        {"Создать двумерный массив (матрицу)", Ids.CreateMatrix},
        {"Добавить строку в конец двумерного массива", Ids.AddRowToMatrix},
        {"Вывести зубчатый массив на печать", Ids.PrintJaggedArray},
        {"Создать зубчатый массив", Ids.CreateJaggedArray},
        {"Удалить все строки, содержащие нули, из зубчатого массива", Ids.RemoveZeroRowsJaggedArray},
        {"Вывести строку на печать", Ids.PrintString},
        {"Ввести новую строку", Ids.CreateString},
        {"Найти самый длинный идентификатор в строке", Ids.FindLongestIdentifier},
        {"Завершить работу программы", -1},
    };

    private static readonly string[] DefaultStringsArray = [
        "static void PrintUpper string info12346: WriteLine ToUpper info, 1234info.",
        "static void PrintUppe string info12346: WriteLine ToUpper info, 1234info.",
        "В лесу родилась елочка. В лесу она росла. Зимой и летом стройная, зеленая была."
    ];
    public static readonly Dictionary<string, string> DefaultStrings;

    public const string CreationLabel = "Как вы хотите инициализировать массив?";
    public const string StringCreationLabel = "Как вы хотите ввести новую строку?";
    public const string ColumnInitLabel = "Как вы хотите инициализировать строку?";
    public const string DefaultStringsLabel = "Выберите строку из стандартного словаря";
    
    public const string Nan = "Вы ввели не число";
    public const string NanInteger = "Вы ввели не целое число";
    public const string EmptyArray = "Пустой массив";

    public const string IntegerOverflow = "Вы ввели слишком большое или слишком маленькое число";
    public const string LengthOverflow = "Вы ввели слишком большое или неположительное число";
    public const string WrongString = "Введенная вами строка не соответствует условиям";
    
    public const string TryAgain = "Пожалуйста, попробуйте еще раз";

    public static string WrapTryAgain(string message) => $"{message}! {TryAgain}.";

    static Message()
    {
        DefaultStrings = new Dictionary<string, string>();

        foreach (var s in DefaultStringsArray)
        {
            DefaultStrings[s] = s;
        }
    }
}