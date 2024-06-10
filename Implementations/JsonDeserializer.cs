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
    public class JsonDeserializer<T>(IConfiguration cfg) : IDeserializer<T>
    {
        private int _loopCount = cfg.GetValue<int>("LoopCount");
        public T Deserialize(string str)
        {
            return JsonSerializer.Deserialize<T>(str);
        }

        public T DeserializeInLoop(string str)
        {
            for (int i = 0; i < _loopCount; i++)
            {
                Deserialize(str);
            }
            return Deserialize(str);
        }
    }
}
