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

List<string> list = new List<string>() {"a","b","c", "d"};
Parallel.ForEach(list, DoWork);

Log("finish");

void DoWork(string i)
{
    Thread.Sleep(100);
    Log($"DoWork {i}");
    //Thread.Sleep(2000);
}




void Log(string message)
{
    Console.WriteLine($"{(DateTime.Now - now).TotalMilliseconds})\t [{Environment.CurrentManagedThreadId}] | {message}");
}

