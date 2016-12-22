using ESRI.ArcGIS.SystemUI;
using LoowooTech.Planner.WorkBench.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LoowooTech.Planner.WorkBench.Commands
{
    /// <summary>
    /// 绑定在界面上的按钮
    /// </summary>
    public class UICommand:IUICommand
    {
        private static IList<IUICommand> _UICommands { get; set; }
        public static IList<IUICommand> UICommands { get { return _UICommands; } set { _UICommands = value; } }
        static UICommand()
        {
            _UICommands = new List<IUICommand>();
        }

        private object _objToLock { get; set; }
        public UICommand()
        {
            _objToLock = new object();
            _createInstanceFailed = new List<string>();
        }


        private Object objCommand { get; set; }
        public object Command { get { return objCommand; } }
        private string _parameter { get; set; }
        /// <summary>
        /// 初始化参数
        /// </summary>
        public string Parameter { get { return _parameter; }set { _parameter = value; } }
        private object _hooker { get; set; }
        public object Hooker { get { return _hooker; }set { _hooker = value; } }
        private string _className { get; set; }
        /// <summary>
        /// 类名
        /// </summary>
        public string ClassName { get { return _className; }set { _className = value; } }
        private string _assemblyName { get; set; }
        /// <summary>
        /// 程序集
        /// </summary>
        public string AssemblyName { get { return _assemblyName; }set { _assemblyName = value; } }
       
        private DevExpress.XtraBars.BarButtonItem _barButtonItem { get; set; }
        public DevExpress.XtraBars.BarButtonItem BarButtonItem { get { return _barButtonItem; }set { _barButtonItem = value; } }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get
            {
                string name = string.Empty;
                if (_barButtonItem != null)
                {
                    name = _barButtonItem.Name;
                }
                return name;
            }
            set
            {
                if (_barButtonItem != null)
                {
                    _barButtonItem.Name = value;
                }
            }
        }
        /// <summary>
        /// 获取或设置是否可用
        /// </summary>
        public bool Enable
        {
            get
            {
                bool enable = false;
                try
                {
                    if (objCommand == null)
                    {
                        CreateCommand();
                    }

                    if((objCommand as ICommand) != null)
                    {
                        enable = (objCommand as ICommand).Enabled;
                    }else if((objCommand as ITLWCommand) != null)
                    {
                        enable = (objCommand as ITLWCommand).Enable;
                    }
                }catch(Exception ex)
                {
                    System.Diagnostics.Trace.WriteLine(ex);
                    //LogManager.Log.LogError(ex.ToString());
                }

                try
                {
                    if (_barButtonItem != null)
                    {
                        _barButtonItem.Enabled = enable;
                    }

                }catch(Exception ex)
                {
                    System.Diagnostics.Trace.WriteLine(ex);
                   // LogManager.Log.LogError(ex.ToString());
                }
                return enable;
            }
        }
        protected bool group { get; set; }
        /// <summary>
        /// 获取或设置界面展示上是否分组
        /// </summary>
        public bool Group { get { return group; }set { group = value; } }
        /// <summary>
        /// 获取按钮是否可见
        /// </summary>
        public bool Visible
        {
            get
            {
                return _barButtonItem != null && _barButtonItem.Visibility == DevExpress.XtraBars.BarItemVisibility.Always;
            }
            set
            {
                if (_barButtonItem != null && value == true)
                {
                    _barButtonItem.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                }
            }
        }
        /// <summary>
        /// 获取界面展示上按钮是否处于选中状态
        /// </summary>
        public bool Checked
        {
            get
            {
                bool check = false;
                try
                {
                    if (objCommand == null)
                    {
                        CreateCommand();
                    }

                    if((objCommand as ICommand) != null)
                    {
                        check = (objCommand as ICommand).Checked;

                        if(!check&&objCommand is ITool)
                        {
                            if (WorkBench.AxMapControl != null 
                                && WorkBench.AxMapControl.CurrentTool != null 
                                && WorkBench.AxMapControl.CurrentTool.GetType().Equals(objCommand.GetType()))
                            {
                                check = true;
                            }else if (WorkBench.AxPageLayoutControl != null 
                                && WorkBench.AxPageLayoutControl.CurrentTool != null 
                                && WorkBench.AxPageLayoutControl.CurrentTool.GetType().Equals(objCommand.GetType()))
                            {
                                check = true;
                            }
                        }
                    }else if((objCommand as ITLWCommand) != null)
                    {
                        check = (objCommand as ITLWCommand).Checked;
                    }

                    if (_barButtonItem != null)
                    {
                        if (_barButtonItem.ButtonStyle != DevExpress.XtraBars.BarButtonStyle.Check)
                        {
                            _barButtonItem.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
                        }

                        _barButtonItem.Down = check;
                    }
                }catch(Exception ex)
                {
                    System.Diagnostics.Trace.WriteLine(ex);
                    //LogManager.Log.LogError(ex.ToString());
                }

                return check;
            }
        }

        /// <summary>
        /// 获取Message（鼠标停留在按钮上时状态栏显示的文本）
        /// </summary>
        public string Message
        {
            get
            {
                if (objCommand == null)
                {
                    CreateCommand();
                }
                string msg = string.Empty;
                try
                {
                    if(objCommand is ITLWCommand)
                    {
                        msg = (objCommand as ITLWCommand).Message;
                    }else if(objCommand is ICommand)
                    {
                        msg = (objCommand as ICommand).Message;
                    }

                }catch(Exception ex)
                {
                    System.Diagnostics.Trace.WriteLine(ex);
                   // LogManager.Log.LogError(ex.ToString());
                }
                return msg;
            }
        }

        /// <summary>
        /// 获取Tooltip(鼠标停留在按钮上时鼠标附近显示的文本)
        /// </summary>
        public string Tooltip
        {
            get
            {
                if (objCommand == null)
                {
                    CreateCommand();
                }
                string msg = string.Empty;

                try
                {
                    if(objCommand is ITLWCommand)
                    {

                    }else if(objCommand is ICommand)
                    {
                        msg = (objCommand as ICommand).Tooltip;
                    }

                    if (_barButtonItem != null)
                    {
                        _barButtonItem.Hint = msg;
                    }
                }catch(Exception ex)
                {
                    System.Diagnostics.Trace.WriteLine(ex);
                   // LogManager.Log.LogError(ex.ToString());
                }
                return msg;

            }
        }

        /// <summary>
        /// 实例化失败列表
        /// </summary>
        private List<string> _createInstanceFailed { get; set; }

        /// <summary>
        /// 作用：实例化
        /// 作者：汪建龙
        /// 编写时间：2016年12月20日10:27:33
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        private object CreateInstance(string assemblyName,string className)
        {
            try
            {
                object obj = null;
                if (_createInstanceFailed.Contains(assemblyName + "_" + className) == false)
                {
                    Assembly ass = Assembly.Load(assemblyName);
                    Type type = ass.GetType(className);
                    obj = Activator.CreateInstance(type);
                }
                return obj;
            }catch(Exception ex)
            {
                if (_createInstanceFailed.Contains(assemblyName + "_" + className) == false)
                {
                    _createInstanceFailed.Add(assemblyName + "_" + className);
                }
                System.Diagnostics.Trace.WriteLine(string.Format("在程序集{0}中创建类{1}的实例失败，详细信息：{2}", assemblyName, className, ex), "创建实例");
                return null;
            }
        }
        /// <summary>
        /// 作用：根据程序集名称和类名
        /// </summary>
        private void CreateCommand()
        {
            try
            {
                if (objCommand == null)
                {
                    lock (_objToLock)
                    {
                        if (objCommand == null)
                        {
                            object obj = CreateInstance(AssemblyName, ClassName);
                            if(obj is ICommand)
                            {
                                ((ICommand)obj).OnCreate(WorkBench.AxMapControl.Object);
                                if (WorkBench.AxToolbarControl != null)
                                {
                                    WorkBench.AxToolbarControl.AddItem(obj);
                                }else if(obj is ITLWCommand)
                                {
                                    try
                                    {
                                        ITLWCommand cmd = obj as ITLWCommand;
                                        cmd.Init(_parameter);

                                    }catch(Exception ex)
                                    {
                                        System.Diagnostics.Trace.WriteLine(ex);
                                        //LogManager.Log.LogError(ex.ToString());
                                    }
                                }
                                else
                                {
                                    obj= null;
                                }
                                objCommand = obj;
                            }
                        }
                    }
                }
            }catch(Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
                //LogManager.Log.LogError(ex.ToString());
                objCommand = null;
            }
        }

        /// <summary>
        /// 作用：单击按钮时触发的事件
        /// 作者：汪建龙
        /// 编写时间：2016年12月20日14:02:05
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnClick(object sender,DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (objCommand == null)
                {
                    CreateCommand();
                }

                if((objCommand as ICommand) != null)
                {
                    ICommand cmd = objCommand as ICommand;
                    cmd.OnClick();

                    ITool tool = cmd as ITool;
                    if (tool != null 
                        && WorkBench.AxToolbarControl != null 
                        && WorkBench.AxToolbarControl.IsDisposed == false)
                    {
                        WorkBench.AxToolbarControl.CurrentTool = tool;
                    }
                }else if((objCommand as ITLWCommand) != null)
                {
                    ITLWCommand cmd = objCommand as ITLWCommand;
                    cmd.OnClick();
                }

            }catch(Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
               // LogManager.Log.LogError(ex.ToString());
            }
        }


    }
}
