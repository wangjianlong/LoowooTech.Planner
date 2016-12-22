using ESRI.ArcGIS.Carto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.Planner.WorkBench.Database
{
    /// <summary>
    /// 作用：地图操作类
    /// 作者：汪建龙
    /// 编写时间：2016年12月22日16:54:40
    /// </summary>
    public class MapOperator
    {
        private IMap _map { get; set; }
        public IMap Map { get { return _map; }set { _map = value; } }

    }
}
