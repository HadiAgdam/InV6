using System.Collections.Generic;


namespace Invisix
{
    class Terminator : Operation
    {
        public override void execute(Dictionary<string, string> arguments)
        {
            TerminatorService.add(arguments["program"]);
        }
    }
}
