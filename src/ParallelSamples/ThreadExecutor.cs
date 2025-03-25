using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelSamples
{    
    /// <summary>
    /// Реализация с использованием классических потоков (System.Threading.Thread) 
    /// </summary>
    public class ThreadExecutor : IExecutor
    {
        public async Task DemoAsync()
        {
            var now = DateTime.Now;
            Console.Clear();

            Console.WriteLine("start");

            int d = 5;
            var t = new Thread(() =>
            {
                int a = 1;
                d = 10 + a;
            });

            IExecutor.Log(now, $"status {t.ThreadState}");

            t.Start();

            Thread.Sleep(1000);
            t.Join();

            IExecutor.Log(now, $"status {t.ThreadState}");

            //Log($"status {Thread.GetDomain().}");
            //Log($"status {t.}");
            //Log($"status {t.ThreadState}");

            IExecutor.Log(now, $"status {t.ThreadState}");


            IExecutor.Log(now, d.ToString()); // d = ?


            Console.WriteLine("finish");

            //void Log(string message)
            //{
            //    Console.WriteLine($"{(DateTime.Now - now).TotalMilliseconds})\t [{Environment.CurrentManagedThreadId}] | {message}");
            //}

        }

        public long CalculateSum(int n, int m)
        {
            //Разделим диапазон[N, M] на K непересекающихся поддиапазонов(K = количество логических ядер CPU).
            int cores = Environment.ProcessorCount;
            long chunkSize = (m - n + 1L) / cores;
            var threads = new List<Thread>(cores);
            long total = 0;
            object lockObj = new();

            //Для каждого поддиапазона создадим отдельный поток, который вычислит сумму простых чисел в своей части.
            for (int i = 0; i < cores; i++)
            {
                int start = (int)(n + i * chunkSize);
                int end = (i == cores - 1) ? m : (int)(start + chunkSize - 1);

                Thread t = new(() =>
                {
                    long localSum = 0;
                    for (int num = start; num <= end; num++)
                        if (IExecutor.IsPrime(num))
                            localSum += num;

                    lock (lockObj) { total += localSum; }
                });

                threads.Add(t);
                t.Start();
            }

            //Синхронизируем потоки и объединим результаты.
            foreach (Thread t in threads) t.Join();
            return total;
        }
    }
}
