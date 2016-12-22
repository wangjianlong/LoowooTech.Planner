using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using LoowooTech.Planner.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.Planner.Controls
{
    public class SelectionEnv
    {
        /// <summary>
        /// 选择环境
        /// </summary>
        private ISelectionEnvironment _pSelectionEnvironment { get; set; }
        public ISelectionEnvironment SelectionEnvironment { get { return _pSelectionEnvironment; } }
        /// <summary>
        /// 选择后的颜色
        /// </summary>
        public IColor SelectColor { get { return _pSelectionEnvironment.DefaultColor; }set { _pSelectionEnvironment.DefaultColor = value; } }
        /// <summary>
        /// 选择方式：点、线、面等
        /// </summary>
        private esriGeometryType _pSelectGeometryType { get; set; }
        public esriGeometryType SelectGeometryType { get { return _pSelectGeometryType; }set { _pSelectGeometryType = value; } }
        /// <summary>
        /// 选择的交互方式
        /// </summary>
        private enumSelectType _pSelectType { get; set; }
        public enumSelectType SelectType { get { return _pSelectType; }set { _pSelectType = value; } }
        /// <summary>
        /// 是否可多图层选择
        /// </summary>
        private bool _canMultiLayerSelect { get; set; }
        public bool CanMultiLayerSelect { get { return _canMultiLayerSelect; }set { _canMultiLayerSelect = value; } }


        public SelectionEnv()
        {
            _pSelectionEnvironment = new SelectionEnvironmentClass();
            _pSelectGeometryType = esriGeometryType.esriGeometryEnvelope;
            _pSelectType = enumSelectType.SelectByIntersect;
            _canMultiLayerSelect = false;
        }

        /// <summary>
        /// 作用：设置选择厚要素的显示颜色
        /// </summary>
        /// <param name="Red"></param>
        /// <param name="Green"></param>
        /// <param name="Blue"></param>
        public void SetSelectColor(int Red,int Green,int Blue)
        {
            IRgbColor pRgbColor = new RgbColorClass();
            pRgbColor.Red = Red;
            pRgbColor.Green = Green;
            pRgbColor.Blue = Blue;
            _pSelectionEnvironment.DefaultColor = pRgbColor as IColor;
        }

        /// <summary>
        /// 作用：设置图层是否可选择
        /// 作者：汪建龙
        /// 编写时间：2016年12月22日19:39:16
        /// </summary>
        /// <param name="map"></param>
        /// <param name="layerName"></param>
        /// <param name="selectable"></param>
        public void SetFeatureLayerSelectable(IMap map,string layerName,bool selectable)
        {
            IFeatureLayer featureLayer = map.GetFeatureLayerByLayerName(layerName);
            if (featureLayer != null)
            {
                featureLayer.Selectable = selectable;
            }
        }
        /// <summary>
        /// 作用：设置所有图层可选择
        /// 作者：汪建龙
        /// 编写时间：2016年12月22日19:43:21
        /// </summary>
        /// <param name="map"></param>
        public void SetAllLayerSelectable(IMap map)
        {
            map.SetMapSelect(true);
        }
        /// <summary>
        /// 作用：设置所有图层不可选择
        /// 作者：汪建龙
        /// 编写时间：2016年12月22日19:44:24
        /// </summary>
        /// <param name="map"></param>
        public void ClearAllLayerSelectable(IMap map)
        {
            map.SetMapSelect(false);
        }
    }

    /// <summary>
    /// 自定义选择的交互方式：相交或全包含
    /// </summary>
    public enum enumSelectType
    {
        SelectByIntersect=0,
        SelectByContain=1
    }
}
