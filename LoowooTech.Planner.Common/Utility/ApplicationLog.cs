using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace LoowooTech.Planner.Common.Utility
{
    public class ApplicationLog
    {
        /// <summary>
        /// 作用：写入错误日志
        /// 作者：汪建龙
        /// 编写时间：2016年12月17日19:43:01
        /// </summary>
        /// <param name="message"></param>
        public static void WriteError(string message)
        {
            WriteLog(TraceLevel.Error, message);
        }
        /// <summary>
        /// 作用：写入提示信息日志
        /// 作者：汪建龙
        /// 编写时间：2016年12月17日19:46:49
        /// </summary>
        /// <param name="message"></param>
        public static void WriteWarning(string message)
        {
            WriteLog(TraceLevel.Warning, message);
        }
        /// <summary>
        /// 作用：记录信息日志
        /// 作者：汪建龙
        /// 编写时间：2016年12月17日19:47:41
        /// </summary>
        /// <param name="message"></param>
        public static void WriteInfo(string message)
        {
            WriteLog(TraceLevel.Info, message);
        }
        /// <summary>
        /// 作用：写入Trace
        /// 作者：汪建龙
        /// 编写时间：2016年12月17日19:48:13
        /// </summary>
        /// <param name="message"></param>
        public static void WriteTrace(string message)
        {
            WriteLog(TraceLevel.Verbose, message);
        }
        /// <summary>
        /// 作用：打开文件 并返回StreamWriter对象
        /// 作者：汪建龙
        /// 编写时间：2016年12月17日19:27:05
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static StreamWriter GetStreamWriter(string path,string fileName)
        {
            string path2 = path + fileName;
            StreamWriter result = null;
            try
            {
                result = File.AppendText(path2);
            }
            catch
            {
                path2 = string.Format("{0}{1}{2}.{3}", new object[] {
                    path,
                    Path.GetFileNameWithoutExtension(fileName),
                    Path.GetFileName(Path.GetTempFileName()),
                    Path.GetExtension(fileName)
                });
                result = File.AppendText(path2);
            }

            return result;
        }
        /// <summary>
        /// 作用：返回日志目录
        /// 作者：汪建龙
        /// 编写时间：2016年12月17日19:40:24
        /// </summary>
        /// <returns></returns>
        private static string GetLogPath()
        {
            string text = AppDomain.CurrentDomain.BaseDirectory;
            text = string.Format("{0}\\Log\\{1}\\", text, DateTime.Now.ToString("yyyy-MM-dd"));
            if (!Directory.Exists(text))
            {
                Directory.CreateDirectory(text);
            }
            return text;
        }
        /// <summary>
        /// 作用：将信息写入日志文件钟
        /// 作者：汪建龙
        /// 编写时间：2016年12月17日19:42:21
        /// </summary>
        /// <param name="level"></param>
        /// <param name="messageText"></param>
        private static void WriteLog(TraceLevel level,string messageText)
        {
            if (level <= ApplicationConfiguration.TracingTraceLevel)
            {
                string value = string.Format("{0:yyyy-MM-dd HH:mm:ss fff}:{1}。{2}", DateTime.Now, messageText, Environment.NewLine);
                try
                {
                    lock (typeof(ApplicationLog))
                    {
                        StreamWriter streamWriter = GetStreamWriter(GetLogPath(), "LoowooTechTrace.txt");
                        try
                        {
                            streamWriter.WriteLine(value);
                            streamWriter.Flush();
                        }
                        finally
                        {
                            streamWriter.Close();
                        }
                    }
                }
                catch
                {

                }
            }
        }
        /// <summary>
        /// 作用：将Exception转换成string
        /// 作者：汪建龙
        /// 编写时间：2016年12月18日15:02:31
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="catchInfo"></param>
        /// <returns></returns>
        public static string FormatException(Exception ex,string catchInfo)
        {
            StringBuilder stringBuilder = new StringBuilder(catchInfo);
            if (ex != null)
            {
                stringBuilder.Append("详细错误信息：\r\n").Append(ex.Message).Append("\r\n").Append(ex.StackTrace);
            }
            return stringBuilder.ToString();
        }
    }
}
