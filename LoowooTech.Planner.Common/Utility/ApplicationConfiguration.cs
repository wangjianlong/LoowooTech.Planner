using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml;

namespace LoowooTech.Planner.Common.Utility
{
    /// <summary>
    /// 作用：应用配置属性
    /// 作者：汪建龙
    /// 编写时间：2016年12月17日18:31:39
    /// </summary>
    public class ApplicationConfiguration: IConfigurationSectionHandler
    {
        private static bool tracingEnabled { get; set; }

        private static string tracingTraceFile { get; set; }
        private static TraceLevel tracingTraceLevel { get; set; }
        public static TraceLevel TracingTraceLevel { get { return tracingTraceLevel; } }
        private static string tracingSwitchName { get; set; }
        private static string tracingSwitchDescription { get; set; }
        private static bool eventLogEnabled { get; set; }
        private static string eventLogMachineName { get; set; }

        private static string eventLogSourceName { get; set; }
        private static string _AppRoot { get; set; }
        /// <summary>
        /// 应用启动目录
        /// </summary>
        public static string AppRoot { get { return _AppRoot; } }

        private static string _DefaultDataSourceName { get; set; }
        /// <summary>
        /// 数据库名字
        /// </summary>
        public static string DefaultDataSourceName { get { return _DefaultDataSourceName; }set { _DefaultDataSourceName = value; } }

        private static string _DataSourceConfigFile { get; set; }
        public static string DataSourceConfigFile { get { return _DataSourceConfigFile; }set { _DataSourceConfigFile = value; } }

        static ApplicationConfiguration()
        {
            tracingEnabled = true;
            tracingTraceFile = AppDomain.CurrentDomain.BaseDirectory + string.Format("trace{0}.txt", DateTime.Now.ToString("yyyyMM"));
            tracingTraceLevel = TraceLevel.Verbose;
            tracingSwitchName = "ApplicationTraceSwitch";
            tracingSwitchDescription = "Application error and tracing information";
            OnApplicationStart(AppDomain.CurrentDomain.BaseDirectory);
        }
        public static void OnApplicationStart(string appPath)
        {
            _AppRoot = appPath + "\\";
            ConfigurationManager.GetSection("ApplicationConfiguration");
            LoadAppSettings();
        }
        private static void LoadAppSettings()
        {
            _DataSourceConfigFile = AppRoot + ConfigurationManager.AppSettings["DataSourceConfigFile"];
            if (_DataSourceConfigFile == AppRoot)
            {
                _DataSourceConfigFile = null;
            }
            var name= ConfigurationManager.AppSettings["DefaultDataSourceName"];
            _DefaultDataSourceName = name;
        }

        public object Create(object parent,object configContext,XmlNode section)
        {
            return null;
        }
    }
}
