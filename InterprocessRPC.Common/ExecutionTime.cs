using System;
using System.Diagnostics;

namespace InterprocessRPC.Common
{
    public class ExecutionTime : IDisposable
    {
        private readonly Stopwatch stopwatch;

        public ExecutionTime()
        {
            stopwatch = Stopwatch.StartNew();
        }

        public void Dispose()
        {
            stopwatch.Stop();
            Debug.WriteLine($"Execution time: {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}