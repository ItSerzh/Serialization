using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serialization.Interfaces
{
    public interface ISpeedMeter<T>
    {
        void CheckSerializationSpeed(Func<T, bool, string> func, T someType, bool needOutput = false);
        void CheckDeserializationSpeed(Func<string, T> func, string serialized);
    }
}
