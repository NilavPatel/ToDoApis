using NLog.Web;
using System;

namespace Todo.api.Logger
{
    public class CustomLogger: ICustomLogger
    {
        public NLog.Logger logger = NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();

        public void LogError(string errorMessage)
        {
            logger.Error(errorMessage);
        }

        public void LogError(string errorMessage, params object[] args)
        {
            logger.Error(errorMessage, args);
        }

        public void LogException(Exception ex)
        {
            logger.Error(ex);
        }

        public void LogInfo(string infoMessage)
        {
            logger.Info(infoMessage);
        }

        public void LogInfo(string infoMessage, params object[] args)
        {
            logger.Info(infoMessage, args);
        }
    }
}
