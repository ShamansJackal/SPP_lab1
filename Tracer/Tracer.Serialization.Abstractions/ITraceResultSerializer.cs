using Tracer.Core.Structs;

namespace Tracer.Serialization.Abstractions
{
    public interface ITraceResultSerializer
    {
        void Serialize(TraceResult traceResult, Stream to);
    }

    public class TraceResultSerializer : ITraceResultSerializer
    {
        public void Serialize(TraceResult traceResult, Stream to)
        {
            using StreamWriter writer = new(to);

            foreach (var theread in traceResult.Threads)
            {
                writer.WriteLine($"Thread id {theread.Id}");
                foreach (var method in theread.InnerMethods)
                {
                    writer.WriteLine($" {method.Class}.{method.Name} - {method.Duration}");
                    foreach(var subMethod in method.InnerMethods)
                    {
                        WriteToFile(subMethod, "  ", writer);
                    }
                }
            }

            writer.Flush();
        }

        private void WriteToFile(TracedMethod method, string offset, StreamWriter to)
        {
            to.WriteLine($"{offset}{method.Class}.{method.Name} - {method.Duration}");
            foreach (var subMethod in method.InnerMethods)
            {
                WriteToFile(subMethod, offset+" ", to);
            }
        }
    }
}