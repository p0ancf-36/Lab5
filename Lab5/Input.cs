namespace Lab5;

public static class Input
{
    public static int ReadInteger(string label = "")
    {
        return ReadIntegerGeneralized(label: label, nanMessage: Message.WrapTryAgain(Message.NanInteger),
            overflowMessage: Message.WrapTryAgain(Message.IntegerOverflow));
    }

    public static int ReadIntegerGe(int min, string label = "", string overflowMessage = Message.IntegerOverflow)
    {
        return ReadIntegerGeneralized(min: min, label: label, nanMessage: Message.WrapTryAgain(Message.NanInteger),
            overflowMessage: overflowMessage);
    }
    
    public static int ReadLength(string label = "")
    {
        return ReadIntegerGeneralized(1, int.MaxValue, label, Message.WrapTryAgain(Message.NanInteger),
            Message.WrapTryAgain(Message.LengthOverflow));
    }

    public static (int min, int max) ReadRandomBounds()
    {
        int min = Input.ReadInteger("Введите нижнюю границу генерации: ");
        int max = Input.ReadIntegerGe(min, "Введите верхнюю границу генерации: ",
            Message.WrapTryAgain($"Верхняя граница генерации должна быть больше меньше ({min})"));

        return (min, max);
    }
    
    public static int ReadIntegerGeneralized(
        int min = int.MinValue,
        int max = int.MaxValue,
        string label = "",
        string nanMessage = "",
        string overflowMessage = "")
    {
        var result = 0;
        var isCorrect = false;

        var startPosition = Console.GetCursorPosition();

        do
        {
            Console.Write(label);
            var line = Console.ReadLine()!;
            var position = Console.GetCursorPosition();

            try
            {
                result = int.Parse(line);

                if (result > max || result < min)
                    throw new OverflowException();

                isCorrect = true;
            }
            catch (OverflowException)
            {
                Utils.Clear(startPosition, position);
                Console.WriteLine(overflowMessage);
            }
            catch (Exception)
            {
                Utils.Clear(startPosition, position);
                Console.WriteLine(nanMessage);
            }
        } while (!isCorrect);

        return result;
    }
}