using log4net;
using log4net.Config;
using System;
using System.Diagnostics;
using System.Reflection;

namespace Shared
{
    public static class Logs
    {
        private static log4net.ILog _log = SetLog();

        // This nonsense is to get the log configured through project dependencies
        public static ILog SetLog()
        {
            _log = log4net.LogManager.GetLogger(typeof(Logs));
            XmlConfigurator.Configure();
            return _log;
        }

        /// <summary>
        /// Finds the calling method name by working down the stacktrace
        /// </summary>
        /// <returns>The name of the calling method</returns>
        private static string GetMethodName()
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame;
            MethodBase stackFrameMethod;
            int frameCount = 0;
            string typeName;

            do
            {
                frameCount++;
                stackFrame = stackTrace.GetFrame(frameCount);
                stackFrameMethod = stackFrame.GetMethod();
                typeName = stackFrameMethod.ReflectedType.FullName;
            }
            while (typeName.EndsWith("Logs"));

            return typeName + "." + stackFrameMethod.Name;
        }

        /// <summary>
        /// Logs a warning
        /// </summary>
        /// <param name="message">The warning</param>
        public static void Warn(string message)
        {
            _log.Warn(GetMethodName() + ": " + message);
        }

        /// <summary>
        /// Logs a warning
        /// </summary>
        /// <param name="message">The warning</param>
        /// <param name="list">an array of parameters</param>
        public static void Warn(string message, params string[] list)
        {
            string infoMessage = string.Format(message, list);

            Warn(GetMethodName() + ": " + infoMessage);
        }

        /// <summary>
        /// Logs an Error message
        /// </summary>
        /// <param name="message">The error message</param>
        public static void Error(string message)
        {
            _log.Error(GetMethodName() + ": " + message);
        }

        /// <summary>
        /// Logs an Error message and an excpetion
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="ex">The Exception</param>
        public static void Error(string message, Exception ex)
        {
            _log.Error(GetMethodName() + ": " + message, ex);
        }

        /// <summary>
        /// Logs an Error message and an exception
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="ex">The Exception</param>
        /// <param name="list">an array of parameters</param>
        public static void Error(string message, Exception ex, params string[] list)
        {
            string infoMessage = string.Format(message, list);

            _log.Error(GetMethodName() + ": " + infoMessage, ex);
        }

        /// <summary>
        /// Logs an Info message
        /// </summary>
        /// <param name="message">the message</param>
        public static void Info(string message)
        {
            _log.Info(GetMethodName() + ": " + message);
        }

        /// <summary>
        /// Logs an Info message
        /// </summary>
        /// <param name="message">the message</param>
        /// <param name="list">an array of parameters</param>
        public static void Info(string message, params string[] list)
        {
            string infoMessage = string.Format(message, list);

            Info(GetMethodName() + ": " + infoMessage);
        }

        /// <summary>
        /// Logs a Trace message
        /// </summary>
        /// <param name="message">the message</param>
        public static void Trace(string message)
        {
            _log.Debug(GetMethodName() + ": " + message);
        }

        /// <summary>
        /// Logs a Trace message
        /// </summary>
        /// <param name="message">the message</param>
        /// <param name="list">an array of parameters</param>
        public static void Trace(string message, params string[] list)
        {
            string traceMessage = string.Format(message, list);

            Trace(GetMethodName() + ": " + traceMessage);
        }

        [Conditional("ENTEREXIT")]
        public static void Enter()
        {
            _log.Info(GetMethodName() + ": In");
        }

        [Conditional("ENTEREXIT")]
        public static void Enter(string s)
        {
            _log.Info(GetMethodName() + ": In with " + s);
        }

        [Conditional("ENTEREXIT")]
        public static void Exit()
        {
            _log.Info(GetMethodName() + ": Out");
        }

        [Conditional("ENTEREXIT")]
        public static void Exit(string s)
        {
            _log.Info(GetMethodName() + ": Out with " + s);
        }
    }
}

