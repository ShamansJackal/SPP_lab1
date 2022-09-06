using Tracer.Core.Structs;
using Tracer.Serialization.Abstractions;
using YamlDotNet.Serialization;

namespace Tracer.Serialization.Yaml
{
    public class TraceResultSerializerYaml : ITraceResultSerializer
    {
        public void Serialize(TraceResult traceResult, Stream to)
        {
            using StreamWriter writer = new(to);
            TextWriter yamlWriter = writer;
            Serializer ser = new();
            ser.Serialize(yamlWriter, traceResult);
            yamlWriter.Flush();
        }
    }
}