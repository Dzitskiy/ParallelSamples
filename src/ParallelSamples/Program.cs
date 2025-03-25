using ParallelSamples;
using System.Diagnostics;

internal class Program
{
    // Параллельное вычисление суммы простых чисел в заданном диапазоне
    // Натуральное число, большее 1 , называется простым, если оно ни на что не делится, кроме себя и 1. 
    private static void Main(string[] args)
    { 
        //Реализация с Threads
        //new ThreadExecutor().DemoAsync();
        //Реализация с Tasks
        //new TaskExecutor().DemoAsync();
        //Реализация с Parallel
        //new ParallelExecutor().DemoAsync();
        //Реализация с PLINQ
        //new PlinqExecutor().DemoAsync();

        IExecutor executor = new ThreadExecutor();

        //var executors = new IExecutor[]
        //{
        //    new ThreadExecutor(),
        //    new TaskExecutor(),
        //    new ParallelExecutor(),
        //    new PlinqExecutor()
        //};             

        //Диапазон чисел
        const int N = 2;
        const int M = 10_000_000;

        int[] sizes = { 10_000, 100_000, 1_000_000, 10_000_000, 100_000_000 };


        //foreach (int size in sizes)
        //{
        Console.WriteLine($"Size: {N}");

        var sw = Stopwatch.StartNew();
        long result = CalculateSequential(N, M);
        sw.Stop();

        var sequentialTime = sw.ElapsedMilliseconds;
        Console.WriteLine($"SequentialSum: {sequentialTime}, ExecutionTime: {sequentialTime}");

        #region parallelSum
        /*
        sw.Restart();
        long parallelSum = executor.CalculateSum(N, M);
        sw.Stop();

        var parallelTime = sw.ElapsedMilliseconds;
        Console.WriteLine($"ParallelSum: {sequentialTime}, ExecutionTime: {parallelTime}");
        */
        #endregion 

        //}

        #region executors
        /*
        foreach (var executor in executors)
        {
            Console.WriteLine(executor.GetType().Name);
            foreach (int size in sizes)
            {
                Console.WriteLine($"Size: {size}");
                sw.Restart();
                long parallelSum = executor.CalculateSum(N, M);
                sw.Stop();
                var parallelTime = sw.ElapsedMilliseconds;
                Console.WriteLine($"ParallelSum: {parallelSum}, ExecutionTime: {parallelTime}");
            }
        }
        */
        #endregion
    }

    #region Последовательная реализация

    /// Метод вычисления суммы простых чисел в заданном диапазоне
    static long CalculateSequential(int n, int m)
    {
        long sum = 0;
        for (int num = n; num <= m; num++)
        {
            if (IsPrime(num))
                sum += num;
        }
        return sum;
    }

    // Метод проверки - является ли число простым
    static bool IsPrime(int number)
    {
        if (number < 2) return false;
        if (number == 2) return true;
        if (number % 2 == 0) return false;

        for (int i = 3; i <= Math.Sqrt(number); i += 2)
            if (number % i == 0)
                return false;

        return true;
    }

    #endregion

}