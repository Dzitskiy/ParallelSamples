var now = DateTime.Now;
Console.Clear();

Console.WriteLine("start");

int d = 5;
var t = new Thread(() =>
{
    int a = 1;
    d = 10 + a;
});

Log($"status {t.ThreadState}");

t.Start();

Thread.Sleep(1000);
t.Join();

Log($"status {t.ThreadState}");

//Log($"status {Thread.GetDomain().}");
//Log($"status {t.}");
//Log($"status {t.ThreadState}");

Log($"status {t.ThreadState}");



Log(d.ToString()); // d = ?


Console.WriteLine("finish");

void Log(string message) 
{
    Console.WriteLine($"{(DateTime.Now - now).TotalMilliseconds})\t [{Environment.CurrentManagedThreadId}] | {message}");
}

