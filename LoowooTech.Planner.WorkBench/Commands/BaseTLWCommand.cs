using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.Planner.WorkBench.Commands
{
    /// <summary>
    /// 基础类
    /// </summary>
    public abstract class BaseTLWCommand:ITLWCommand
    {
        public abstract void OnClick();

        public virtual bool Enable { get { return true; } }

        protected string _Para { get; set; }

        public virtual void Init(string para)
        {
            _Para = para;
        }

        protected object _Hook { get; set; }
        public virtual void SetHook(object obj)
        {
            _Hook = obj;
        }

        public virtual string Message { get { return string.Empty; } }
        public virtual string Tooltip { get { return string.Empty; } }
        public virtual bool Visible { get { return true; } }
        public virtual bool Checked { get { return false; } }

    }
}
