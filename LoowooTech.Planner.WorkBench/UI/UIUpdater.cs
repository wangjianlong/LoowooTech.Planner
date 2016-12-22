using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Ribbon.ViewInfo;
using LoowooTech.Planner.WorkBench.Commands;
using LoowooTech.Planner.WorkBench.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LoowooTech.Planner.WorkBench.UI
{
    /// <summary>
    /// 管理界面更新
    /// </summary>
    internal class UIUpdater
    {
        /// <summary>
        /// 作用：鼠标在RibbonControl控件上移动时触发的事件
        /// 作者：汪建龙
        /// 编写时间：2016年12月19日12:56:44
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ribbonControl_MouseMove(object sender,MouseEventArgs e)
        {
            try
            {
                RibbonControl ribbonControl = sender as RibbonControl;
                if (ribbonControl == null||e==null)
                {
                    return;
                }

                RibbonHitInfo hitInfo = ribbonControl.CalcHitInfo(new System.Drawing.Point(e.X, e.Y));
                if (hitInfo.InItem)
                {
                    BarButtonItem item = hitInfo.Item.Item as BarButtonItem;
                    if(item!=null&&item.Tag is IUICommand)
                    {
                        item.Hint = (item.Tag as IUICommand).Tooltip;
                    }
                }

            }catch(Exception ex)
            {
                //LogManager.Log.LogError(ex.ToString());
            }
        }

        /// <summary>
        /// 作用：设置当前选中的RibbonPage
        /// 作者：汪建龙
        /// 编写时间：2016年12月22日17:09:59
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool SetSelectPage(string name)
        {
            bool setted = false;
            try
            {
                if(WorkBench.RibbonControl!=null
                    &&WorkBench.RibbonControl.Created
                    &&WorkBench.RibbonControl.IsDisposed==false
                    &&WorkBench.RibbonControl.Pages!=null
                    && WorkBench.RibbonControl.Pages.Count > 0)
                {
                    foreach(RibbonPage ribbonPage in WorkBench.RibbonControl.Pages)
                    {
                        if (ribbonPage.Name.ToUpper() == name.ToUpper())
                        {
                            WorkBench.RibbonControl.SelectedPage = ribbonPage;
                            setted = true;
                            break;
                        }
                    }
                }

            }catch(Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
            }
            return setted;
        }
        /// <summary>
        /// 作用：设置状态栏显示的文本
        /// 作者：汪建龙
        /// 编写时间：2016年12月22日20:08:48
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetStatusBarValue(string name,string value)
        {
            bool setted = false;
            try
            {
                if (WorkBench.StatusBar != null && WorkBench.StatusBar.Created && WorkBench.StatusBar.IsDisposed == false)
                {
                    RibbonStatusBar statusBar = WorkBench.StatusBar;
                    for(var i = 0; i < statusBar.ItemLinks.Count; i++)
                    {
                        if (statusBar.ItemLinks[i].Item.Name.ToUpper() == name.ToUpper())
                        {
                            statusBar.ItemLinks[i].Item.Caption = value;
                            setted = true;
                        }
                    }
                }

                if (setted && WorkBench.RibbonControl != null && WorkBench.RibbonControl.Created && WorkBench.RibbonControl.IsDisposed == false)
                {
                    WorkBench.RibbonControl.StatusBar.Refresh();
                }


            }catch(Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
            }
            return setted;
        }
    }
}
