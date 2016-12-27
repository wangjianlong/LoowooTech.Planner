using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace LoowooTech.Planner.Common
{
    public class MapDataLoader
    {
        private string _templateMxdFileName { get; set; }
        /// <summary>
        /// 模板Mxd
        /// </summary>
        public string TemplateMxdFileName
        {
            set { _templateMxdFileName = value; }
            get
            {
                if (string.IsNullOrEmpty(_templateMxdFileName))
                {
                    string fileName = string.Empty;
                    fileName = System.Windows.Forms.Application.StartupPath;
                    fileName = System.IO.Path.Combine(fileName, "Config");
                    fileName = System.IO.Path.Combine(fileName, "TemplateMxd.mxd");
                    return fileName;
                }
                return _templateMxdFileName;
            }
        }

        private string _configFileName { get; set; }
        /// <summary>
        /// 配置文件路径
        /// </summary>
        public string ConfigFileName
        {
            set { _configFileName = value; }
            get
            {
                if (string.IsNullOrEmpty(_configFileName))
                {
                    string fileName = System.Windows.Forms.Application.StartupPath;
                    fileName = System.IO.Path.Combine(fileName, "Config", "MapDataLoadConfig.xml");
                    return fileName;
                }
                return _configFileName;
            }
        }

        private string _systemMxdFileName { get; set; }
        /// <summary>
        /// 系统Mxd文件路径
        /// </summary>
        public string SystemMxdFileName
        {
            get
            {
                if (string.IsNullOrEmpty(_systemMxdFileName))
                {
                    string fileName = System.Windows.Forms.Application.StartupPath;
                    fileName = System.IO.Path.Combine(fileName, "Config", "MapDocument.mxd");
                    return fileName;
                }
                return _systemMxdFileName;
            }
            set
            {
                _systemMxdFileName = value;
            }
        }

        private bool _directlyLoadedSystemMxdifExist = true;


        /// <summary>
        /// 作用：打开TemplateMapDocument
        /// 作者：汪建龙
        /// 编写时间：2016年12月23日11:18:34
        /// </summary>
        /// <returns></returns>
        private IMapDocument OpenTemplateDocument()
        {
            try
            {
                IMapDocument tempMapDocumnet = new MapDocumentClass();
                if (!tempMapDocumnet.get_IsPresent(TemplateMxdFileName) 
                    || !tempMapDocumnet.get_IsMapDocument(TemplateMxdFileName) 
                    || tempMapDocumnet.get_IsPasswordProtected(TemplateMxdFileName) 
                    || tempMapDocumnet.get_IsRestricted(TemplateMxdFileName))
                {
                    var message = string.Format("路径：\"{0}\"下的地图文档不正确，请拷贝该文件到该路径下",_templateMxdFileName);
                    System.Diagnostics.Trace.WriteLine(message);
                    return null;
                }

                tempMapDocumnet.Open(TemplateMxdFileName, null);

                if (tempMapDocumnet.DocumentVersion == esriMapDocumentVersionInfo.esriMapDocumentVersionInfoFail)
                {
                    var message = string.Format("路径：\"{0}\"下的地图文档版本不正确，请拷贝该文件到该路径下", TemplateMxdFileName);
                    return null;
                }

                return tempMapDocumnet;
            }catch(Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
                return null;
            }
        }

        /// <summary>
        /// 作用：加载地图文件
        /// 作者：汪建龙
        /// 编写时间：2016年12月27日14:10:11
        /// </summary>
        /// <param name="mapControl"></param>
        public void　Load(IMapControl2 mapControl)
        {
            try
            {

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(ConfigFileName);

                XmlNode noderoot = xmlDocument.SelectSingleNode("/MapDataLoadConfig");
                if (noderoot == null)
                {
                    return;
                }
                if (string.IsNullOrEmpty(_templateMxdFileName))
                {
                    _templateMxdFileName = PlannerPathHelp.GetFilePath(XmlHelper.GetAttributeValue(noderoot, "TemplateMXDFile"));
                }
                if (string.IsNullOrEmpty(_systemMxdFileName))
                {
                    _systemMxdFileName = PlannerPathHelp.GetFilePath(XmlHelper.GetAttributeValue(noderoot, "SystemMXDFile"));
                }
                var temp = XmlHelper.GetAttributeValue(noderoot, "DirectlyLoadedSystemMXDIfExist");
                if (!string.IsNullOrEmpty(temp))
                {
                    _directlyLoadedSystemMxdifExist = temp.ToUpper().IndexOf("F") > -1 ? false : true;
                }

                if (_directlyLoadedSystemMxdifExist && System.IO.File.Exists(_systemMxdFileName))
                {
                    mapControl.Load(_systemMxdFileName);
                }
                else
                {
                   // mapControl.Load(xmlDocument);
                }


            }catch(Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
            }
        }

        private void Load(IMapControl2 mapControl,XmlDocument xmlDocument)
        {
            try
            {
                XmlNode nodeMap = xmlDocument.SelectSingleNode("/MapDataLoadConfig/Map");
                if (nodeMap == null)
                {
                    return;
                }
                IMapDocument templateDocument = OpenTemplateDocument();
                IMap templateMap = templateDocument.get_Map(0);
                IMap map = new MapClass();
                if (XmlHelper.GetAttributeValue(nodeMap, "Name") != string.Empty)
                {
                    map.Name = XmlHelper.GetAttributeValue(nodeMap, "Name");
                }

                XmlNodeList nodeListLayers = nodeMap.ChildNodes;
                if (nodeListLayers != null && nodeListLayers.Count > 0)
                {
                    for(var i = 0; i < nodeListLayers.Count; i++)
                    {
                        XmlNode nodelayer = nodeListLayers[i];
                        
                    }
                }
                

            }catch(Exception ex)
            {

            }
        }
    }
}
