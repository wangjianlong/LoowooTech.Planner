using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.Planner.WorkBench.Database
{
    public interface IDbHelp
    {
        DataSource DataSourceInfo
        {
            get;
            set;
        }
    }
}
