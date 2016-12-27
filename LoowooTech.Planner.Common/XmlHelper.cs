using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace LoowooTech.Planner.Common
{
    public static class XmlHelper
    {
        private static XmlDocument _xmlDocument { get; set; }
        public static void Load(string xmlFile)
        {
            _xmlDocument = new XmlDocument();
            _xmlDocument.Load(xmlFile);
        }
        /// <summary>
        /// 作用：指定xml路径，获取InnerText，调用前  如果xml文件发生改变，需调用load(xmlfile);
        /// 作者：汪建龙
        /// 编写时间：2016年12月27日17:19:48
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetValue(string path)
        {
            if (_xmlDocument == null)
            {
                return string.Empty;
            }
            string result = string.Empty;
            try
            {
                XmlNode xn = _xmlDocument.SelectSingleNode(path);
                result = xn.InnerText;
            }
            catch
            {

            }
            return result;
        }


        /// <summary>
        /// 作用：获取某一属性的值
        /// 作者：汪建龙
        /// 编写时间：2016年12月23日12:05:56
        /// </summary>
        /// <param name="xmlNode"></param>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        public static string GetAttributeValue(XmlNode xmlNode,string attributeName)
        {
            string attribute = string.Empty;
            try
            {

                if (xmlNode != null 
                    && xmlNode.NodeType != XmlNodeType.Comment 
                    && xmlNode.Attributes != null 
                    && xmlNode.Attributes[attributeName] != null)
                {
                    attribute = xmlNode.Attributes[attributeName].Value;
                }
            }catch(Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
            }
            return attribute;
        }

        
    }
}
