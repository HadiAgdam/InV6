using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invisix
{
    abstract class Operation
    {
        public abstract void execute(Dictionary<string, string> args);
    }
}
