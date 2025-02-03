using System;
using System.Text;


public class Program
{
    public static void uncheckedFunction()
    {
        unchecked
        {
            int a = Int32.MaxValue;
            Console.WriteLine(a + 1);
        }
    }

    public static void checkedFunction()
    {
        checked
        {
            int a = Int32.MaxValue;
            Console.WriteLine(a + 1);
        }
    }

    public static void Main(string[] args)
    {
        bool isTrue = true;

        Console.WriteLine("Введите любое целое число: ");
        byte firstNumber = Convert.ToByte(Console.ReadLine());
        Console.WriteLine("Вы ввели это число: " + firstNumber);

        Console.WriteLine("\nВведите любой символ: ");
        char symbol = Convert.ToChar(Console.ReadLine());
        Console.WriteLine("Вы ввели этот символ: " + symbol);

        Console.WriteLine("\nВведите любое десятичное число: ");
        decimal secondNumber = Convert.ToDecimal(Console.ReadLine());
        Console.WriteLine("Вы ввели это число: " + secondNumber);

        Console.WriteLine("\nВведите любое число: ");
        double thirdNumber = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine("Вы ввели это число: " + thirdNumber);

        Console.WriteLine("\nВведите любое число с плавающей точкой: ");
        float fourthNumber = Convert.ToSingle(Console.ReadLine());
        Console.WriteLine("Вы ввели это число: " + fourthNumber);

        Console.WriteLine("\nВведите любое целое число: ");
        int fifthNumber = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Вы ввели это число: " + fifthNumber);

        Console.WriteLine("\nВведите любое целое число: ");
        long sixthNumber = Convert.ToInt64(Console.ReadLine());
        Console.WriteLine("Вы ввели это число: " + sixthNumber);

        Console.WriteLine("\nВведите любое целое число: ");
        short seventhNumber = Convert.ToInt16(Console.ReadLine());
        Console.WriteLine("Вы ввели это число: " + seventhNumber);

        //Неявные преобразования

        int number_1 = 2412432;
        long number_1_long = number_1;

        short number_2 = 1243;
        int number_2_integer = number_2;

        byte number_3 = 213;
        short number_3_short = number_3;

        uint number_4 = 234235135;
        double number_4_double = number_4;

        float number_5 = 12.3f;
        double number_5_double = number_5;

        // //Явные преобразования

        int number_6 = 234132;
        short number_6_short = (short)number_6;

        float number_7 = 2131.35325423f;
        int number_7_integer = (int)number_7;

        double number_8 = 345.235345523562356235;
        float number_8_floating_point = (float)number_8;

        short number_9 = 12441;
        byte number_9_byte = (byte)number_9;

        double number_10 = 23124.24542345;
        short number_10_short = (short)number_10;

        //Упаковка и распаковка значимых типов

        int firstVariable = 0;
        object firstObject = firstVariable;
        int firstVariableForUnboxing = (int)firstObject;

        long secondVariable = 1;
        object secondObject = secondVariable;
        long secondVariableForUnboxing = (long)secondObject;

        float thirdVariable = 1.1f;
        object thirdObject = thirdVariable;
        float thirdVariableForUnboxing = (float)thirdObject;


        //Nullable переменная

        int? nullableVariable = null;
        Console.WriteLine("\nпеременная с возможным значением null содержит " + nullableVariable);

        nullableVariable = 123;
        Console.WriteLine("\nСейчас она содержит " + nullableVariable);

        // //Неявно типизированная переменная

        var implicitlyTyped = 124;
        Console.WriteLine("\nНеявно типизированная переменная: " + implicitlyTyped);

        //var implicitlyTyped = "Hello";

        //Объявите строковые литералы. Сравните их

        string str1 = "I like";
        string str2 = "banana";
        int result = String.Compare(str1, str2);

        if (result < 0)
        {
            Console.WriteLine("\n 1 строка  больше 2");
        }
        else if (result > 0)
        {
            Console.WriteLine("\n2 строка больше 1");
        }
        else
        {
            Console.WriteLine("\nСтроки одинаковы");
        }

        string firstString = "подстрока";
        string secondString = "лимон";
        string thirdString = "яблоко";
        string stringForConcat = String.Concat(firstString, secondString, thirdString);
        Console.WriteLine("\nСоединенные строки: " + stringForConcat);

        string stringForCopy = String.Copy(firstString);
        Console.WriteLine("\nСкопированная первая строка: " + stringForCopy);

        string stringForSubString = firstString.Substring(1, firstString.Length - 1);
        Console.WriteLine("\nПодстрока первой подстроки: " + stringForSubString);

        string[] words = thirdString.Split(' ');
        foreach (var word in words)
        {
            Console.WriteLine($"{word}");
        }

        string stringForPaste = firstString.Insert(2, secondString);
        Console.WriteLine("\nПервая строка с вставленной подстрокой, начиная со 2-й позиции: " + stringForPaste);
        Console.WriteLine("\nПервая строка с удалённой подстрокой с 2-й по 7-ю позицию: " + stringForPaste.Remove(2, 5));
        Console.WriteLine($"\nИнтерполяция строк: {firstString}, {secondString}, {thirdString}");

        //Пустая и null строка

        string emptyString = "";
        string nullString = null;
        Console.WriteLine("Результат метода IsNullOrEmpty для emptyString: " + String.IsNullOrEmpty(emptyString));
        Console.WriteLine("Результат метода IsNullOrEmpty для nullString: " + String.IsNullOrEmpty(nullString));
        Console.WriteLine("Мы можем объединить их: " + emptyString + nullString);
        Console.WriteLine("Мы можем сравнить их с помощью метода Compare: " + String.Compare(emptyString, nullString));//null больше

        // Создайте строку на основе StringBuilder

        StringBuilder newString = new StringBuilder("Строка на основе StringBuilder", 50);
        newString.Remove(0, 6);
        newString.Append("Aааааааа");
        newString.Insert(0, "Bввввввв");
        Console.WriteLine("\n" + newString);

        //Создайте целый двумерный массив и выведите его на консоль в отформатированном виде (матрица)

        int[,] matrix = new int[3, 4] { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9, 10, 11, 12 } };
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Console.Write(matrix[i, j] + " ");
            }
            Console.WriteLine();
        }

        string[] stringArray = new[] { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота", "Воскресенье" };
        for (int i = 0; i < stringArray.Length; i++)
        {
            Console.Write(stringArray[i] + " ");
        }
        Console.WriteLine("\nДлина вашего массива: " + stringArray.Length);
        Console.WriteLine("Введите индекс элемента, который хотите заменить: ");
        int index = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Введите новую строку: ");
        stringArray[index] = Console.ReadLine();
        for (int i = 0; i < stringArray.Length; i++)
        {
            Console.Write(stringArray[i] + " ");
        }
        Console.WriteLine();

        double[][] stair_array = new double[3][];
        stair_array[0] = new double[2];
        stair_array[1] = new double[3];
        stair_array[2] = new double[4];
        Console.WriteLine("Введите 9 чисел...");
        for (int i = 0; i < 2; i++)
        {
            stair_array[0][i] = Convert.ToDouble(Console.ReadLine());
        }
        for (int i = 0; i < 3; i++)
        {
            stair_array[1][i] = Convert.ToDouble(Console.ReadLine());
        }
        for (int i = 0; i < 4; i++)
        {
            stair_array[2][i] = Convert.ToDouble(Console.ReadLine());
        }
        Console.WriteLine("\nЗначения ступенчатого массива:");
        for (int i = 0; i < stair_array.Length; i++)  
        {
            for (int j = 0; j < stair_array[i].Length; j++)  
            {
                Console.Write(stair_array[i][j] + " ");
            }
            Console.WriteLine(); 
        }

        var array = new[] { "Я", "пишу", "на", "С#" };
        var newStringTwo = "Hello, world"; 

        //Кортежи

        (int tupleInt, string tupleString, char tupleChar, string tupleString2, ulong tupleUlong) tuple = (100, "милка", 'о', "лапка", 6666667);
        Console.WriteLine(tuple);
        Console.WriteLine($"Элемент 1: {tuple.Item1}, Элемент 3: {tuple.Item3}, Элемент 5: {tuple.Item5}");
        (int tupleInt, string tupleString, _, _, ulong tupleUlong) = tuple;

      
        Console.WriteLine($"Элемент 1: {tupleInt}, Элемент 2: {tupleString}, Элемент 5: {tupleUlong}");

        int number = tuple.tupleInt;
        string firstString2 = tuple.tupleString;
        char character = tuple.tupleChar;
        string secondString3 = tuple.tupleString2;
        ulong ulongNumber = tuple.tupleUlong;

        Console.WriteLine($"Элемент 1: {number}, Элемент 2: {firstString2}, Элемент 3: {character}, Элемент 4: {secondString3}, Элемент 5: {ulongNumber}");

        string tmpStringVariable = tuple.tupleString;

        Console.WriteLine("Мы используем переменную из распакованного кортежа: " + tmpStringVariable);
        (int tupleInt, string tupleString, char tupleChar, string tupleString2, ulong tupleUlong) tuple2 = (100, "милка", 'о', "лапка", 6666667);
        if (tuple == tuple2)
        {
            Console.WriteLine("Кортежи одинаковы");
        }
        else
        {
            Console.WriteLine("Кортежи разные");
        }

        //Локальная функция

        (int maxValue, int minValue, int sumOfElements, char firstSymbol) LocalFunction(int[] arrayForFunction,
            string stringForFunction)
        {
            int sum = 0;
            int max = Int32.MinValue;
            int min = Int32.MaxValue;
            for (int i = 0; i < arrayForFunction.Length; i++)
            {
                sum += arrayForFunction[i];
                if (arrayForFunction[i] < min)
                {
                    min = arrayForFunction[i];
                }

                if (arrayForFunction[i] > max)
                {
                    max = arrayForFunction[i];
                }
            }

            return (max, min, sum, stringForFunction[0]);
        };

        Console.WriteLine(LocalFunction(new[] { 1, 2, -4, 7 }, "Анна"));

        //Checked и unchecked

        checkedFunction();
        uncheckedFunction();
    }
    }