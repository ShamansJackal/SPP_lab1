using System.Xml.Serialization;
using Tracer.Core.Structs;
using Tracer.Serialization.Abstractions;

namespace Tracer.Serialization.Xml
{
    public class TraceResultSerializerXml : ITraceResultSerializer
    {
        public void Serialize(TraceResult traceResult, Stream to)
        {
            using StreamWriter writer = new(to);
            XmlSerializer ser = new XmlSerializer(typeof(TraceResult));
            ser.Serialize(writer, traceResult);
            writer.Flush();
        }
    }
}
