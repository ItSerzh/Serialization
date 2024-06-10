using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serialization.Interfaces
{
    public interface ISerializer<in T>
    {
        public string Serialize(T obj);
        string SerializeInLoop(T obj, bool needOutput = false);
    }
}
