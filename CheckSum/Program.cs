using System.Diagnostics;

int[] sizes = { 10000, 100000, 1000000, 10000000, 20000000, 100000000 };

foreach (int size in sizes)
{
    Console.WriteLine($"size: {size}");

    var arr = Enumerable.Range(0, size).ToArray();

    var sw = Stopwatch.StartNew();
    long sequentialSum = SequentialSum(arr);
    sw.Stop();
    long sequentialTime = sw.ElapsedMilliseconds;

    sw.Restart();
    long parallelSum = ParallelSum(arr);
    sw.Stop();
    var parallelTime = sw.ElapsedMilliseconds;

    Console.WriteLine($"sequentialSum: {sequentialTime}, parallelSum: {parallelTime}");
}

long SequentialSum(int[] arr)
{
    long sum = 0;
    foreach (var i in arr)
        sum += i;
    return sum;
}

long ParallelSum(int[] arr)
{
    return arr.AsParallel().Sum(x => (long)x);
}