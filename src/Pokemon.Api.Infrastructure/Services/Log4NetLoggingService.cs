using log4net;
using Pokemon.Api.Core.Logging;
using System;

namespace Pokemon.Api.Infrastructure.Services
{
    public class Log4NetLoggingService : ILoggingService
    {
        public ILog Current => LogManager.GetLogger(typeof(Log4NetLoggingService));

        public void Debug(object message)
        {
            Current.Debug(message);
        }

        public void Debug(object message, Exception exception)
        {
            Current.Debug(message, exception);
        }

        public void Error(object message)
        {
            Current.Error(message);
        }

        public void Error(object message, Exception exception)
        {
            Current.Error(message, exception);
        }

        public void Info(object message)
        {
            Current.Info(message);
        }

        public void Info(object message, Exception exception)
        {
            Current.Info(message, exception);
        }

        public void Warn(object message)
        {
            Current.Warn(message);
        }

        public void Warn(object message, Exception exception)
        {
            Current.Warn(message, exception);
        }

        public void Fatal(object message)
        {
            Current.Fatal(message);
        }

        public void Fatal(object message, Exception exception)
        {
            Current.Fatal(message, exception);
        }
    }
}
