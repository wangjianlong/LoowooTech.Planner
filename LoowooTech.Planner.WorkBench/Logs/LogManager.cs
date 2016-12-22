using LoowooTech.Planner.WorkBench.Logs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.Planner.WorkBench.Logs
{
    /// <summary>
    /// 作用：日志管理
    /// 作者：汪建龙
    /// 编写时间：2016年12月18日21:02:46
    /// </summary>
    public sealed class LogManager
    {
        private SystemLog _loginLog { get; set; }
        private SystemLog _operationLog { get; set; }
        private SystemLog _errorLog { get; set; }

        static LogManager()
        {

        }

        private LogManager()
        {
            _loginLog = new SystemLog("LoginLogger");
            _operationLog = new SystemLog("OperationLogger");
            _errorLog = new SystemLog("ErrorLogger");

        }
        /// <summary>
        /// 获取SystemLogger实例
        /// </summary>
        public static LogManager Log { get { return LoggerNested.SystemLoggerMan; } }
        /// <summary>
        /// 当前登录用户名
        /// </summary>
        public string CurrentLoginUserName { get { return WorkBench.LoginUserName; } }

        #region 错误日志

        public void LogError(string userName, string message, Exception ex)
        {
            if (string.IsNullOrEmpty(message) && ex == null)
                return;
            LogItem omsg = new LogItem();
            omsg.UserName = userName;
            omsg.Category = LogCategoryType.Error;
            omsg.Message = message;
            omsg.Name = "Error";
            _errorLog.Error(omsg, ex);
        }
        public void LogError(string message, Exception ex)
        {
            LogError(CurrentLoginUserName, message, ex);
        }

        public void LogError(string userName, string message)
        {
            LogError(userName, message, null);
        }

        public void LogError(string message)
        {
            LogError(CurrentLoginUserName, message, null);
        }

        /// <summary>
        /// 警告.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public void LogWarn(string userName, string message, Exception ex)
        {
            if (string.IsNullOrEmpty(message) &&
                ex == null)
                return;
            LogItem omsg = new LogItem();
            omsg.UserName = userName;
            omsg.Category = LogCategoryType.Error;
            omsg.Message = message;
            omsg.Name = "Warn";
            _errorLog.Warn(omsg, ex);
        }

        /// <summary>
        /// 警告.
        /// </summary>
        /// <param name="message"></param>
        public void LogWarn(string userName, string message)
        {
            LogWarn(userName, message, null);
        }

        /// <summary>
        /// 警告.
        /// </summary>
        /// <param name="message"></param>
        public void LogWarn(string message)
        {
            LogWarn(CurrentLoginUserName, message, null);
        }

        /// <summary>
        /// 致命错误
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public void LogFatalError(string userName, string message, Exception ex)
        {
            if (string.IsNullOrEmpty(message) &&
                ex == null)
                return;
            LogItem omsg = new LogItem();
            omsg.UserName = userName;
            omsg.Category = LogCategoryType.Error;
            omsg.Message = message;
            omsg.Name = "Fatal";
            _errorLog.Fatal(omsg, ex);
        }

        /// <summary>
        /// 致命错误
        /// </summary>
        /// <param name="message"></param>
        public void LogFatalError(string userName, string message)
        {
            LogFatalError(userName, message, null);
        }

        /// <summary>
        /// 致命错误
        /// </summary>
        /// <param name="message"></param>
        public void LogFatalError(string message)
        {
            LogFatalError(CurrentLoginUserName, message, null);
        }

        #endregion

        /// <summary>
        /// 作用：内部类及静态成员实现Singleton延迟实例化和线程安全
        /// </summary>
        private class LoggerNested
        {
            public static readonly LogManager SystemLoggerMan = new LogManager();
            static LoggerNested()
            {

            }
        }
    }
}
