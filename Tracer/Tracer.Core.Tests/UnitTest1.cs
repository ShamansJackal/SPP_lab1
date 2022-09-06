using Tracer.Core.Interfaces;

namespace Tracer.Core.Tests
{
    public class UnitTest1
    {
        private ITracer _tracer = new Tracer();
        private int waitTime = 100;

        private void AnyMethod()
        {
            _tracer.StartTrace();
            Thread.Sleep(waitTime);
            _tracer.StopTrace();
        }

        private void AnyMethod2()
        {
            _tracer.StartTrace();
            AnyMethod3();
            AnyMethod3();
            _tracer.StopTrace();
        }

        private void AnyMethod3()
        {
            _tracer.StartTrace();
            Thread.Sleep(waitTime / 4);
            _tracer.StopTrace();
        }

        [Fact]
        public void TimeTest()
        {
            _tracer.StartTrace();
            Thread.Sleep(waitTime);
            _tracer.StopTrace();
            long actualTime = _tracer.GetTraceResult().Threads.Single(x => x.Id == Environment.CurrentManagedThreadId).TotalTime;
            Assert.True(actualTime >= waitTime);
        }

        [Fact]
        public void NameTest()
        {
            _tracer.StartTrace();
            Thread.Sleep(waitTime);
            _tracer.StopTrace();
            string actualClassName = _tracer.GetTraceResult().Threads.Single(x => x.Id == Environment.CurrentManagedThreadId).InnerMethods[0].Class;
            string actualMethodName = _tracer.GetTraceResult().Threads.Single(x => x.Id == Environment.CurrentManagedThreadId).InnerMethods[0].Name;
            Assert.Equal(nameof(NameTest), actualMethodName);
            Assert.Equal(nameof(UnitTest1), actualClassName);
        }

        [Fact]
        public void ThreadCountTest()
        {
            _tracer.StartTrace();

            List<Thread> threads = new();
            for (int i = 0; i < 4; ++i)
            {
                var thread = new Thread(AnyMethod);
                threads.Add(thread);
                thread.Start();
            }

            foreach (Thread thread in threads) thread.Join();

            _tracer.StopTrace();

            int actualCountOfThreads = _tracer.GetTraceResult().Threads.Count;
            Assert.Equal(5, actualCountOfThreads);
        }

        [Fact]
        public void MethodCountTest()
        {
            _tracer.StartTrace();

            AnyMethod2();
            AnyMethod2();
            AnyMethod2();

            _tracer.StopTrace();

            int actualCountOfMethods = _tracer.GetTraceResult().Threads.Single(x => x.Id == Environment.CurrentManagedThreadId).InnerMethods[0].InnerMethods.Count;
            Assert.Equal(3, actualCountOfMethods);
        }
    }
}