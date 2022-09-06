using Newtonsoft.Json;
using Tracer.Core.Structs;
using Tracer.Serialization.Abstractions;

namespace Tracer.Serialization.Json
{
    public class TraceResultSerializerJson : ITraceResultSerializer
    {
        public void Serialize(TraceResult traceResult, Stream to)
        {
            using StreamWriter writer = new(to);
            using JsonTextWriter jsonWriter = new(writer);
            JsonSerializer ser = new();
            ser.Serialize(jsonWriter, traceResult);
            jsonWriter.Flush();
        }
    }
}