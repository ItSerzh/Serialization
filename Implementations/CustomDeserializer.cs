using Microsoft.Extensions.Configuration;
using Serialization.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Serialization.Implementations
{
    public class CustomDeserializer<T>(IConfiguration cfg) : IDeserializer<T> where T : class, new()
    {
        private char _delimiter = cfg.GetValue<char>("Delimiter");
        private int _loopCount = cfg.GetValue<int>("LoopCount");

        public T Deserialize(string str)
        {
            var serializedFields = GetDictionary(str);
            var obj = (T)Activator.CreateInstance(typeof(T));
            var bindingFlags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance;
            var fields = obj.GetType().GetFields(bindingFlags);
            foreach (var field in fields)
            {
                string fieldVal;
                serializedFields.TryGetValue(field.Name, out fieldVal);
                var val = Convert.ChangeType(fieldVal, field.FieldType);
                field.SetValue(obj, val);
            }

            return obj;
        }

        private Dictionary<string, string> GetDictionary(string str)
        {
            return str.Split(_delimiter)
                .Select(s => s.Split(" "))
                .ToDictionary(s => s[0], s => s[1]);
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
