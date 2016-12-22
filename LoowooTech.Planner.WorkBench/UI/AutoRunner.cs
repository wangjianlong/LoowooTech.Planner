using LoowooTech.Planner.WorkBench.Commands;
using LoowooTech.Planner.WorkBench.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace LoowooTech.Planner.WorkBench.UI
{
    internal class AutoRunner
    {
        private string _xmlConfigFilePath { get; set; }
        /// <summary>
        /// 配置文件
        /// </summary>
        public string XMLConfigFilePath { get { return _xmlConfigFilePath; }set { _xmlConfigFilePath = value; } }
        public void Start()
        {
            try
            {
                string configfile = XMLConfigFilePath;
                if (System.IO.File.Exists(configfile))
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(configfile);

                    XmlNode xmlNode = xmlDoc.SelectSingleNode("/Workbench/AutoRunCommand");
                    if (xmlNode != null)
                    {
                        XmlNodeList nodes = xmlNode.SelectNodes("CommandClass");
                        foreach(XmlNode node in nodes)
                        {
                            try
                            {
                                string assemblyName = node.Attributes["AssemblyName"].Value;
                                string className = node.Attributes["ClassName"].Value;
                                string parameter = node.Attributes["Parameter"].Value;
                                ITLWCommand cmd = InstanceHelper.CreateInstance(assemblyName, className) as ITLWCommand;
                                if (cmd != null)
                                {
                                    cmd.Init(parameter);
                                    cmd.OnClick();
                                }

                                System.Windows.Forms.Application.DoEvents();

                            }catch(Exception ex)
                            {
                                System.Diagnostics.Trace.WriteLine(ex);
                                //LogManager.Log.LogError(ex.ToString());
                            }
                        }
                    }
                }


            }catch(Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
                //LogManager.Log.LogError(ex.ToString());
            }
        }

    }
}
