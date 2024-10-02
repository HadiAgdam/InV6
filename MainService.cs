using System.ServiceProcess;
using System.Threading.Tasks;


namespace Invisix
{
    public partial class MainService : ServiceBase
    {
        private Listener listener;
        private TerminatorService terminator;


        public MainService()
        {


        }


        protected override async void OnStart(string[] args)
        {

            terminator = new TerminatorService();
            terminator.start();
            listener = new Listener();
            await listener.StartListening();



            // new Terminator().execute(new System.Collections.Generic.Dictionary<string, string> { { "program", "notepad" } });
        }

        protected override void OnStop()
        {

        }




    }
}
