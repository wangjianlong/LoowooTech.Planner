using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.Planner.Common.Utility
{
    public class ExceptionHandle
    {
        /// <summary>
        /// 作用：Throw 错误 并将错误信息记录到日志中
        /// 作者：汪建龙
        /// 编写时间：2016年12月18日15:04:14
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <param name="debugInfo"></param>
        public static void Throw(string message,Exception ex,string debugInfo)
        {
            string text = ApplicationLog.FormatException(ex,message);
            ApplicationLog.WriteError(text + "\n" + debugInfo);
            throw new Exception(text);
        }


        public static void ThrowUserError(string errorText)
        {
            throw new ApplicationException(errorText);
        }

        public static void Throw(string message,Exception ex)
        {
            Throw(message, ex, true);
        }

        public static void Throw(string message,Exception ex,bool throwNewException)
        {
            if(ex is ApplicationException)
            {
                ThrowUserError(string.Format("{0}\n({1})", message, ex.Message));
                return;
            }

            string message2 = ApplicationLog.FormatException(ex, message);
            ApplicationLog.WriteError(message2);
            if (!throwNewException)
            {
                throw ex;
            }
            if (ex != null)
            {
                throw new Exception(string.Format("{0}\n({1})", message, ex.Message));
            }

            throw new Exception(message);
        }
    }
}
