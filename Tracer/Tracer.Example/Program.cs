using System.Reflection;
using Tracer.Core.Interfaces;
using Tracer.Core.Structs;
using Tracer.Serialization.Abstractions;

namespace Tracer.Example
{
    public class Foo
    {
        private Bar _bar;
        private ITracer _tracer;

        internal Foo(ITracer tracer)
        {
            _tracer = tracer;
            _bar = new Bar(_tracer);
        }

        public void MyMethod()
        {
            _tracer.StartTrace();
            _bar.InnerMethod();
            _bar.InnerMethod();
            _bar.InnerMethod();

            Thread.Sleep(200);
            _tracer.StopTrace();
        }
    }

    public class Bar
    {
        private ITracer _tracer;

        internal Bar(ITracer tracer)
        {
            _tracer = tracer;
        }

        public void InnerMethod()
        {
            _tracer.StartTrace();
            Thread.Sleep(100);
            _tracer.StopTrace();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            ITracer tracer = new Tracer.Core.Tracer();
            var s = new Foo(tracer);

            s.MyMethod();

            var res = tracer.GetTraceResult();

            XmlExample(res);
        }

        static void TextExampl(TraceResult result)
        {
            using var to = File.Open("dump.txt", FileMode.Create);

            var obj = new TraceResultSerializer();
            obj.Serialize(result, to);

            to.Close();
        }

        static void JsonExample(TraceResult result)
        {
            using var to = File.Open("dump.json", FileMode.Create);

            Assembly a = Assembly.LoadFrom(@"D:\Учеба\5 сем\СПП\lab1\Tracer.Serialization\Tracer.Serialization.Json\bin\Debug\net6.0\Tracer.Serialization.Json.dll");

            Type myType = a.GetType("Tracer.Serialization.Json.TraceResultSerializerJson", true);
            var obj = (ITraceResultSerializer)Activator.CreateInstance(myType);
            obj.Serialize(result, to);

            to.Close();
        }

        static void YamlExample(TraceResult result)
        {
            using var to = File.Open("dump.yaml", FileMode.Create);

            Assembly a = Assembly.LoadFrom(@"D:\Учеба\5 сем\СПП\lab1\Tracer.Serialization\Tracer.Serialization.Yaml\bin\Debug\net6.0\Tracer.Serialization.Yaml.dll");

            Type myType = a.GetType("Tracer.Serialization.Yaml.TraceResultSerializerYaml", true);
            var obj = (ITraceResultSerializer)Activator.CreateInstance(myType);
            obj.Serialize(result, to);

            to.Close();
        }

        static void XmlExample(TraceResult result)
        {
            using var to = File.Open("dump.xml", FileMode.Create);

            Assembly a = Assembly.LoadFrom(@"D:\Учеба\5 сем\СПП\lab1\Tracer.Serialization\Tracer.Serialization.Xml\bin\Debug\net6.0\Tracer.Serialization.Xml.dll");

            Type myType = a.GetType("Tracer.Serialization.Xml.TraceResultSerializerXml", true);
            var obj = (ITraceResultSerializer)Activator.CreateInstance(myType);
            obj.Serialize(result, to);

            to.Close();
        }
    }
}