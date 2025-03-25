namespace ParallelSamples
{
    internal interface IExecutor
    {
        /// <summary>
        /// Запуск тестового примера 
        /// </summary>
        Task DemoAsync();

        /// <summary>
        /// Метод вывода сообщения в консоль с указанием времени выполнения и идентификатора потока
        /// </summary>
        /// <param name="startDateTime">Начало отсчета</param>
        /// <param name="message">Сообщение</param>
        
        /// <summary>
        /// Расчет суммы простых чисел в заданном диапазоне
        /// </summary>
        /// <param name="n">Начало диапазона</param>
        /// <param name="m">Конец диапазона</param>
        /// <returns>Сумма простых чисел</returns>
        long CalculateSum(int n, int m);

        /// <summary>
        /// Метод проверки - является ли число простым
        /// </summary>
        /// <param name="number">Число</param>
        /// <returns>Признак того, что число является простым</returns>
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
        static void Log(DateTime startDateTime, string message)
        {
            Console.WriteLine($"{(DateTime.Now - startDateTime).TotalMilliseconds})\t [{Environment.CurrentManagedThreadId}] | {message}");
        }

    }
}