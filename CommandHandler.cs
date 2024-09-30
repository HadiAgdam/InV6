using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invisix
{
    class CommandHandler
    {

        Dictionary<string, Operation> operations = new Dictionary<string, Operation>()
        {
            { "terminator", new Terminator() }
        };

        public void Handle(Command command)
        {
            operations[command.command].execute(command.args);
        }

    }
}
