using System.Diagnostics;
using Tracer.Core.Interfaces;
using Tracer.Core.Structs;

namespace Tracer.Core
{
    public class Tracer : ITracer
    {
        public Tracer()
        {
            _result = new TraceResult();
        }

        private TraceResult _result;

        public TraceResult GetTraceResult() => _result;

        public void StartTrace()
        {
            StackTrace stackTrace = new(1);
            var method = stackTrace.GetFrame(0).GetMethod();
            TracedMethod tracedMethod = new() { Name = method.Name, Class = method.DeclaringType.Name};

            _result.AddMethodToThread(tracedMethod, Environment.CurrentManagedThreadId);
            tracedMethod.StartTracing();
        }

        public void StopTrace()
        {
            _result.PopMethodFromThreadStack(Environment.CurrentManagedThreadId);
        }
    }
}