using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.Planner.WorkBench.Commands
{
    /// <summary>
    /// 系统编码时实现的按钮
    /// </summary>
    public interface ITLWCommand
    {
        /// <summary>
        /// 单击时触发的事件
        /// </summary>
        void OnClick();
        /// <summary>
        /// 获取按钮是否可用
        /// </summary>
        bool Enable { get; }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="para"></param>
        void Init(string para);
        /// <summary>
        /// 设置Hook
        /// </summary>
        /// <param name="obj"></param>
        void SetHook(object obj);
        /// <summary>
        /// 获取Message（鼠标停留在按钮上时状态栏显示的文本）
        /// </summary>
        string Message { get; }
        /// <summary>
        /// 获取Tooltip（鼠标停留在按钮上时鼠标附近显示的文本）
        /// </summary>
        string Tooltip { get; }
        /// <summary>
        /// 获取按钮是否可见
        /// </summary>
        bool Visible { get; }
        /// <summary>
        /// 获取界面展示上按钮是否处于选中状态
        /// </summary>
        bool Checked { get; }

    }
}
