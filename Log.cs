using System;
using System.IO;

namespace Invisix
{

    static class Log
    {
        public static void e(string text)
        {
            e(text, new Exception());
        }

        public static void e(string text, Exception exception)
        {
            StreamWriter writer = File.AppendText("log.txt");

            writer.Write("\n\n-----------------------------------------------------------------------\n\n" + text + "\n" + exception.ToString());

            writer.Close();
        }

    }

}
