using Microsoft.Extensions.Configuration;
using Serialization.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Serialization.Implementations
{
    public class CustomSerializer<T>(IConfiguration cfg, IOutput output) : ISerializer<T> where T : class
    {
        private char _delimiter = cfg.GetValue<char>("Delimiter");
        private int _loopCount = cfg.GetValue<int>("LoopCount");
        private IOutput _output = output;

        public string Serialize(T obj)
        {
            var result = new StringBuilder();
            var bindingFlags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance;
            var properties = obj.GetType().GetProperties(bindingFlags);
            var fields = obj.GetType().GetFields(bindingFlags);

            foreach (var field in fields)
            {
                result.Append($"{field.Name} {field.GetValue(obj)}{_delimiter}");
            }

            foreach (var property in properties)
            {
                result.Append($"{property.Name} {property.GetValue(obj)}{_delimiter}");
            }

            return result.ToString().TrimEnd(_delimiter);
        }

        public string SerializeInLoop(T obj, bool needOutput = false)
        {
            for (int i = 0; i < _loopCount; i++)
            {
                var resut = Serialize(obj);
                if (needOutput)
                {
                    _output.Write($"{resut}\r\n");
                }
            }
            return Serialize(obj);
        }
    }
}
