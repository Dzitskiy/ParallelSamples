using BenchmarkDotNet.Attributes;

namespace ParallelSamples
{
    public class Benchmarks
    {
        const int N = 2;
        [Params(10_000)]  // Size of the array
        public int M;

        [Benchmark]
        public void ThreadExecution()
        {
            new ThreadExecutor().CalculateSum(N, M);
        }

        [Benchmark]
        public void TaskExecution()
        {
            new ThreadExecutor().CalculateSum(N, M);
        }

        [Benchmark]
        public void ParallelExecution()
        {
            new ParallelExecutor().CalculateSum(N, M);
        }

        [Benchmark]
        public void PlinqExecution()
        {
            new PlinqExecutor().CalculateSum(N, M);
        }
    }
}
