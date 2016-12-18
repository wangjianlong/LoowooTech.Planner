using LoowooTech.Planner.Common.Utility;
using LoowooTech.Planner.WorkBench.Database.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace LoowooTech.Planner.WorkBench.Database
{
    [Serializable]
    public class DataSource
    {
        private string _Name { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get { return _Name; }set { _Name = value; } }
        private DataSourceTypes _SourceType { get; set; }
        /// <summary>
        /// 数据库类型
        /// </summary>
        public DataSourceTypes SourceType { get { return _SourceType; }set { _SourceType = value; } }
        private DbHelpClassTypes _DbHelpType { get; set; }
        /// <summary>
        /// 数据库Help
        /// </summary>
        public DbHelpClassTypes DbHelpType { get { return _DbHelpType; }set { _DbHelpType = value; } }
        private string _ConnectionString { get; set; }
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString
        {
            get
            {
                string result = string.Empty;
                if (this._SourceType == DataSourceTypes.ACCESS)
                {
                    result = string.Format(_ConnectionString, AppDomain.CurrentDomain.BaseDirectory);
                }
                else
                {
                    result = _ConnectionString;
                }
                return result;
            }
            set
            {
                _ConnectionString = value;
            }
        }

        private StringCollection _Attributes { get; set; }
        public StringCollection Attributes { get { return _Attributes; }set { _Attributes = value; } }

        public DataSource()
        {

        }

        public DataSource(string name,DataSourceTypes sourceType,DbHelpClassTypes dbHelpType,string connectionString)
        {
            _Name = name;
            _SourceType = sourceType;
            _DbHelpType = dbHelpType;
            _ConnectionString = connectionString;
            _Attributes = new StringCollection();
        }
        /// <summary>
        /// 作用：获取所有数据库连接信息
        /// 作者：汪建龙
        /// 编写时间：2016年12月17日18:36:10
        /// </summary>
        /// <param name="configFile"></param>
        /// <returns></returns>
        public static DataSource[] GetAllDataSources(string configFile)
        {
            DataSource[] array = Serializer.LoadObjectFromFile(typeof(DataSource[]), configFile) as DataSource[];
            //DataSource[] array2 = array;
            //for(var i = 0; i < array2.Length; i++)
            //{
            //    DataSource datasource = array2[i];
            //    if (datasource.ConnectionString.ToUpper().IndexOf("MDB") > 0)
            //    {

            //    }
            //}

            return array;
        }



    }
}
