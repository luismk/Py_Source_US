using System;
namespace PangyaAPI.Helper.Tools
{
    public static class WriteConsole
    {
        public static void Information(string message)
        {
            WriteLine($"[INFO]: {message}", ConsoleColor.White);
        }

        public static void Notice(string message)
        {
            WriteLine($"[NOTICE]: {message}", ConsoleColor.White);
        }

        public static void Warning(string message)
        {
            WriteLine($"[WARNING]: {message}", ConsoleColor.Yellow);
        }

        public static void Debug(string message)
        {
            WriteLine($"[DEBUG]: {message}", ConsoleColor.Cyan);
        }

        public static void Error(string message)
        {
            WriteLine($"[ERROR]: {message}", ConsoleColor.Red);
        }

        public static void FatalError(string message)
        {
            WriteLine($"[FATAL_ERROR]: {message}", ConsoleColor.DarkRed);
        }

        public static void Connected(string message)
        {
            WriteLine($"[PLAYER_CONNETED]: {message}", ConsoleColor.Green);
        }

        public static void Disconnected(string message)
        {
            WriteLine($"[PLAYER_DISCONNETED]: {message}", ConsoleColor.Red);
        }

        public static void Packet(string message)
        {
            WriteLine($"[HANDLE_REQUEST_PACKET]: {message}", ConsoleColor.Cyan);
        }


        public static void WriteLine()
        {
            Console.WriteLine();
        }

        public static void WriteLine(string format)
        {
            Console.WriteLine(DateTime.Now.ToString("[yyyy/MM/dd HH:mm:ss] ") + format);
        }
        
        public static void WriteLine(string format, params object[] args)
        {
            Console.WriteLine(string.Format(DateTime.Now.ToString("[yyyy/MM/dd HH:mm:ss] ") + format, args));
        }

        public static void WriteLine(string format, ConsoleColor Fore = ConsoleColor.White, params object[] args)
        {
            Console.ResetColor();
            Console.ForegroundColor = Fore;
            Console.WriteLine(string.Format(format, args));
            Console.ResetColor();
        }
        /// <summary>
        /// Format = [2020/07/08 13:06:35] [SERVER_START]: PORT 10103
        /// </summary>
        /// <param name="msg">Exemple: [SERVER_START]</param>
        /// <param name="Fore">Exemple: ConsoleColor.White</param>
        public static void WriteLine(string msg, ConsoleColor Fore = ConsoleColor.White)
        {
            Console.ResetColor();
            Console.ForegroundColor = Fore;
            Console.WriteLine(DateTime.Now.ToString("[yyyy/MM/dd HH:mm:ss] ") + msg);
            Console.ResetColor();
        }

        public static void Write(string msg = "", ConsoleColor Fore = ConsoleColor.White)
        {
            Console.ResetColor();
            Console.ForegroundColor = Fore;
            Console.WriteLine(msg);
            Console.ResetColor();
        }
    }
}
