using System;
using System.Diagnostics;
using System.Globalization;
using Exceptions;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace Helpers
{
    public static class LoggingHelper
    {
        private static Logger NLogger { get; set; }

        public static void Configure(Type type)
        {
            var config = new LoggingConfiguration();

            // Targets where to log to: File and Console
            var logfile = new FileTarget("logfile") { FileName = "NLog.log" };
            var logconsole = new ConsoleTarget("logconsole");

            // Rules for mapping loggers to targets
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);

            // Apply config
            LogManager.Configuration = config;

            //Set the logger
            NLogger = LogManager.GetCurrentClassLogger(type);
        }

        public static void FlushLogger()
        {
            LogManager.Shutdown();
        }

        public static void LogException(Exception exception)
        {
            NLogger.Error(exception, GetCustomErrorMessageFromException(exception));
        }

        private static string GetCustomErrorMessageFromException(Exception exception)
        {
            var stackFrame = new StackTrace(exception, true).GetFrame(0);
            var codigoError = exception is SolidamException solidamException
                ? "Nro " + (int)solidamException.ErrorCode
                : "Desconocido";
            var mensaje = exception.Message;
            var nombreArchivo = stackFrame.GetFileName();
            var metodo = stackFrame.GetMethod();
            var nombreClase = metodo.DeclaringType == null ? "(Desconocido)" : metodo.DeclaringType.Name;
            var nombreMetodo = metodo.Name;
            var numeroLinea = stackFrame.GetFileLineNumber().ToString();
            var stackTrace = exception.StackTrace;
            return
                $"[{DateTime.Now.ToString(CultureInfo.InvariantCulture)}] Error {codigoError}: '{mensaje}' en el archivo '{nombreArchivo}' en la clase '{nombreClase}' en el método '{nombreMetodo}' en la línea {numeroLinea}\nStackTrace: {stackTrace}\n\n";
        }
    }
}