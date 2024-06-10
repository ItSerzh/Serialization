using Microsoft.Extensions.Configuration;
using Serialization.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Serialization.Implementations
{
    public class JsonSerializer<T>(IConfiguration cfg) : ISerializer<T> where T : class, new()
    {
        private int _loopCount = cfg.GetValue<int>("LoopCount");
        public string Serialize(T obj)
        {
            return JsonSerializer.Serialize(obj);
        }

        public string SerializeInLoop(T obj, bool needOutput)
        {
            for (int i = 0; i < _loopCount; i++)
            {
                var result = Serialize(obj);
                if (needOutput)
                {
                    Console.WriteLine(result);
                }
            }
            return Serialize(obj);
        }
    }
}
