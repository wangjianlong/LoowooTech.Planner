using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Ribbon.ViewInfo;
using LoowooTech.Planner.Commands;
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
                LogManager.Log.LogError(ex.ToString());
            }
        }

    }
}
