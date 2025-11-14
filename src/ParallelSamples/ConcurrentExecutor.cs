using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelSamples
{
    /// <summary>
    /// Реализация с использованием библиотеки Сoncurrent
    /// </summary>
    public class СoncurrentExecutor : IExecutor
    {
        public async Task DemoAsync()
        {
            // ConcurrentQueue - потокобезопасная очередь
            var queue = new ConcurrentQueue<int>();

            // ConcurrentDictionary - потокобезопасный словарь
            var dictionary = new ConcurrentDictionary<int, string>();

            // ConcurrentBag - потокобезопасная неупорядоченная коллекция
            var bag = new ConcurrentBag<int>();

            // Запускаем несколько задач для работы с коллекциями
            Task producer = Task.Run(() => ProduceData(queue, dictionary, bag));
            Task consumer = Task.Run(() => ConsumeData(queue, dictionary, bag));

            Task.WaitAll(producer, consumer);

            Console.WriteLine("Все задачи завершены.");
        }

        /// <summary>
        /// Метод для добавления данных в коллекции
        /// </summary>
        /// <param name="queue"></param>
        /// <param name="dictionary"></param>
        /// <param name="bag"></param>
        static void ProduceData(ConcurrentQueue<int> queue, ConcurrentDictionary<int, string> dictionary, ConcurrentBag<int> bag)
        {
            for (int i = 0; i < 10; i++)
            {
                // Добавляем данные в очередь
                queue.Enqueue(i);
                Console.WriteLine($"Добавлено в очередь: {i}");

                // Добавляем данные в словарь
                dictionary.TryAdd(i, $"Value_{i}");
                Console.WriteLine($"Добавлено в словарь: {i} -> Value_{i}");

                // Добавляем данные в bag
                bag.Add(i);
                Console.WriteLine($"Добавлено в bag: {i}");

                // Имитируем задержку
                Task.Delay(1000).Wait();
            }
        }

        /// <summary>
        /// Метод для извлечения данных из коллекций
        /// </summary>
        /// <param name="queue"></param>
        /// <param name="dictionary"></param>
        /// <param name="bag"></param>
        static void ConsumeData(ConcurrentQueue<int> queue, ConcurrentDictionary<int, string> dictionary, ConcurrentBag<int> bag)
        {
            for (int i = 0; i < 10; i++)
            {
                // Извлекаем данные из очереди
                if (queue.TryDequeue(out int queueItem))
                {
                    Console.WriteLine($"Извлечено из очереди: {queueItem}");
                }

                // Получаем данные из словаря
                if (dictionary.TryGetValue(i, out string dictValue))
                {
                    Console.WriteLine($"Извлечено из словаря: {i} -> {dictValue}");
                }

                // Извлекаем данные из bag
                if (bag.TryTake(out int bagItem))
                {
                    Console.WriteLine($"Извлечено из bag: {bagItem}");
                }

                // Имитируем задержку
                Task.Delay(1000).Wait();
            }
        }


        public long CalculateSum(int n, int m)
        {
            return 
                //CalculateWithConcurrentBag(n,  m);
                // CalculateWithConcurrentQueue(n,  m);
                 CalculateWithConcurrentDictionary(n,  m);
        }

        #region 

        /// <summary>
        /// Решение с ConcurrentBag (Простейшая реализация)
        /// </summary>
        /// <param name="n"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        static long CalculateWithConcurrentBag(int n, int m)
        {
            var primes = new ConcurrentBag<long>();

            Parallel.For(n, m + 1, num =>
            {
                if (IExecutor.IsPrime(num))
                {
                    primes.Add(num);
                }
            });

            return primes.Sum();
        }

        /// <summary>
        /// Решение с ConcurrentQueue (Подходит для пакетной обработки диапазонов)
        /// </summary>
        /// <param name="n"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        static long CalculateWithConcurrentQueue(int n, int m)
        {
            var queue = new ConcurrentQueue<long>();
            long total = 0;

            Parallel.ForEach(Partitioner.Create(n, m + 1), range =>
            {
                long localSum = 0;
                for (int num = range.Item1; num < range.Item2; num++)
                {
                    if (IExecutor.IsPrime(num)) localSum += num;
                }
                queue.Enqueue(localSum);
            });

            return queue.Sum(x => x);
        }

        /// <summary>
        /// Решение с ConcurrentDictionary (Позволяет хранить дополнительные метаданные)
        /// </summary>
        /// <param name="n"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        static long CalculateWithConcurrentDictionary(int n, int m)
        {
            var dict = new ConcurrentDictionary<int, bool>();
            long sum = 0;

            Parallel.For(n, m + 1, num =>
            {
                dict.TryAdd(num, IExecutor.IsPrime(num));
            });

            return dict.Where(kvp => kvp.Value).Sum(kvp => (long)kvp.Key);
        }

        #endregion


    }
}
