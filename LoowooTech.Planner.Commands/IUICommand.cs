using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.Planner.Commands
{
    /// <summary>
    /// 绑定在界面上的按钮
    /// </summary>
    public interface IUICommand
    {
        /// <summary>
        /// 获取或设置类名（全名）
        /// </summary>
        string ClassName { get; set; }
        /// <summary>
        /// 获取或设置程序集名称
        /// </summary>
        string AssemblyName { get; set; }
        /// <summary>
        /// 获取或设置名称
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 单击按钮时触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e);

        /// <summary>
        /// 初始化的参数
        /// </summary>
        string Parameter { get; set; }
        /// <summary>
        /// Hook
        /// </summary>
        object Hooker { get; set; }

        /// <summary>
        /// 获取按钮是否可用
        /// </summary>
        bool Enable { get; }
        /// <summary>
        /// 获取或设置按钮是否可见
        /// </summary>
        bool Visible { get; set; }
        /// <summary>
        /// 获取界面展示上按钮是否处于选中状态
        /// </summary>
        bool Checked { get; }
        /// <summary>
        /// 获取或设置界面展示上是否分组
        /// </summary>
        bool Group { get; set; }
        /// <summary>
        /// 获取Message（鼠标停留在按钮上时状态栏显示的文本）
        /// </summary>
        string Message { get; }
        /// <summary>
        /// 获取Tooltip（鼠标停留在按钮上时鼠标附近显示的文本）
        /// </summary>
        string Tooltip { get; }

        /// <summary>
        /// BarButtonItem
        /// </summary>
        BarButtonItem BarButtonItem { get; set; }
        /// <summary>
        /// ICommand或ITLWCommand
        /// </summary>
        object Command { get; }
    }
}
