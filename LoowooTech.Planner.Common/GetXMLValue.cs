using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace LoowooTech.Planner.Common
{
    /// <summary>
    /// 废弃
    /// </summary>
    public class GetXMLValue
    {
        public string strXMLPath { get; set; }
        public GetXMLValue()
        {
            strXMLPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + @"/config/defaultConfig.xml";
        }

        public GetXMLValue(string filePath)
        {
            strXMLPath = filePath;
        }

        public string GetValue(string path)
        {
            string result = null;
            XmlDocument doc = new XmlDocument();
            try
            {

            }catch(Exception ex)
            {

            }
            return result;
        }

    }
}
