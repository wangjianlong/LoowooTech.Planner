using LoowooTech.Planner.WorkBench.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LoowooTech.Planner.WorkBench
{
    public static class InstanceHelper
    {
        /// <summary>
        /// 作用：实例化
        /// 对象：汪建龙
        /// 编写时间：2016年12月20日15:38:39
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public static object CreateInstance(string assemblyName,string className)
        {
            try
            {
                Assembly ass = Assembly.Load(assemblyName);
                Type type = ass.GetType(className);
                object obj = Activator.CreateInstance(type);
                return obj;

            }catch(Exception ex)
            {
                var msg = string.Format("在程序集{0}中创建类{1}的实例失败，详细信息：{2}", assemblyName, className, ex.Message);
                System.Diagnostics.Trace.WriteLine(msg, "创建实例");
                //LogManager.Log.LogError(msg);
               // LogManager.Log.LogError(ex.ToString());
                return null;
            }
        }
    }
}
