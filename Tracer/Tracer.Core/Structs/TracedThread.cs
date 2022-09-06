using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Tracer.Core.Structs
{
    public class TracedThread
    {

        public int Id { get; set; }

        public List<TracedMethod> InnerMethods => _rootMethods;
        public long TotalTime => _rootMethods.Sum(x => x.Duration);


        private Stack<TracedMethod> _methodStack = new();
        private List<TracedMethod> _rootMethods = new();

        private bool InRootOfThread => _methodStack.Count == 0;

        public void AddMethod(TracedMethod tracedMethod)
        {
            if (InRootOfThread)
            {
                _rootMethods.Add(tracedMethod);
            }
            else
            {
                _methodStack.Peek().InnerMethods.Add(tracedMethod);
            }

            _methodStack.Push(tracedMethod);
        }

        public void PopMethod()
        {
            _methodStack.Pop().StopTracing();
        }
    }
}
