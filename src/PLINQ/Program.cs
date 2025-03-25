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


"abcdefg1234567890"
    .AsParallel()
    .AsOrdered()
    .Select(c => char.ToUpper(c))
.ForAll(Console.WriteLine);



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

