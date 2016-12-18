using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.Planner.WorkBench.UI
{
    public class SysIniter
    {
        private string _XmlConfigFilePath { get; set; }
        /// <summary>
        /// 配置文件
        /// </summary>
        public string XMLConfigFilePath { get { return _XmlConfigFilePath; } set { _XmlConfigFilePath = value; } }

        public void Initer()
        {

        }
    }
}
