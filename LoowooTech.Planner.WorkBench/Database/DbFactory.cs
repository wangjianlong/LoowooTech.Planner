using LoowooTech.Planner.Common.Utility;
using LoowooTech.Planner.WorkBench.Database.SQL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.Planner.WorkBench.Database
{
    public class DbFactory
    {
        private static DbFactory _Instance { get; set; }
        public static DbFactory Instance { get { return _Instance; } }

        private Hashtable _DbHelpList { get; set; }
        private Hashtable DbHelpList { get { return _DbHelpList; } }
        private DataSource[] _dataSources { get; set; }

        static DbFactory()
        {
            _Instance = new DbFactory();
        }
        private DbFactory()
        {
            _DbHelpList = new Hashtable();
            LoadDataSource();
        }

        public void LoadDataSource()
        {
            ApplicationAssert.CheckCondition(ApplicationConfiguration.DataSourceConfigFile != null, "DataSourceConfigFile参数没有设置！", ApplicationAssert.LineNumber);
            _dataSources = DataSource.GetAllDataSources(ApplicationConfiguration.DataSourceConfigFile);
        }

        public IDbHelp GetDbHelp()
        {
            ApplicationAssert.DebugCheck(ApplicationConfiguration.DefaultDataSourceName != null, "DefaultDataSourceName参数没有设置！", ApplicationAssert.LineNumber);
            return  GetDbHelp(ApplicationConfiguration.DefaultDataSourceName);
        }

        public IDbHelp CreateDbHelp(string dataSourceName)
        {
            DataSource[] dataSources = _dataSources;
            IDbHelp result = null;
            for(var i = 0; i < dataSources.Length; i++)
            {
                DataSource dataSource = dataSources[i];
                if (dataSource.Name == dataSourceName)
                {

                }
            }
            return result;
        }

        public IDbHelp GetDbHelp(string dataSourceName)
        {
            return CreateDbHelp(dataSourceName);
        }

        public IDbHelp CreateDbHelp(DataSource dataSource)
        {
            IDbHelp result = null;
            if (dataSource.DbHelpType == Models.DbHelpClassTypes.SqlDbHelp)
            {
                result = new SqlDbHelp(dataSource);
            }
            else
            {
               
            }
            return result;
        }

        

    }
}
