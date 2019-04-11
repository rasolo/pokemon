using System;

namespace Pokemon.Api.Core.Logging
{
    /// <summary>
    ///     The main class used by the application to log messages.
    /// </summary>
    public interface ILoggingService
    {
        /// <summary>
        ///     Logs a message with debug level.
        /// </summary>
        void Debug(object message);

        /// <summary>
        ///     Logs a message with debug level including the stack trace of the <see cref="T:System.Exception" />.
        /// </summary>
        void Debug(object message, Exception exception);

        /// <summary>
        ///     Logs a message with error level.
        /// </summary>
        void Error(object message);

        /// <summary>
        ///     Logs a message with fatal level including the stack trace of the <see cref="T:System.Exception" />.
        /// </summary>
        void Error(object message, Exception exception);

        /// <summary>
        ///     Logs a message with info level.
        /// </summary>
        void Info(object message);

        /// <summary>
        ///     Logs a message with info level including the stack trace of the <see cref="T:System.Exception" />.
        /// </summary>
        void Info(object message, Exception exception);

        /// <summary>
        ///     Logs a message with warn level.
        /// </summary>
        void Warn(object message);

        /// <summary>
        ///     Logs a message with warn level including the stack trace of the <see cref="T:System.Exception" />.
        /// </summary>
        void Warn(object message, Exception exception);

        /// <summary>
        ///     Logs a message with fatal level.
        /// </summary>
        void Fatal(object message);

        /// <summary>
        ///     Logs a message with fatal level including the stack trace of the <see cref="T:System.Exception" />.
        /// </summary>
        void Fatal(object message, Exception exception);
    }
}