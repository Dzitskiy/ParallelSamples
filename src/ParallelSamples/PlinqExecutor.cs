using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelSamples
{
    /// <summary>
    /// Реализация с применением Parallel LINQ (PLINQ).
    /// </summary>
    public class PlinqExecutor : IExecutor
    {
        public async Task DemoAsync()
        {
            var now = DateTime.Now;
            Console.Clear();
            Console.WriteLine("start");

            var t = "abcdefg1234567890"
                .AsParallel()
                .AsOrdered()
                .Select(c => char.ToUpper(c))
                .ToArray();
            foreach (var item in t)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine(string.Join(", ", t));

            "abcdefg1234567890"
                .AsParallel()
                .AsUnordered()
                .Select(c => char.ToUpper(c))
            .ForAll(Console.WriteLine);

            Console.WriteLine("finish");

            void DoWork(string i)
            {
                Thread.Sleep(100);
                Console.WriteLine($"DoWork {i}");
                //Thread.Sleep(2000);
            }

        }

        public long CalculateSum(int n, int m)
        {
            // C PLINQ можно использовать  методы AsParallel(), WithDegreeOfParallelism() и Aggregate() для распараллеливания LINQ-запроса.

            Console.WriteLine($"ProcessorCount: {Environment.ProcessorCount}");

            return Enumerable.Range(n, m - n + 1)
                .AsParallel()
                .WithDegreeOfParallelism(Environment.ProcessorCount)
                .Where(IExecutor.IsPrime)
                .Sum(x => (long)x);
        }
    }
}