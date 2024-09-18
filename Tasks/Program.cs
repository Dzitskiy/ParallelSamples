var now = DateTime.Now;
Console.Clear();

Log("start");

var d = Task.Delay(1000);


var t = DoWork();

await Task.WhenAll(d,t);
//await Task.WhenAny(d, t);

Log("finish");

async Task DoWork()
{ 
    //await Task.Delay(2000);
    Log("DoWork");
}

void Log(string message)
{
    Console.WriteLine($"{(DateTime.Now - now).TotalMilliseconds})\t [{Environment.CurrentManagedThreadId}] | {message}");
}

