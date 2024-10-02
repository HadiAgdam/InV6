using System;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace Invisix
{
    class TerminatorService
    {
        private static string[] programs;
        private const string filename = ".\t_p";

        private static string readFile()
        {
            FileStream fs = File.Open(filename, FileMode.OpenOrCreate);

            try
            {
                var result = new byte[fs.Length];

                fs.Read(result, 0, result.Length);
                fs.Close();

                string text = "";

                foreach (char c in result)
                    text += c;

                if (text == null || text == "")
                    return "";
                else
                    return EncryptionService.Decrypt(text);
            }
            catch (Exception ex)
            {
                fs.Close();

                Log.e("load terminator file failed", ex);

                return null;
            }
        }

        private static void writeFile(string content)
        {
            content = EncryptionService.Encrypt(content);
            byte[] resultByteArray = new byte[content.Length];

            for (int i = 0; i < content.Length; i++)
                resultByteArray[i] = (byte)content[i];

            FileStream writer = File.OpenWrite(filename);
            writer.Write(resultByteArray, 0, resultByteArray.Length);
            writer.Close();
        }

        private void LoadPrograms()
        {
            string text = readFile();

            if (text.Length == 0) { programs = new string[0]; Log.r("yes"); }
            else
            {
                programs = text.Split(';');
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
                Log.r("'" + program + "' killed");
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
            foreach (string p in programs)
                if (p == program) return;

            var tmp = new string[programs.Length + 1];
            for (int i = 0; i < programs.Length; i++)
                tmp[i] = programs[i];
            tmp[tmp.Length - 1] = program;
            programs = tmp;

            try
            {
                string content = readFile();

                if (content != "")
                    content += ";" + program;
                else
                    content = program;


                writeFile(content);

                Log.r("terminator added '" + program + "'");
            }
            catch (Exception ex)
            {
                Log.e("failed to add terminator program", ex);
            }
        }

        public static void remove(string program)
        {

            bool found = false;
            foreach (string p in programs)
                if (program == p)
                {
                    found = true;
                    break;
                }
            if (!found) return;

            string[] newContent = new string[programs.Length - 1];

            int j = 0;
            for (int i = 0; i < newContent.Length; i++)
            {
                if (j == 0 && programs[i] == program) j++;
                newContent[i] = programs[i + j];
            }
            programs = newContent;

            string text = "";

            for (int i = 0; i < programs.Length; i++)
            {
                text += programs[i];
                if (i != programs.Length - 1) text += ";";
            }


            string newPrograms = "";
            foreach (string p in programs)
                newPrograms += p + " ";
            Log.r("delete; new programs :" + newPrograms);

            try
            {
                writeFile(text);
            }
            catch (Exception ex)
            {
                Log.e("failed to remove terminator program");
            }

        }

    }
}
