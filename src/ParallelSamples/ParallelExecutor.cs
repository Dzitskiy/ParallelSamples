using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelSamples
{
    /// <summary>
    /// Реализация с использованием библиотеки Parallel
    /// </summary>
    internal class ParallelExecutor : IExecutor
    {
        public async Task DemoAsync()
        {
            var now = DateTime.Now;
            Console.Clear();
            Console.WriteLine("start");

            //Parallel.Invoke(DoWork, DoWork);

            //List<Action> actions = new List<Action>();
            //for (int i = 0; i < 24; i++)
            //{
            //    actions.Add(DoWork);
            //}

            //Parallel.Invoke(
            //    new ParallelOptions() 
            //    { 
            //        MaxDegreeOfParallelism = 4
            //    },
            //    actions.ToArray());

            //Parallel.For(0, 24, DoWork);

            List<string> list = new List<string>() { "a", "b", "c", "d" };
            Parallel.ForEach(list, DoWork);

            Console.WriteLine("finish");

            void DoWork(string i)
            {
                Thread.Sleep(100);
                IExecutor.Log(now, $"DoWork {i}");
                //Thread.Sleep(2000);
            }


        }

        public long CalculateSum(int n, int m)
        {
            //Для параллельного обхода диапазона используем Parallel.For или Parallel.ForEach.

            long totalSum = 0;

            // Используем Parallel.For с локальным хранилищем для потоков
            Parallel.For(
                n,          // Начальное значение
                m + 1 + 1,  // Конечное значение (включительно)
                () => 0L,   // Инициализация локальной суммы для потока

           // Основная логика 
           (num, state, localSum) =>
           {
               return IExecutor.IsPrime(num)
                   ? localSum + num     // Добавляем простое число к локальной сумме
                   : localSum;          // Пропускаем не простые числа
           },

           // Объединяем локальных сумм
           localSum => Interlocked.Add(ref totalSum, localSum));

            return totalSum;
        }
    }
}