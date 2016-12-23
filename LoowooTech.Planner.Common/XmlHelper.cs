using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace LoowooTech.Planner.Common
{
    public static class XmlHelper
    {
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

                if (xmlNode != null && xmlNode.NodeType == XmlNodeType.Comment && xmlNode.Attributes != null && xmlNode.Attributes[attributeName] != null)
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
