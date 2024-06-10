using Microsoft.Extensions.Configuration;
using Serialization.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serialization.Implementations
{
    public class SpeedMeter<T>(IOutput output, IConfiguration cfg) : ISpeedMeter<T>
    {
        private readonly IOutput _output = output;
        private int _loopCount = cfg.GetValue<int>("LoopCount");
        public void CheckSerializationSpeed(Func<T, bool, string> func, T someType, bool needOutput = false)
        {
            var sw = new Stopwatch();
            sw.Start();
            var result = func.Invoke(someType, needOutput);
            sw.Stop();
            var timeSpan = sw.ElapsedMilliseconds;
            _output.Write($"Object of type: ");
            _output.Write($"{typeof(T)}", ConsoleColor.Green);
            _output.Write($" serialized to:");
            _output.Write($" {result}\r\n", ConsoleColor.Blue);
            _output.Write($"{_loopCount:N0} ", ConsoleColor.DarkBlue);
            _output.Write("times in ");
            _output.Write($"{timeSpan} ", ConsoleColor.Red);
            _output.Write($"milliseconds\r\n");
            _output.Write($"need console output: ");
            _output.Write($"{needOutput}", ConsoleColor.DarkBlue);
            _output.Write("\r\n");
        }

        public void CheckDeserializationSpeed(Func<string, T> func, string serialized)
        {
            var sw = new Stopwatch();
            sw.Start();
            var result = func.Invoke(serialized);
            sw.Stop();
            var timeSpan = sw.ElapsedMilliseconds;
            _output.Write("String");
            _output.Write($"{serialized} ", ConsoleColor.Green);
            _output.Write($"deserialized to:");
            _output.Write($"{result?.GetType()}\r\n", ConsoleColor.Blue);
            _output.Write($"{_loopCount:N0}", ConsoleColor.DarkBlue);
            _output.Write("times in ");
            _output.Write($"{timeSpan} ", ConsoleColor.Red);
            _output.Write("milliseconds");
        }
    }
}
