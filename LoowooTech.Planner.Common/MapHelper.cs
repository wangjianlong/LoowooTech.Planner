using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
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
        /// <summary>
        /// 作用：MapControl加载mxd文件
        /// 作者：汪建龙
        /// 编写时间：2016年12月26日09:49:25
        /// </summary>
        /// <param name="mapControl"></param>
        /// <param name="mxdFile"></param>
        public static void Load(this IMapControl2 mapControl,string mxdFile)
        {
            try
            {
                if (System.IO.File.Exists(mxdFile) == false)
                {
                    return;
                }

                IMapDocument mapDocument = new MapDocumentClass();
                if(!mapDocument.get_IsPresent(mxdFile)
                    ||!mapDocument.get_IsMapDocument(mxdFile)
                    ||mapDocument.get_IsPasswordProtected(mxdFile)
                    || mapDocument.get_IsRestricted(mxdFile))
                {
                    string message = string.Format("路径：\"{0}\"下的地图文档不正确，请拷贝文件到该路径下。", mxdFile);
                    System.Diagnostics.Trace.WriteLine(message);
                }
                else
                {
                    mapDocument.Open(mxdFile, null);
                    if (mapDocument.DocumentVersion == esriMapDocumentVersionInfo.esriMapDocumentVersionInfoFail)
                    {
                        string message = string.Format("路径：\"{0}\"下的地图文档版本不正确，请拷贝该文件到该路径下。", mxdFile);
                        System.Diagnostics.Trace.WriteLine(message);
                    }else if (mapDocument.MapCount > 0)
                    {
                        IMap map = mapDocument.get_Map(0);
                        mapControl.Map = map;
                    }
                }

            }catch(Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
            }
        }
        /// <summary>
        /// 作用：保存mxd文件
        /// 作者：汪建龙
        /// 编写时间：2016年12月27日18:13:46
        /// </summary>
        /// <param name="mapControl"></param>
        /// <param name="saveMXDFileName"></param>
        public static void Save(this IMapControl2 mapControl,string saveMXDFileName)
        {
            try
            {
                if (System.IO.File.Exists(saveMXDFileName))
                {
                    System.IO.File.Delete(saveMXDFileName);
                }
                if (System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(saveMXDFileName)) == false)
                {
                    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(saveMXDFileName));
                }

                IMapDocument mapDocument = new MapDocumentClass();
                mapDocument.New(saveMXDFileName);
                mapDocument.ReplaceContents(mapControl.Map as IMxdContents);
                mapDocument.Save(mapDocument.UsesRelativePaths, true);
                
            }catch(Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
            }
        }
    }
}
