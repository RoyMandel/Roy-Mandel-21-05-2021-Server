using NLog.Web;
using System;

namespace Framework.Logger
{
    public static class Logger
    {
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(AppDomain.CurrentDomain.BaseDirectory + "nlog.config").GetCurrentClassLogger();

        public static void Error(string errorMessage = null, Exception exception = null)
        {
            if (exception == null)
            {
                logger.Error(errorMessage);
            }
            else { logger.Error(exception, errorMessage); }
        }

        public static void Info(string message)
        {
            if (message != null)
            {
                logger.Info(message);
            }
        }

        public static void Warn(string message)
        {
            if (message != null)
            {
                logger.Warn(message);
            }
        }
    }
}
