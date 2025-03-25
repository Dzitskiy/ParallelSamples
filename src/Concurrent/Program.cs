using System.Collections.Concurrent;

class Program
{
    static void Main()
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
}