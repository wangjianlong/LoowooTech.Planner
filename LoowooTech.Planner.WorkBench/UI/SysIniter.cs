
using LoowooTech.Planner.WorkBench.Forms;
using LoowooTech.Planner.WorkBench.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
            Form form = null;
            System.Windows.Forms.Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(WorkBench.Application_ThreadException);
            System.AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(WorkBench.CurrentDomain_UnhandledException);

            try
            {
                UIIniter initer = new UIIniter();
                initer.XMLConfigFilePath = XMLConfigFilePath;
                form = initer.CreateUI();
                form.Show();
                WorkBench.MainForm = form;

            }catch(Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
               
            }

            System.Windows.Forms.Application.DoEvents();

            try
            {
                AutoRunner runner = new AutoRunner();
                runner.XMLConfigFilePath = XMLConfigFilePath;
                runner.Start();


            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
            }

            //if(form is FormMain)
            //{
            //}


        }
    }
}
