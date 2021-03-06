﻿using LoowooTech.Planner.WorkBench.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LoowooTech.Planner.WorkBench.Logs.Models
{
    /// <summary>
    /// 作用：日志记录，封装了Log4net钟日志对象
    /// </summary>
    internal sealed class SystemLog
    {

        private log4net.ILog _Log { get; set; }

        static SystemLog()
        {
            FileInfo fileInfo = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["LogConfig"]);

            log4net.Config.XmlConfigurator.Configure(fileInfo);
        }


        public SystemLog()
        {
            _Log = log4net.LogManager.GetLogger(this.GetType());
            SetAdoNetAppendersConnection(_Log, GetDefaultConnectionString());
        }
        public SystemLog(string loggerName)
        {
            _Log = log4net.LogManager.Exists(loggerName);
            SetAdoNetAppendersConnection(_Log, GetDefaultConnectionString());
        }

        private string GetDefaultConnectionString()
        {
            IDbHelp dbhelp = DbFactory.Instance.GetDbHelp();
            return dbhelp.DataSourceInfo.ConnectionString;
        }

        private void SetAdoNetAppendersConnection(log4net.ILog log,string connectionString)
        {
            log4net.Appender.IAppender[] appenders = log.Logger.Repository.GetAppenders();
            foreach(log4net.Appender.IAppender appender in appenders)
            {
                if(appender is log4net.Appender.AdoNetAppender)
                {
                    log4net.Appender.AdoNetAppender adoNetAppender = appender as log4net.Appender.AdoNetAppender;
                    adoNetAppender.ConnectionString = connectionString;
                    adoNetAppender.ActivateOptions();
                }
            }
        }

        #region Error
        public void Error(object message, Exception ex)
        {
            _Log.Error(message, ex);
        }

        public void Error(object message)
        {
            _Log.Error(message);
        }

        public void ErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
            _Log.ErrorFormat(provider, format, args);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            _Log.ErrorFormat(format, args);
        }

        #endregion

        #region Fatal error

        public void Fatal(object message, Exception ex)
        {
            _Log.Fatal(message, ex);
        }

        public void Fatal(object message)
        {
            _Log.Fatal(message);
        }

        public void FatalFormat(IFormatProvider provider, string format, params object[] args)
        {
            _Log.FatalFormat(provider, format, args);
        }

        public void FatalFormat(string format, params object[] args)
        {
            _Log.FatalFormat(format, args);
        }

        #endregion

        #region Information

        public void Info(object message, Exception ex)
        {
            _Log.Info(message, ex);
        }

        public void Info(object message)
        {
            _Log.Info(message);
        }

        public void InfoFormat(IFormatProvider provider, string format, params object[] args)
        {
            _Log.InfoFormat(provider, format, args);
        }

        public void InfoFormat(string format, params object[] args)
        {
            _Log.InfoFormat(format, args);
        }

        #endregion

        #region Warn

        public void Warn(object message, Exception ex)
        {
            _Log.Warn(message, ex);
        }

        public void Warn(object message)
        {
            _Log.Warn(message);
        }

        public void WarnFormat(IFormatProvider provider, string format, params object[] args)
        {
            _Log.WarnFormat(provider, format, args);
        }

        public void WarnFormat(string format, params object[] args)
        {
            _Log.WarnFormat(format, args);
        }

        #endregion
    }
}
