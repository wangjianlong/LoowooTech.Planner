using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.Planner.Controls
{
    /// <summary>
    /// 作用：创建唯一的选择环境实例
    /// 作者：汪建龙
    /// 编写时间：2016年12月22日19:49:56
    /// </summary>
    public class MapControlSelectionEnv:SelectionEnv
    {
        private static MapControlSelectionEnv mapControlSelectionEnv { get; set; }
        private static object myObject { get; set; }

        static MapControlSelectionEnv()
        {
            myObject = new object();
        }
        public MapControlSelectionEnv()
        {
            CanMultiLayerSelect = true;
        }
        /// <summary>
        /// 作用：静态方法 得到当前唯一的实例
        /// 作者：汪建龙
        /// 编写时间：2016年12月22日19:49:21
        /// </summary>
        /// <returns></returns>
        public static MapControlSelectionEnv Instance()
        {
            lock (myObject)
            {
                if (mapControlSelectionEnv == null)
                {
                    mapControlSelectionEnv = new MapControlSelectionEnv();
                    mapControlSelectionEnv.CanMultiLayerSelect = true;//针对SDE数据
                }
            }

            return mapControlSelectionEnv;
        }



    }
}
