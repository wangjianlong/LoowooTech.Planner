using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.Planner.Common
{
    public static class PubishFunction
    {
        /// <summary>
        /// 系统图层数据是否在系统自动时自动加载标识，true加载  false不加载
        /// </summary>
        public static bool LoadDataState
        {
            get
            {
                try
                {
                    XmlHelper.Load(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config\\defaultConfig.xml"));
                    string sLoadDataState = XmlHelper.GetValue(@"/Config/System/LoadDataState");
                    return sLoadDataState == "T";
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
