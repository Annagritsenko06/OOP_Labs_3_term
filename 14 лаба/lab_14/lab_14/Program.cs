using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Timers;

class Program
{
    
    static void PrintRunningProcesses()
    {
        Console.WriteLine("Запущенные процессы:");
        Process[] processes = Process.GetProcesses();
        foreach (var process in processes)
        {
            try
            {
                Console.WriteLine($"ID: {process.Id}, Name: {process.ProcessName}, Priority: {process.PriorityClass}, Start Time: {process.StartTime}, State: {process.Responding}, CPU Time: {process.TotalProcessorTime}");
            }
            catch
            {
               
            }
        }
    }

  
    static void InvestigateAppDomain()
    {
        var currentDomain = AppDomain.CurrentDomain;
        Console.WriteLine($"\nТекущий домен: {currentDomain.FriendlyName}");

        foreach (var assembly in currentDomain.GetAssemblies())
        {
            Console.WriteLine($"Сборка: {assembly.GetName().Name}");
        }

     
        var newDomain = AppDomain.CreateDomain("NewDomain");
        Console.WriteLine("Создан новый домен: NewDomain");
        AppDomain.Unload(newDomain);
        Console.WriteLine("Новый домен выгружен.");
    }


    static void CalculatePrimesInThread()
    {
        Console.Write("Введите число n для расчета простых чисел: ");
        int n = int.Parse(Console.ReadLine());
        Thread thread = new Thread(() => CalculatePrimes(n));
        thread.Start();

        Thread.Sleep(2000); 
        Console.WriteLine("Приостановка потока...");
        thread.Suspend();
        Thread.Sleep(2000);
        Console.WriteLine("Возобновление потока...");
        thread.Resume();
    }

    static void CalculatePrimes(int n)
    {
        using (StreamWriter writer = new StreamWriter("primes.txt"))
        {
            for (int i = 2; i <= n; i++)
            {
                if (IsPrime(i))
                {
                    Console.WriteLine(i);
                    writer.WriteLine(i);
                }
            }
        }
    }

    static bool IsPrime(int number)
    {
        if (number < 2) return false;
        for (int i = 2; i <= Math.Sqrt(number); i++)
        {
            if (number % i == 0) return false;
        }
        return true;
    }

   
    static void PrintEvenAndOddNumbers()
    {
        Console.Write("Введите число n для вывода четных и нечетных чисел: ");
        int n = int.Parse(Console.ReadLine());

        Thread evenThread = new Thread(() => PrintEvenNumbers(n));
        Thread oddThread = new Thread(() => PrintOddNumbers(n));

        evenThread.Start();
        oddThread.Start();

        evenThread.Join();
        oddThread.Join();
    }

    static object lockObj = new object();

    static void PrintEvenNumbers(int n)
    {
        for (int i = 2; i <= n; i += 2)
        {
            lock (lockObj)
            {
                Console.WriteLine(i);
                Thread.Sleep(100); 
            }
        }
    }

    static void PrintOddNumbers(int n)
    {
        for (int i = 1; i <= n; i += 2)
        {
            lock (lockObj)
            {
                Console.WriteLine(i);
                Thread.Sleep(200); 
            }
        }
    }

    static void StartRepeatingTask()
    {
        timer = new System.Timers.Timer(2000); 
        timer.Elapsed += OnTimedEvent;
        timer.AutoReset = true;
        timer.Enabled = true;
        Console.WriteLine("Запущена повторяющаяся задача с таймером.");
    }

    private static void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        Console.WriteLine($"Задача выполнена в {e.SignalTime:HH:mm:ss.fff}");
    }
    
    private static System.Timers.Timer timer;

    static void Main()
    {
       
        PrintRunningProcesses();

        InvestigateAppDomain();

        PrintEvenAndOddNumbers();

        StartRepeatingTask();

        CalculatePrimesInThread();
        Console.WriteLine("Нажмите Enter для завершения.");
        Console.ReadLine();
    }

}