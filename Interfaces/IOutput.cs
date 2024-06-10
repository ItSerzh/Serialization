using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serialization.Interfaces
{
    public interface IOutput
    {
        void Write(string str, ConsoleColor color = ConsoleColor.White);
    }
}
