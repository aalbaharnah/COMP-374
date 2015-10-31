using System;
using System.Collections.Generic;
using System.Threading;

namespace Homework3 {
    internal class ProgressMonitor {
        private readonly List<long> _results;
        public long TotalCount = 0;
        static object _lock = new object();

        public ProgressMonitor(List<long> results) {
            _results = results;
        }

        public void Run() {
            while (true) {
                Monitor.Enter(_lock);
                Thread.Sleep(100); // wait for 1/10th of a second

                while (_results.Count == 0)
                {
                    Monitor.Wait(_lock);
                }
                var currentcount = _results.Count;
                TotalCount += currentcount;
                Monitor.Pulse(_lock);
                Monitor.Exit(_lock);
                _results.Clear(); // clear out the current primes to save some memory
                
                if (currentcount > 0) {
                    Console.WriteLine("{0} primes found so far", TotalCount);
                }
            }
        }
    }
}