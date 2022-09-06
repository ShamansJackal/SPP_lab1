using System.Xml.Serialization;

namespace Tracer.Core.Structs
{
    public class TraceResult
    {
        public TraceResult()
        {
            _threads = new();
        }


        public List<TracedThread> Threads => _threads;
        private List<TracedThread> _threads;

        public void PopMethodFromThreadStack(int threadId) => _threads.Single(x => x.Id == threadId).PopMethod();

        public void AddMethodToThread(TracedMethod method, int threadId)
        {
            var thread = _threads.SingleOrDefault(x => x.Id == threadId);

            if(thread == null)
            {
                thread = new TracedThread() { Id = threadId };
                _threads.Add(thread);
            }

            thread.AddMethod(method);
        }

    }
}
