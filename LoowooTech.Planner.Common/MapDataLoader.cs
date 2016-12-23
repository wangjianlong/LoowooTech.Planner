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

                }

            }catch(Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
            }
        }
    }
}
