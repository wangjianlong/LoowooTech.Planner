using DevExpress.XtraBars.Ribbon;
using LoowooTech.Planner;
using LoowooTech.Planner.Winforms;
using LoowooTech.Planner.WorkBench.Logs;
using LoowooTech.Planner.WorkBench.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LoowooTech.Planner.WorkBench
{
    /// <summary>
    /// 工作台
    /// 
    /// </summary>
    public static  class WorkBench
    {
        private static object objToLock { get; set; }

        private static string _userName { get; set; }
        public static string LoginUserName { get { return _userName; }set { _userName = value;  } }

        private static string _configFileName { get; set; }
        /// <summary>
        /// 配置文件路径
        /// </summary>
        public static string ConfigFileName
        {
            get
            {
                if (string.IsNullOrEmpty(_configFileName))
                {
                    return System.IO.Path.Combine(Application.StartupPath, System.Configuration.ConfigurationManager.AppSettings["Config"]);
                }
                else
                {
                    return _configFileName;
                }
            }
            set { _configFileName = value; }
        }

        static WorkBench()
        {
            objToLock = new object();
        }
        #region  窗体

        private static Form _mainForm { get;  set; }
        /// <summary>
        /// 主窗体
        /// </summary>
        public static Form MainForm
        {
            get
            {
                if (_mainForm == null || _mainForm.Created == false || _mainForm.IsDisposed == false)
                {
                    lock (objToLock)
                    {
                        if (_mainForm == null || _mainForm.Created == false || _mainForm.IsDisposed == false)
                        {
                            System.Windows.Forms.Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
                            System.AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

                            SysIniter sysIniter = new SysIniter();
                            sysIniter.XMLConfigFilePath = ConfigFileName;
                            sysIniter.Initer();
                        }
                    }
                }
                return _mainForm;
            }

            internal set { _mainForm = value; }
        }
        #endregion


        #region  管理主窗体界面



        #endregion

        #region DEV控件

        private static RibbonControl _ribbonControl { get; set; }
        /// <summary>
        /// RibbonControl控件
        /// </summary>
        public static RibbonControl RibbonControl { get { return _ribbonControl; }set { _ribbonControl = value; } }

        #endregion


        #region 异常处理事件
        /// <summary>
        /// 作用：未捕获线程异常处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void Application_ThreadException(object sender,System.Threading.ThreadExceptionEventArgs e)
        {
            try
            {

                string msg = "系统发生未处理的异常！";
                msg += "\r\n异常信息：" + e.Exception.Message;
                msg += "\r\n引发异常的对象：" + e.Exception.Source;
                msg += "\r\n引发异常的方法：" + e.Exception.TargetSite;
                msg += "\r\n错误堆栈：" + e.Exception.StackTrace.ToString();
                LogManager.Log.LogError(msg);

            }catch
            {

            }
        }


        public static void CurrentDomain_UnhandledException(object sender,UnhandledExceptionEventArgs e)
        {
            try
            {
                string msg = "系统发生未处理的异常！";
                msg += e.ToString();
                msg += e.ExceptionObject.ToString();
                LogManager.Log.LogError(msg);

            }catch
            {

            }
        }
        #endregion
    }
}
