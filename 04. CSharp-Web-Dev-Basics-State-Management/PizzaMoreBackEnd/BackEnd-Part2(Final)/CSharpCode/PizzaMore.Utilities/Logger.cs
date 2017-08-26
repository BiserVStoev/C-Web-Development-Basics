namespace PizzaMore.Utilities
{
    using System;
    using System.IO;

    public static class Logger
    {
        public static void Log(string message)
        {
            File.AppendAllText("log.txt", message + Environment.NewLine);
        }
    }
}
