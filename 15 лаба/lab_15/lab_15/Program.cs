using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

class Program
{

    static void FindPrimes(int max, CancellationToken token)
    {
        bool[] isPrime = new bool[max + 1];
        for (int i = 2; i <= max; i++)
            isPrime[i] = true;

        for (int p = 2; p * p <= max; p++)
        {
            if (isPrime[p])
            {
                for (int i = p * p; i <= max; i += p)
                {
                    isPrime[i] = false;
                    token.ThrowIfCancellationRequested();
                }
            }
        }

        for (int i = 2; i <= max; i++)
        {
            if (isPrime[i])
                Console.WriteLine(i);
        }
    }

    static int CalculateValue(int input)
    {
        return input * input; 
    }

    static int CombineResults(int[] results)
    {
        return results.Sum();
    }

    static void SupplyGoods(BlockingCollection<string> warehouse, int supplierId)
    {
        string item = $"Item from Supplier {supplierId}";
        warehouse.Add(item);
        Console.WriteLine($"{item} added to warehouse.");
    }

    static void BuyGoods(BlockingCollection<string> warehouse, int customerId)
    {
        if (warehouse.TryTake(out string item))
        {
            Console.WriteLine($"Customer {customerId} bought {item}.");
        }
        else
        {
            Console.WriteLine($"Customer {customerId} found no items and left.");
        }
    }

    static async Task SomeAsyncMethod()
    {
        await Task.Delay(1000);
        Console.WriteLine("Async method completed.");
    }
    static async Task Main(string[] args)
    {
       
        var cancellationTokenSource = new CancellationTokenSource();
        var token = cancellationTokenSource.Token;

        Task primeTask = Task.Run(() => FindPrimes(1000000, token), token);
        Console.WriteLine($"Task ID: {primeTask.Id}");
        Stopwatch stopwatch = Stopwatch.StartNew();

        while (!primeTask.IsCompleted)
        {
            Console.WriteLine($"Task Status: {primeTask.Status}");
            await Task.Delay(100);
        }

        stopwatch.Stop();
        Console.WriteLine($"Task Status: {primeTask.Status}");
        Console.WriteLine($"Elapsed Time: {stopwatch.ElapsedMilliseconds} ms");

       
        cancellationTokenSource.Cancel(); 

      
        var results = await Task.WhenAll(
            Task.Run(() => CalculateValue(1)),
            Task.Run(() => CalculateValue(2)),
            Task.Run(() => CalculateValue(3))
        );

        var finalResult = CombineResults(results);
        Console.WriteLine($"Final Result: {finalResult}");

        Task continuationTask = primeTask.ContinueWith(t =>
        {
            Console.WriteLine("Continuation task started after the first task completion.");
        });

        await continuationTask;


        Task<int>[] tasks = new[]
        {
    Task.Run(() => CalculateValue(1)),
    Task.Run(() => CalculateValue(2)),
    Task.Run(() => CalculateValue(3))
};

        var results2 = new int[tasks.Length];
        for (int i = 0; i < tasks.Length; i++)
        {
            results2[i] = tasks[i].GetAwaiter().GetResult(); 
        }

        var finalResult2 = CombineResults(results);
        Console.WriteLine($"Final Result from GetAwaiter: {finalResult2}");

        Parallel.For(0, 1000000, i =>
        {
            var value = i * 2; 
        });

        Parallel.Invoke(
            () => Console.WriteLine("Block 1 executed."),
            () => Console.WriteLine("Block 2 executed."),
            () => Console.WriteLine("Block 3 executed.")
        );

        var warehouse = new BlockingCollection<string>();
        var suppliers = Enumerable.Range(1, 5).Select(i => Task.Run(() => SupplyGoods(warehouse, i))).ToArray();
        var customers = Enumerable.Range(1, 10).Select(i => Task.Run(() => BuyGoods(warehouse, i))).ToArray();

        await Task.WhenAll(suppliers);
        await Task.WhenAll(customers);

        await SomeAsyncMethod();
    }

}