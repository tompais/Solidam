using Exceptions;
using System;

namespace Helpers
{
    public static class LoggingHelper
    {
        private static log4net.ILog Logger4Net { get; set; }
        private static NLog.Logger NLogger { get; set; }

        private static log4net.ILog GetLogger(Type type) => log4net.LogManager.GetLogger(type);

        private static NLog.Logger GetNLogger(Type type) => NLog.LogManager.GetCurrentClassLogger(type);

        public static void Configure(Type type)
        {
            Logger4Net = GetLogger(type);
            NLogger = GetNLogger(type);
        }

        public static void LogException(Exception exception)
        {
            Logger4Net.Error(exception);

            if (exception is SolidamException solidamException)
                NLogger.Error(solidamException, solidamException.GetCustomErrorMessage());
            else
                NLogger.Error(exception);
        }
    }
}