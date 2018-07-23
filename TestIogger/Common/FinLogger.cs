using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestIogger.Common
{
    class FinLogger
    {
        public static void Log(string message, LoggerLevel level, LogArguments logArgs)
        {
            var loggerMessage = $"Execution: {logArgs.ExecutionId} ({logArgs.FunctionAppName})\n\n{message}";

            //Had to disable logging as we're getting charged a fortune for it by Azure contrary to the way that the Function claims to act.

            if (logArgs.Logger != null)
                switch (level)
                {
                    case LoggerLevel.Info:
                        logArgs.Logger.LogInformation(loggerMessage);
                        break;
                    case LoggerLevel.Success:
                        logArgs.Logger.LogInformation(loggerMessage);
                        break;
                    case LoggerLevel.Error:
                        logArgs.Logger.LogError(loggerMessage);
                        break;
                    case LoggerLevel.Trace:
                        logArgs.Logger.LogTrace(loggerMessage);
                        break;
                }
        }
    }

    enum LoggerLevel
    {
        Info = 0,
        Success = 1,
        Error = 2,
        Trace = 3,
    }

    public class LogArguments
    {
        public LogArguments(ILogger logger, LoggerApp loggerApp, string executionId, string functionAppName)
        {
            Logger = logger;
            LoggerApp = loggerApp;
            ExecutionId = executionId;
            FunctionAppName = functionAppName;
        }

        public ILogger Logger { get; set; }
        public LoggerApp LoggerApp { get; set; }
        public string FunctionAppName { get; set; }
        public string ExecutionId { get; set; }
    }

    public enum LoggerApp
    {
        ThisApp = 0
    }
}
