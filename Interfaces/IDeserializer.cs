using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serialization.Interfaces
{
    public interface IDeserializer<out T>
    {
        public T Deserialize(string str);

        public T DeserializeInLoop(string str);
    }
}
