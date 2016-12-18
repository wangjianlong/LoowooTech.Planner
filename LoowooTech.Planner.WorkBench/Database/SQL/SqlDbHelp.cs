using LoowooTech.Planner.Common.Utility;
using LoowooTech.Planner.WorkBench.Database.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace LoowooTech.Planner.WorkBench.Database.SQL
{
    public class SqlDbHelp:IDbHelp
    {
        #region  属性
        private DataSource _DataSourceInfo { get; set; }
        /// <summary>
        /// 数据库连接信息
        /// </summary>
        public DataSource DataSourceInfo { get { return _DataSourceInfo; }set { _DataSourceInfo = value; if (_Connection != null) { _Connection.ConnectionString = _DataSourceInfo.ConnectionString; } } }
        private SqlConnection _Connection { get; set; }
        private SqlTransaction _Transaction { get; set; }
        private bool _ConnectionIsOpen { get; set; }
        /// <summary>
        /// 数据库是否连接
        /// </summary>
        public bool ConnectionIsOpen { get { return _ConnectionIsOpen; } }
        private bool _TransactionIsOpen { get; set; }
        public bool TransactionIsOpen { get { return _TransactionIsOpen; } }
        private bool _TransactionIsRolledBack { get; set; }
        public bool TransactionIsRollBack { get { return _TransactionIsRolledBack; } }
        private bool _TransactionIsCommitted { get; set; }
        public bool TransactionIsCommitted { get { return _TransactionIsCommitted; } }
        private ConnectionState _OldConnectionState { get; set; }

        public string SqlParamPrefix { get { return "@"; } }

        #endregion


        #region  构造函数
        public SqlDbHelp()
        {
            _Connection = new SqlConnection();
        }

        public SqlDbHelp(DataSource dataSourceInfo)
        {
            _DataSourceInfo = dataSourceInfo;
            _Connection = new SqlConnection(dataSourceInfo.ConnectionString);
        }

        public SqlDbHelp(SqlConnection connection)
        {
            if (connection == null)
            {
                throw new Exception("参数错误！");
            }

            _Connection = connection;
        }

        #endregion

        /// <summary>
        /// 作用：打开数据库连接
        /// 作者：汪建龙
        /// 编写时间：2016年12月18日16:12:40
        /// </summary>
        public void OpenConnection()
        {
            try
            {
                _Connection.Open();
                _ConnectionIsOpen = true;

            } catch(Exception ex)
            {
                _ConnectionIsOpen = false;
                if (_Connection != null)
                {
                    ExceptionHandle.Throw("打开数据库连接错误，请确认数据库以打开并连接正确！", ex, string.Format("数据库连接字符串={0}", _Connection.ConnectionString));
                }
                else
                {
                    ExceptionHandle.Throw("打开数据库连接错误，连接对象未创建.", ex);
                }
            }
        }

        /// <summary>
        /// 作用：关闭数据库连接
        /// 作者：汪建龙
        /// 编写时间：2016年12月18日16:13:21
        /// </summary>
        public void CloseConnection()
        {
            _Connection.Close();
            _ConnectionIsOpen = false;
        }
        /// <summary>
        /// 作用：确保打开数据库连接
        /// 作者：汪建龙
        /// 编写时间：2016年12月18日16:14:27
        /// </summary>
        public void CheckOpenConnection()
        {
            _OldConnectionState = _Connection.State;
            if (_OldConnectionState == ConnectionState.Closed)
            {
                OpenConnection();
            }
        }
        /// <summary>
        /// 作用：确保数据库关闭连接
        /// 作者：汪建龙
        /// 编写时间：2016年12月18日16:18:01
        /// </summary>
        public void CheckCloseConnection()
        {
            if (_OldConnectionState == ConnectionState.Closed)
            {
                CloseConnection();
            }
        }
        /// <summary>
        /// 作用：开始事务
        /// 作者：汪建龙
        /// 编写时间：2016年12月18日16:23:46
        /// </summary>
        public void BeginTranscation()
        {
            CheckOpenConnection();
            _Transaction = _Connection.BeginTransaction();
            _TransactionIsOpen = true;
            _TransactionIsCommitted = false;
            _TransactionIsRolledBack = false;
        }

        public void CommitTransaction()
        {
            if (_Transaction != null)
            {
                try
                {
                    _Transaction.Commit();
                    _Transaction = null;
                    _TransactionIsCommitted = true;
                    _TransactionIsOpen = false;
                }
                finally
                {
                    CheckCloseConnection();
                }
            }
        }

        public void RollbackTransaction()
        {
            if (_Transaction != null)
            {
                try
                {
                    _Transaction.Rollback();
                    _Transaction = null;
                    _TransactionIsRolledBack = true;
                    _TransactionIsOpen = false;
                }
                finally
                {
                    CheckCloseConnection();
                }
            }
        }

        public IDataParameter[] CreateParameters(int length)
        {
            return new SqlParameter[length];
        }

        public IDataParameter CreateParameter()
        {
            return new SqlParameter();
        }
        public IDataParameter CreateParameter(string name,ColumnType dataType)
        {
            return new SqlParameter(name, GetSqlDbType(dataType));
        }
        public IDataParameter CreateParameter(string name,ColumnType dataType,int size)
        {
            return new SqlParameter(name, GetSqlDbType(dataType), size);
        }

        public IDataParameter CreateParameter(string name, ColumnType dbType, int size, ParameterDirection direction, bool isNullable, int precision, int scale, string srcColumn, DataRowVersion srcVersion, object value)
        {
            return new SqlParameter(name, this.GetSqlDbType(dbType), size, ParameterDirection.Input, isNullable, (byte)precision, (byte)scale, srcColumn, srcVersion, value);
        }

        public IDataParameter CreateParameter(string name, ColumnType dbType, int size, string srcColumn)
        {
            return new SqlParameter(name, this.GetSqlDbType(dbType), size, srcColumn);
        }

        public IDataParameter CreateParameter(string name, object obj)
        {
            return new SqlParameter(name, obj);
        }

        public IDataParameter CreateParameter(string name, object obj, ColumnType dataType)
        {
            IDataParameter dataParameter = this.CreateParameter(name, dataType);
            dataParameter.Value = obj;
            return dataParameter;
        }

        public IDataParameter CreateParameter(string name, object obj, ColumnType dataType, int size)
        {
            IDataParameter dataParameter = this.CreateParameter(name, dataType, size);
            dataParameter.Value = obj;
            return dataParameter;
        }

        private SqlDbType GetSqlDbType(ColumnType dataType)
        {
            SqlDbType result;
            switch (dataType)
            {
                case ColumnType.VarNumeric:
                    result = SqlDbType.Decimal;
                    return result;
                case ColumnType.Binary:
                    result = SqlDbType.Binary;
                    return result;
                case ColumnType.LongVarBinary:
                    result = SqlDbType.Binary;
                    return result;
                case ColumnType.Date:
                    result = SqlDbType.DateTime;
                    return result;
                case ColumnType.DateTime:
                    result = SqlDbType.DateTime;
                    return result;
                case ColumnType.Char:
                    result = SqlDbType.Char;
                    return result;
                case ColumnType.VarChar:
                    result = SqlDbType.VarChar;
                    return result;
                case ColumnType.Text:
                    result = SqlDbType.Text;
                    return result;
                case ColumnType.LongVarChar:
                    result = SqlDbType.Text;
                    return result;
                case ColumnType.Blob:
                    result = SqlDbType.Binary;
                    return result;
            }
            result = SqlDbType.VarChar;
            return result;
        }
    }
}
