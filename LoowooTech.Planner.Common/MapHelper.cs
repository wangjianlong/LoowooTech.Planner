using ESRI.ArcGIS.Carto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.Planner.Common
{
    public static class MapHelper
    {
        /// <summary>
        /// 作用：根据图层名称获取IFeatureLayer
        /// 作者：汪建龙
        /// 编写时间：2016年12月22日19:37:25
        /// </summary>
        /// <param name="map"></param>
        /// <param name="layerName"></param>
        /// <returns></returns>
        public static IFeatureLayer GetFeatureLayerByLayerName(this IMap map,string layerName)
        {
            IFeatureLayer currentfeatureLayer = null;
            if (map != null)
            {
                IEnumLayer layers = map.get_Layers(null, true);
                ILayer layer = layers.Next();
                while (layer != null)
                {
                    IFeatureLayer featureLayer = layer as IFeatureLayer;
                    if (featureLayer != null && featureLayer.Valid && featureLayer.FeatureClass != null && featureLayer.Name == layerName)
                    {
                        currentfeatureLayer = featureLayer;
                        break;
                    }
                }
            }
            return currentfeatureLayer;
        }
        
        /// <summary>
        /// 作用：设置map地图所有图层是否可选
        /// 作者：汪建龙
        /// 编写时间：2016年12月22日19:42:09
        /// </summary>
        /// <param name="map"></param>
        /// <param name="selectable"></param>
        public static void SetMapSelect(this IMap map,bool selectable)
        {
            ILayer layer = null;
            IFeatureLayer featureLayer = null;
            for(var i = 0; i <= map.LayerCount; i++)
            {
                layer = map.get_Layer(i);
                if(layer is IFeatureLayer)
                {
                    featureLayer = layer as IFeatureLayer;
                    featureLayer.Selectable = selectable;
                }
            }
        }

    }
}
