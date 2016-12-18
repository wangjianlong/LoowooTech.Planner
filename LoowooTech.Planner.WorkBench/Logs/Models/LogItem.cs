using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.Planner.WorkBench.Logs.Models
{
    /// <summary>
    /// 日志项
    /// </summary>
    internal class LogItem
    {
        #region  属性
        private int _logID { get; set; }
        /// <summary>
        /// 日志ID
        /// </summary>
        public int LogID { get { return _logID; }set { _logID = value; } }
        private DateTime _date { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime Date { get { return _date; }set { _date = value; } }
        private string _message { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get { return _message; }set { _message = value; } }
        private string _exception { get; set; }
        /// <summary>
        /// 异常
        /// </summary>
        public string Exception { get { return _exception; }set { _exception = value; } }
        private LogCategoryType _category { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        public LogCategoryType Category { get { return _category; }set { _category = value; } }
        private string _name { get; set; }
        /// <summary>
        /// 日志项名称
        /// </summary>
        public string Name { get { return _name; } set { _name = value; } }
        private string _userName { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get { return _userName; }set { _userName = value; } }

        private LogCryptographyActionType _cryptographyAction { get; set; }
    
        public LogCryptographyActionType CryptographyAction { get { return _cryptographyAction; }set { _cryptographyAction = value; } }

        #endregion


        #region 构造函数
        public LogItem()
        {
            _logID = -1;
            _date = DateTime.MaxValue;
            _message = string.Empty;
            _exception = string.Empty;
            _category = LogCategoryType.Unknown;
            _name = string.Empty;
            _userName = string.Empty;
        }
        public LogItem(int logID, DateTime date, string message, string exception, LogCategoryType category, string name, string userName)
        {
            _logID = logID;
            _date = date;
            _message = message;
            _exception = exception;
            _category = category;
            _name = name;
            _userName = userName;
        }

        #endregion


        #region  方法

        /// <summary>
        /// CategoryType转为字符串
        /// </summary>
        /// <param name="operationCategoryType"></param>
        /// <returns></returns>
        public static string ToCategoryString(LogCategoryType operationCategoryType)
        {
            string strCat = string.Empty;
            switch (operationCategoryType)
            {
                case LogCategoryType.EditData:
                    strCat = "数据编辑";
                    break;
                case LogCategoryType.OtherOperation:
                    strCat = "其他操作";
                    break;
                case LogCategoryType.Login:
                    strCat = "用户登录";
                    break;
                case LogCategoryType.Error:
                    strCat = "错误";
                    break;
            }
            return strCat;
        }

        /// <summary>
        /// 字符串转为CategoryType
        /// </summary>
        /// <param name="categoryString"></param>
        /// <returns></returns>
        public static LogCategoryType FromCategoryString(string categoryString)
        {
            if (string.IsNullOrEmpty(categoryString))
                return LogCategoryType.Unknown;
            switch (categoryString)
            {
                case "数据编辑":
                    return LogCategoryType.EditData;
                case "其他操作":
                    return LogCategoryType.OtherOperation;
                case "用户登录":
                    return LogCategoryType.Login;
                case "错误":
                    return LogCategoryType.Error;
                default:
                    return LogCategoryType.Unknown;
            }
        }

        public override string ToString()
        {
            return _message ?? string.Empty;
        }
        #endregion
    }

    /// <summary>
    /// 操作类型
    /// </summary>
    [Flags]
    internal enum LogCategoryType
    {
        EditData = 1,       //数据编辑
        OtherOperation = 2,     //其他的操作
        Login = 4,          //登录
        Error = 8,          //错误
        Unknown = 0,         //未知类型
        All = 15            //所有的类型
    }

    internal enum LogCryptographyActionType
    {
        Encryption, //加密
        Decryption, //解密
        None        //不加解密
    }
}
