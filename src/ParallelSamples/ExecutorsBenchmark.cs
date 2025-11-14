using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using ParallelSamples;

namespace ParallelSamples
{
    [MemoryDiagnoser]
    [SimpleJob(RuntimeMoniker.Net80)]
    public class ExecutorsBenchmark
    {
        // Range start (kept small); primary variation is in M
        [Params(2)]
        public int N { get; set; }

        // Adjust these sizes to match the workload you want to measure
        [Params(/*1_000_000, 5_000_000,*/ 10_000_000)]
        public int M { get; set; }

        private ThreadExecutor _threadExecutor;
        private TaskExecutor _taskExecutor;
        private ParallelExecutor _parallelExecutor;
        private PlinqExecutor _plinqExecutor;

        [GlobalSetup]
        public void Setup()
        {
            _threadExecutor = new ThreadExecutor();
            _taskExecutor = new TaskExecutor();
            _parallelExecutor = new ParallelExecutor();
            _plinqExecutor = new PlinqExecutor();
        }

        [Benchmark(Baseline = true, Description = "Thread")]
        public long ThreadExecutor_Benchmark() => _threadExecutor.CalculateSum(N, M);

        [Benchmark(Description = "Task")]
        public long TaskExecutor_Benchmark() => _taskExecutor.CalculateSum(N, M);

        [Benchmark(Description = "Parallel")]
        public long ParallelExecutor_Benchmark() => _parallelExecutor.CalculateSum(N, M);

        [Benchmark(Description = "PLINQ")]
        public long PlinqExecutor_Benchmark() => _plinq_executor_check();

        // Calling PLINQ via the provided executor
        private long _plinq_executor_check() => _plinqExecutor.CalculateSum(N, M);
    }
}