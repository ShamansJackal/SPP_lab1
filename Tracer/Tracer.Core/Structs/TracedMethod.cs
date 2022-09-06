using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer.Core.Structs
{
    public class TracedMethod
    {
        public List<TracedMethod> InnerMethods = new();
        public string Name { get; set; }
        public string Class { get; set; }

        private Stopwatch _sw;
        public long Duration => _duration;

        private long _duration;

        public void StopTracing()
        {
            _sw.Stop();
            _duration = _sw.ElapsedMilliseconds;
        }

        public void StartTracing()
        {
            _sw = new Stopwatch();
            _sw.Start();
        }
    }
}
