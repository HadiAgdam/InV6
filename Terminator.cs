using System.Collections.Generic;


namespace Invisix
{
    class Terminator : Operation
    {
        public override void execute(Dictionary<string, string> arguments)
        {
            if (arguments["mode"] == "add")
                TerminatorService.add(arguments["program"]);
            else if (arguments["mode"] == "remove")
                TerminatorService.remove(arguments["program"]);
        }
    }
}
