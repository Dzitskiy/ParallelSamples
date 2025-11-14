using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using ParallelSamples;
using System.Diagnostics;

namespace ParallelSamples
{
    public class Program
    {
        // Параллельное вычисление суммы простых чисел в заданном диапазоне
        // Натуральное число, большее 1 , называется простым, если оно ни на что не делится, кроме себя и 1. 
        private static void Main(string[] args)
        {
            BenchmarkRunner.Run<ExecutorsBenchmark>();

            Console.ReadKey();


            #region IExecutor

            IExecutor executor = new ThreadExecutor();

            executor.DemoAsync();

            var executors = new IExecutor[]
            {
            new ThreadExecutor(),     // Реализация с Threads
            new TaskExecutor(),       // Реализация с Tasks
            new ParallelExecutor(),   // Реализация с Parallel
            new PlinqExecutor(),      // Реализация с PLINQ
            new СoncurrentExecutor()  // Реализация с Concurrent Collections
            };

            #endregion

            //Диапазон чисел
            const int N = 2;
            //const int M = 10_000_000;

            int[] sizes = { //10_000,
                100_000, 1_000_000, 10_000_000,
                //100_000_000
                };


            foreach (int size in sizes)
            {
                Console.WriteLine($"Size: {/*M*/size}");

                var sw = Stopwatch.StartNew();
                long sum = CalculateSequential(N, /*M*/size);
                sw.Stop();

                var sequentialTime = sw.ElapsedMilliseconds;
                Console.WriteLine($"SequentialSum: {sum}, ExecutionTime: {sequentialTime}");

                #region parallelSum
                /*
                sw.Restart();
                long parallelSum = executor.CalculateSum(N, M);
                sw.Stop();

                var parallelTime = sw.ElapsedMilliseconds;
                Console.WriteLine($"ParallelSum: {parallelSum}, ExecutionTime: {parallelTime}");
                */
                #endregion

                //}

                #region all executors
                /*
                foreach (var executor in executors)
                {
                    Console.WriteLine(executor.GetType().Name);
                    foreach (int m in sizes)
                    {
                        Console.WriteLine($"Size: {m}");
                        sw.Restart();
                        long parallelSum = executor.CalculateSum(N, m);
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
    }
}