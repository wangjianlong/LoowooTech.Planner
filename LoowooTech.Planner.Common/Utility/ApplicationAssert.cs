using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace LoowooTech.Planner.Common.Utility
{
    public class ApplicationAssert
    {
        /// <summary>
        /// StraceTrace所在的行号
        /// </summary>
        public static int LineNumber
        {
            get
            {
                int result = 0;
                try
                {
                    result = new StackTrace(1, true).GetFrame(0).GetFileLineNumber();
                    return result;
                }
                catch
                {

                }
                return result;
            }
        }
        /// <summary>
        /// 作用：验证condition 并记录errorText
        /// 作用：汪建龙
        /// 编写时间：2016年12月17日18:39:56
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="errorText"></param>
        /// <param name="lineNumber"></param>
        public static void CheckCondition(bool condition,string errorText,int lineNumber)
        {
            if (!condition)
            {
                string empty = string.Empty;
                GenerateStackTrace(lineNumber, ref empty);
                throw new ApplicationException(errorText);

            }
        }
        [Conditional("DEBUG")]
        private static void GenerateStackTrace(int lineNumber,ref string currentTrace)
        {
            currentTrace = string.Empty;
            StreamReader streamReader = null;
            bool flag = false;
            StringBuilder stringBuilder = new StringBuilder();
            StackTrace stackTrace = new StackTrace(2, true);
            try
            {
                StackFrame frame = stackTrace.GetFrame(0);
                string fileName = string.Empty;
                int num = 0;
                if ((fileName = frame.GetFileName()) != string.Empty && (num = (lineNumber != 0 ? lineNumber : frame.GetFileLineNumber())) >= 0)
                {
                    stringBuilder.Append(fileName).Append(",Line:").Append(num);
                    streamReader = new StreamReader(fileName);
                    flag = true;
                    string text;
                    do
                    {
                        text = streamReader.ReadLine();
                        num--;

                    } while (num != 0);
                    stringBuilder.Append("\r\n");
                    if (lineNumber != 0)
                    {
                        stringBuilder.Append("Current executable line:");
                    }
                    else
                    {
                        stringBuilder.Append("\r\n").Append("Next executable line:");
                    }
                    stringBuilder.Append("\r\n").Append(text.Trim());
                }
            }
            catch
            {

            }
            finally
            {
                if (flag)
                {
                    streamReader.Close();
                }
            }
            currentTrace = stringBuilder.ToString();
        }
        [Conditional("DEBUG")]
        public static void DebugCheck(bool condition,string errorText,int lineNumber)
        {
            if (!condition)
            {
                string empty = string.Empty;
                GenerateStackTrace(lineNumber, ref empty);
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("Assert: ").Append("\r\n").Append(errorText).Append("\r\n").Append(empty);
                ApplicationLog.WriteWarning(stringBuilder.ToString());
            }
        }
    }
}
