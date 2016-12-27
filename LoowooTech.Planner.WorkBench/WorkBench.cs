using DevExpress.XtraBars.Ribbon;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using LoowooTech.Planner;
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
                if (_mainForm == null || _mainForm.Created == false || _mainForm.IsDisposed == true)
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

             set { _mainForm = value; }
        }
        #endregion

        #region 子窗体

        /// <summary>
        /// 作用：查找子窗体
        /// 作者：汪建龙
        /// 编写时间：2016年12月27日14:02:13
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T FindDocument<T>() where T : ContainerControl
        {
            try
            {
                if (MainForm == null 
                    || MainForm.Created == false 
                    || MainForm.IsDisposed 
                    || MainForm.MdiChildren == null 
                    || MainForm.MdiChildren.Length == 0)
                {
                    return null;
                }

                foreach(Form subForm in MainForm.MdiChildren)
                {
                    if (subForm.IsDisposed)
                    {
                        continue;
                    }

                    if (subForm.GetType().Equals(typeof(T)) && subForm.Created && subForm.IsDisposed == false)
                    {
                        return subForm as T;
                    }
                    else
                    {
                        DevExpress.XtraEditors.XtraUserControl xtraUserControl = subForm.Tag as DevExpress.XtraEditors.XtraUserControl;
                        if (xtraUserControl != null && xtraUserControl.IsDisposed == false)
                        {
                            return xtraUserControl as T;
                        }
                    }
                }

            }catch(Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
            }
            return null;
        }

        /// <summary>
        /// 作用：显示子窗体
        /// 作者：汪建龙
        /// 编写时间：2016年12月27日14:06:07
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ShowDocument<T>() where T : Form, new()
        {
            try
            {

                T frm = FindDocument<T>();
                if (frm == null)
                {
                    frm = new T();
                    if (MainForm != null && MainForm.Created && MainForm.IsDisposed == false)
                    {
                        frm.MdiParent = MainForm;
                    }
                    frm.Show();
                }
                else
                {
                    frm.Activate();
                }

                return frm;
            }catch(Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
            }
            return null;
        }

        #endregion


        #region  管理主窗体界面

        /// <summary>
        /// 作用：设置RibbonControl中被选中的页
        /// 作者：汪建龙
        /// 编写时间：2016年12月22日19:10:26
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool SetSelectedPage(string name)
        {
            bool setted = false;
            try
            {
                UIUpdater updater = new UIUpdater();
                setted = updater.SetSelectPage(name);

            }catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return setted;
        }

        public static bool SetStatusBarValue(string name,string value)
        {
            bool setted = false;
            try
            {
                UIUpdater updater = new UIUpdater();
                setted = updater.SetStatusBarValue(name, value);


            }catch(Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
            }
            return setted;
        }

        public static void UpdateUI()
        {
            if (MainForm == null)
                return;

            try
            {
                AutoRunner runner = new AutoRunner();
                runner.XMLConfigFilePath = ConfigFileName;
                runner.Start();


            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
            }

        }
        #endregion

        #region DEV控件

        private static RibbonControl _ribbonControl { get; set; }
        /// <summary>
        /// RibbonControl控件
        /// </summary>
        public static RibbonControl RibbonControl { get { return _ribbonControl; }set { _ribbonControl = value; } }
        private static RibbonStatusBar _statisBar { get; set; }
        public static RibbonStatusBar StatusBar
        {
            get
            {
                if (_statisBar == null || _statisBar.Created == false || _statisBar.IsDisposed)
                {
                    return null;
                }
                return _statisBar;
            }
            set
            {
                _statisBar = value;
            }
        }

        #endregion

        #region ESRI控件
        private static AxPageLayoutControl _axPageLayoutControl { get; set; }
        public static AxPageLayoutControl AxPageLayoutControl
        {
            get { return _axPageLayoutControl; }
            set
            {
                try
                {
                    _axPageLayoutControl = value;
                    if (_axToolbarControl != null && _axToolbarControl.IsDisposed == false && _axToolbarControl.Created)
                    {
                        if (AxPageLayoutControl != null)
                        {
                            _axToolbarControl.SetBuddyControl(AxPageLayoutControl.Object);
                        }
                        else
                        {
                            _axToolbarControl.SetBuddyControl(null);
                        }
                    }

                }catch(Exception ex)
                {
                    throw new Exception(ex.ToString());
                    //LogManager.Log.LogError(ex.ToString());
                }
            }
        }

        private static AxToolbarControl _axToolbarControl { get; set; }
        public static AxToolbarControl AxToolbarControl
        {
            get { return _axToolbarControl; }
            set
            {
                try
                {
                    _axToolbarControl = value;
                    if (value != null && value.Created && value.IsDisposed == false)
                    {
                        _axToolbarControl.OperationStack = new ControlsOperationStackClass();
                        if (AxMapControl != null)
                        {
                            _axToolbarControl.SetBuddyControl(AxMapControl.Object);
                        }else if (AxPageLayoutControl != null)
                        {
                            _axToolbarControl.SetBuddyControl(AxPageLayoutControl.Object);
                        }
                    }
                }catch(Exception ex)
                {
                    //LogManager.Log.LogError(ex.ToString());
                }
            }
        }
        private static AxMapControl _axMapControl { get; set; }
        public static AxMapControl AxMapControl
        {
            get { return _axMapControl; }
            set
            {
                try
                {
                    _axMapControl = null;
                    if (_axMapControl != null && _axMapControl.IsDisposed == false && _axMapControl.Created)
                    {
                        if (_axMapControl != null)
                        {
                            _axToolbarControl.SetBuddyControl(_axMapControl.Object);
                        }
                        else
                        {
                            _axToolbarControl.SetBuddyControl(null);
                        }
                    }

                }catch(Exception ex)
                {
                    //LogManager.Log.LogError(ex.ToString());
                }
            }
        }

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
                // LogManager.Log.LogError(msg);
                throw new Exception(msg);

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
                throw new Exception(msg);
               // LogManager.Log.LogError(msg);

            }catch
            {

            }
        }
        #endregion
    }
}
