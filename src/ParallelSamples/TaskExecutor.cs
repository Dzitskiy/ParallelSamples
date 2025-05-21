using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelSamples
{
    /// <summary>
    /// Реализация с использованием задач (System.Threading.Tasks.Task).
    /// </summary>
    internal class TaskExecutor : IExecutor
    {
        public async Task DemoAsync()
        {
            var now = DateTime.Now;
            Console.Clear();

            Console.WriteLine("start");

            var d = Task.Delay(1000);


            var t = DoWork();

            await Task.WhenAll(d, t);
            //await Task.WhenAny(d, t);

            Console.WriteLine("finish");

            async Task DoWork()
            {
                await Task.Delay(2000);
                Console.WriteLine("DoWork");
            }

        }
        public long CalculateSum(int n, int m)
        {
            //Применим так же ручное разделение диапазона на равные части по числу ядер процессора

            int tasksCount = Environment.ProcessorCount;
            long chunkSize = (m - n + 1L) / tasksCount;
            var tasks = new Task<long>[tasksCount];

            for (int i = 0; i < tasksCount; i++)
            {
                int start = (int)(n + i * chunkSize);
                int end = (i == tasksCount - 1) ? m : (int)(start + chunkSize - 1);

                //Каждая задача возвращает свою локальную сумму
                tasks[i] = Task.Factory.StartNew((obj) =>
                {
                    var (s, e) = ((int, int))obj!;
                    long sum = 0;
                    for (int num = s; num <= e; num++)
                        if (IExecutor.IsPrime(num)) sum += num;
                    return sum;
                }, (start, end));
            }

            //Ожидание завершения всех задач через Task.WaitAll
            Task.WaitAll(tasks);

            //Агрегация результатов через LINQ 
            return tasks.Sum(t => t.Result);
        }
    }
}
