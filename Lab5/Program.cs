using System.Text.RegularExpressions;

namespace Lab5;

// Формирование двумерного массива.
// Добавление строки в конец матрицы.

// Формирование массива массивов.
// Удалить все строки, в которых есть нули.

// Формирование строки.
// Печать самого длинного идентификатора (\b[a-zA-Z_]\w*\b)

/* Формирование массивов:
 *      - случайным образом;
 *      - вводом с клавиатуры.
 */

/* Формирование строк:
 *      - из заранее заготовленного словаря;
 *      - с клавиатуры.
 */

/* В двумерных массивах: A[Y, X]
 * Первое число - количество строк
 * Второе число - количество столбцов
 *
 * Измерение №0 - Y
 * Измерение №1 - X
 */

public static partial class Program
{
    public static bool IsEmpty(this Array? array)
    {
        return array is null || array.Length == 0;
    }
    
    public static void Main(string[] args)
    {
        int[,] matrix = {};
        int[][] jaggedArray = [];
        string str = "";

        while (true) // Rider жаловался, так как case прекращения работы использует return
        {            // То есть тут либо низкая сложность восприятия, либо отсутствие while(true)
            int action = Menu.Choose("Что вы хотите сделать?", Message.MainMenu);
            Console.WriteLine();

            switch (action)
            {
                case -1:
                    return;
                case Ids.PrintMatrix:
                    PrintMatrix(matrix);
                    break;
                case Ids.CreateMatrix:
                    matrix = CreateMatrix();
                    break;
                case Ids.AddRowToMatrix:
                    matrix = AddRow(matrix);
                    break;
                case Ids.PrintJaggedArray:
                    PrintJaggedArray(jaggedArray);
                    break;
                case Ids.CreateJaggedArray:
                    jaggedArray = CreateJaggedArray();
                    break;
                case Ids.RemoveZeroRowsJaggedArray:
                    jaggedArray = RemoveZeroRows(jaggedArray);
                    break;
                case Ids.PrintString:
                    PrintString(str);
                    break;
                case Ids.CreateString:
                    str = CreateString();
                    break;
                case Ids.FindLongestIdentifier:
                    FindLongestIdentifier(str);
                    break;
                default:
                    Console.WriteLine("Неизвестная операция!");
                    break;
            }

            Console.WriteLine("\nНажмите любую клавишу, чтобы продолжить");
            Console.ReadKey();
            Console.Clear();
        }
    }

    #region Matrix
    
    private static void PrintMatrix(int[,] array)
    {
        if (array.Length == 0)
        {
            Console.WriteLine(Message.EmptyArray);
            return;
        }
        
        for (int y = 0; y < array.GetLength(0); y++)
        {
            for (int x = 0; x < array.GetLength(1); x++)
            {
                Console.Write($"{array[y, x],-11} ");
            }

            Console.WriteLine();
        }
    }

    public delegate void InitMatrix(int[,] array, int fromX, int fromY, int countX, int countY);
    
    private static int[,] CreateMatrix()
    {
        int width = Input.ReadLength("Введите количество столбцов матрицы: ");
        int height = Input.ReadLength("Введите количество строк матрицы: ");

        int[,] result = new int[height, width];

        Console.WriteLine();
        InitMatrix initializer = Menu.Choose(Message.CreationLabel, Message.MatrixInitMethods);
        initializer.Invoke(result, 0, 0, width, height);

        return result;
    }

    private static int[,] AddRow(int[,] array)
    {
        int[,] result = new int[array.GetLength(0) + 1, array.GetLength(1)];
        Array.Copy(array, result, array.Length);

        InitMatrix initializer = Menu.Choose(Message.ColumnInitLabel, Message.MatrixInitMethods);
        initializer.Invoke(result, 0, array.GetLength(0), array.GetLength(1), 1);

        return result;
    }

    public static void InitMatrixRandom(int[,] array, int fromX, int fromY, int countX, int countY)
    {
        Random random = new Random();
        (int min, int max) = Input.ReadRandomBounds();

        for (int y = fromY; y < fromY + countY; y++)
        {
            for (int x = fromX; x < fromX + countX; x++)
            {
                array[y, x] = random.Next(min, max);
            }
        }
    }

    public static void InitMatrixKeyboard(int[,] array, int fromX, int fromY, int countX, int countY)
    {
        for (int y = fromY; y < fromY + countY; y++)
        {
            for (int x = fromX; x < fromX + countX; x++)
            {
                array[y, x] = Input.ReadInteger($"Введите число в строку №{x + 1} столбец №{y + 1}: ");
            }
        }
    }

    #endregion

    #region Jagged array

    private static void PrintJaggedArray(int[][] jaggedArray)
    {
        if (jaggedArray.IsEmpty())
        {
            Console.WriteLine(Message.EmptyArray);
            return;
        }

        foreach (int[] row in jaggedArray)
        {
            if (row.IsEmpty())
            {
                Console.WriteLine(Message.EmptyArray);
            }
            foreach (int element in row)
            {
                Console.Write($"{element, -11}");
            }

            Console.WriteLine();
        }
    }

    public delegate void InitJaggedArray(int[][] array);
    
    private static int[][] CreateJaggedArray()
    {
        int length = Input.ReadLength("Введите количество строк зубчатого массива: ");
        int[][] result = new int[length][];

        InitJaggedArray initializer = Menu.Choose(Message.CreationLabel, Message.JaggedArrayInitMethods);
        initializer.Invoke(result);

        return result;
    }

    public static void InitJaggedArrayRandom(int[][] array)
    {
        (int min, int max) = Input.ReadRandomBounds();
        Random random = new Random();

        for (int i = 0; i < array.Length; i++)
        {
            int length = Input.ReadLength($"Введите длину строки №{i + 1}: ");
            int[] newArray = new int[length];

            for (int j = 0; j < length; j++)
                newArray[j] = random.Next(min, max);

            array[i] = newArray;
        }
    }

    public static void InitJaggedArrayKeyboard(int[][] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int length = Input.ReadLength($"Введите длину строки №{i + 1}");
            int[] newArray = new int[length];

            for (int j = 0; j < length; j++)
                newArray[j] = Input.ReadInteger($"Введите число в позицию {j + 1}: ");

            array[i] = newArray;
        }
    }

    private static int[][] RemoveZeroRows(int[][] array)
    {
        int c = array.Length;

        int i;
        for (i = 0; i < array.Length; i++)
        {
            if (!array[i].Contains(0)) continue;
            
            array[i] = null;
            c--;
        }

        int[][] result = new int[c][];
        i = 0;
        foreach (var element in array)
        {
            if (element is null) continue;
            
            result[i] = element;
            i++;
        }

        return result;
    }

    #endregion

    #region String

    private static void PrintString(string str)
    {
        Console.WriteLine(str);
    }

    public delegate string ReadString();

    private static string CreateString()
    {
        ReadString initializer = Menu.Choose(Message.StringCreationLabel, Message.StringReadMethods);
        string result = initializer.Invoke();

        return result;
    }

    public static string ReadStringKeyboard()
    {
        string result;
        bool isCorrect;

        do
        {
            Console.Write("Введите строку: ");
            result = Console.ReadLine() ?? "";
            isCorrect = StringRegex().IsMatch(result) && !TooManySpaces().IsMatch(result);

            if (!isCorrect)
            {
                Console.WriteLine(Message.WrongString);
            }
        } while (!isCorrect);
        
        return result;
    }

    public static string ReadStringDictionary()
    {
        return Menu.Choose(Message.DefaultStringsLabel, Message.DefaultStrings);
    }

    private static void FindLongestIdentifier(string str)
    {
        var matches = Regex.Matches(str, @"\b[a-zA-Z_]\w*\b");
        int maxLength = 0;
        
        foreach (Match match in matches)
        {
            maxLength = Math.Max(maxLength, match.Length);
        }

        foreach (Match match in matches)
        {
            if (match.Length == maxLength)
            {
                Console.WriteLine(match.Value);
            }
        }
    }

    [GeneratedRegex("^([\\sA-Za-zА-Яа-я,;:]+[.?!])+$")]
    private static partial Regex StringRegex();

    [GeneratedRegex("[.?!,;:\\s]{2,}")]
    private static partial Regex TooManySpaces();

    #endregion
}