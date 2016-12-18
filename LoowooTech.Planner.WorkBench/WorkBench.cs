using LoowooTech.Planner;
using LoowooTech.Planner.Winforms;
using LoowooTech.Planner.WorkBench.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        #region  窗体

        private static FormMain _mainForm { get;  set; }
        /// <summary>
        /// 主窗体
        /// </summary>
        public static FormMain MainForm
        {
            get
            {
                if (_mainForm == null || _mainForm.Created == false || _mainForm.IsDisposed == false)
                {
                    lock (objToLock)
                    {
                        if (_mainForm == null || _mainForm.Created == false || _mainForm.IsDisposed == false)
                        {

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
               // LogManager.Log
            }catch(Exception ex)
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


            }catch(Exception ex)
            {

            }
        }
        #endregion
    }
}
