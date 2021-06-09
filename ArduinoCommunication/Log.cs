using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading;

namespace ArduinoCommunication
{
    public static class Log
    {
        private static StringBuilder sb = new StringBuilder(2048);

        private static ConsoleColor DefaultBackgroundColor = Console.BackgroundColor;
        private static ConsoleColor DefaultForegroundColor = Console.ForegroundColor;

        public static bool EnableDebugOutput { get; set; } = false;

        public static string BuildLogMessage(string message, string type, bool new_line, bool time, string prefix)
        {
            lock (sb)
            {
                sb.Clear();

                sb.AppendFormat("[{0} {1}:{2}]", (time ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") : string.Empty), type, Thread.CurrentThread.ManagedThreadId);

                if (!string.IsNullOrWhiteSpace(prefix))
                    sb.AppendFormat("{0}", prefix);

                sb.AppendFormat(":{0}", message);

                if (new_line)
                    sb.AppendLine();

                return sb.ToString();
            }
        }

        internal static void Output(string message)
        {
            Console.Write(message);
        }

        internal static void ColorizeConsoleOutput(string message, ConsoleColor f, ConsoleColor b)
        {
            Console.BackgroundColor = b;
            Console.ForegroundColor = f;

            Output(message);

            Console.ResetColor();
        }

        public static void Info(string message, [CallerMemberName]string prefix = "<Unknown Method>")
        {
            var msg = BuildLogMessage(message, "INFO", true, true, prefix);
            ColorizeConsoleOutput(msg, ConsoleColor.Green, DefaultBackgroundColor);
        }

        public static void Debug(string message, [CallerMemberName]string prefix = "<Unknown Method>")
        {
            if (EnableDebugOutput)
            {
                var msg = BuildLogMessage(message, "DEBUG", true, true, prefix);
                Output(msg);
            }
        }

        public static void Warn(string message, [CallerMemberName]string prefix = "<Unknown Method>")
        {
            var msg = BuildLogMessage(message, "WARN", true, true, prefix);
            ColorizeConsoleOutput(msg, ConsoleColor.Yellow, DefaultBackgroundColor);
        }

        public static void Error(string message, [CallerMemberName]string prefix = "<Unknown Method>")
        {
            var msg = BuildLogMessage(message, "ERROR", true, true, prefix);
            ColorizeConsoleOutput(msg, ConsoleColor.Red, ConsoleColor.Yellow);
        }

        public static void Error(string message, Exception e, [CallerMemberName]string prefix = "<Unknown Method>")
        {
            message = $"{message} , Exception : {Environment.NewLine} {e.Message} {Environment.NewLine} {e.StackTrace}";
            var msg = BuildLogMessage(message, "ERROR", true, true, prefix);
            ColorizeConsoleOutput(msg, ConsoleColor.Red, ConsoleColor.Yellow);
        }
    }

    public static class Log<T>
    {
        public static readonly string PREFIX = typeof(T).Name;

        public static void Info(string message) => Log.Info(message, PREFIX);
        public static void Debug(string message) => Log.Debug(message, PREFIX);
        public static void Warn(string message) => Log.Warn(message, PREFIX);
        public static void Error(string message) => Log.Error(message, PREFIX);
    }
}