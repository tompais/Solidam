namespace Helpers
{
    public static class LoggingHelper
    {
        public static log4net.ILog GetLogger<T>() => log4net.LogManager.GetLogger(typeof(T));
    }
}