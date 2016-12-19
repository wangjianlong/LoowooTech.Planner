using DevExpress.XtraBars;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraBars.Ribbon;
using LoowooTech.Planner.Commands;
using LoowooTech.Planner.Winforms;
using LoowooTech.Planner.WorkBench.Logs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace LoowooTech.Planner.WorkBench.UI
{
    /// <summary>
    /// 界面初始化工具
    /// </summary>
    internal class UIIniter
    {
        private const string AssemblyNameOfDevExpress = "DevExpress.XtraBars";


        private string _XmlConfigFilePath { get; set; }
        /// <summary>
        /// 配置文件
        /// </summary>
        public string XMLConfigFilePath { get { return _XmlConfigFilePath; }set { _XmlConfigFilePath = value; } }


        public Form CreateUI()
        {
            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.Skins.SkinManager.EnableMdiFormSkins();
            FormMain frm = new FormMain();
            try
            {
                frm.SuspendLayout();
                frm.ResumeLayout();
                frm.IsMdiContainer = true;

                string configFile = XMLConfigFilePath;
                if (System.IO.File.Exists(configFile))
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(configFile);

                    frm.Name = "MainForm";
                    frm.Text = GetAttribute(xmlDoc, "/Workbench/RibbonControl", "Text");

                    CreateRibbonControl(frm);
                    CreatePage(WorkBench.RibbonControl, xmlDoc);

                }

            }catch(Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
                LogManager.Log.LogError(ex.ToString());
            }
            return frm;
        }

        /// <summary>
        /// 作用：从RibbonControl中添加完善分页
        /// 作者：汪建龙
        /// 编写时间：2016年12月19日15:26:32
        /// </summary>
        /// <param name="ribbonControl"></param>
        /// <param name="xmlDocument"></param>
        private void CreatePage(RibbonControl ribbonControl,XmlDocument xmlDocument)
        {
            XmlNode node = xmlDocument.SelectSingleNode("/Workbench/RibbonControl/Pages");
            if (node == null)
            {
                return;
            }
            string load = GetAttribute(node, "Load");
            if (string.IsNullOrEmpty(load) == false && load.ToUpper().IndexOf("F") > -1)
            {
                return;
            }

            XmlNodeList xmlNodeListPageItem = node.SelectNodes("PageItem");
            if (xmlNodeListPageItem == null)
            {
                return;
            }

            for(var i = 0; i < xmlNodeListPageItem.Count; i++)
            {
                XmlNode xmlNodePageItem = xmlNodeListPageItem.Item(i);
                RibbonPage ribbonPage = CreateItem(ribbonControl, xmlNodePageItem) as RibbonPage;
                if (ribbonPage != null)
                {
                    ribbonControl.Pages.Add(ribbonPage);
                }
            }
        }

        /// <summary>
        /// 作用：根据配置文件的不同类型，创建不同类型的RibbonControl中的对象
        /// 作者：汪建龙
        /// 编写时间：2016年12月19日15:30:24
        /// </summary>
        /// <param name="ribbonControl"></param>
        /// <param name="xmlNode"></param>
        /// <returns></returns>
        private object CreateItem(RibbonControl ribbonControl,XmlNode xmlNode)
        {
            if (xmlNode == null || xmlNode.NodeType == XmlNodeType.Comment)
            {
                return null;
            }

            string type = GetAttribute(xmlNode, "Type");
            if (string.IsNullOrEmpty(type))
            {
                return null;
            }

            object obj = null;
            switch (type)
            {
                case "Ribbon.RibbonPage":
                    obj = CreateItemRibbonPage(ribbonControl, xmlNode);
                    break;
                case "Ribbon.RibbonPageGroup":
                    obj = CreateItemRibbonPageGroup(ribbonControl, xmlNode);
                    break;
                case "BarButtonItem":

                    break;
            }
            return obj;
        }

        private object CreateItemBarButtonItem(XmlNode xmlNode)
        {
            if (xmlNode == null || xmlNode.NodeType == XmlNodeType.Comment)
            {
                return null;
            }
            try
            {
                BarButtonItem barButtonItem = CreateInstance(AssemblyNameOfDevExpress, "BarButtonItem") as BarButtonItem;
                if (barButtonItem == null)
                {
                    return null;
                }

                barButtonItem.Name = GetAttribute(xmlNode, "Name");
                barButtonItem.Caption = GetAttribute(xmlNode, "Text");
                var strVisible = GetAttribute(xmlNode, "Visible");
                bool visible = true;
                if (string.IsNullOrEmpty(strVisible) == false && strVisible.ToUpper().IndexOf("F") > -1)
                {
                    visible = false;
                }
                barButtonItem.Visibility = visible ? BarItemVisibility.Always : BarItemVisibility.Never;

                bool largeGlyph = false;
                if(bool.TryParse(GetAttribute(xmlNode,"LargeGlyph"),out largeGlyph) && largeGlyph)
                {
                    barButtonItem.RibbonStyle = RibbonItemStyles.Large;
                  
                }

                return barButtonItem;

            }catch(Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
                return null;
            }
        }

        /// <summary>
        /// 作用：加载图片  返回Image
        /// 作者：汪建龙
        /// 编写时间：2016年12月19日16:56:56
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        private Image LoadImageFromResource(string resourceName)
        {
            try
            {
                //首先从文件系统中搜索资源文件
                string filePath = resourceName;
                if (System.IO.File.Exists(filePath))
                    return System.Drawing.Image.FromFile(filePath);

                filePath = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, resourceName);
                if (File.Exists(filePath))
                    return System.Drawing.Image.FromFile(filePath);

                filePath = Path.Combine(System.Windows.Forms.Application.StartupPath, "Images");
                filePath = Path.Combine(filePath, resourceName);
                if (File.Exists(filePath))
                    return System.Drawing.Image.FromFile(filePath);

                string filePath2 = filePath + ".jpg";
                if (File.Exists(filePath2))
                    return System.Drawing.Image.FromFile(filePath2);

                filePath2 = filePath + ".bmp";
                if (File.Exists(filePath2))
                    return System.Drawing.Image.FromFile(filePath2);

                filePath2 = filePath + ".png";
                if (File.Exists(filePath2))
                    return System.Drawing.Image.FromFile(filePath2);

                filePath2 = filePath + ".gif";
                if (File.Exists(filePath2))
                    return System.Drawing.Image.FromFile(filePath2);


                //如果指定了类型名，则从类型名所在的命名空间中查找
                int index = resourceName.LastIndexOf(",");
                if (index != -1)
                {
                    string strType = resourceName.Substring(0, index).Trim();
                    string resource = resourceName.Substring(index + 1, resourceName.Length - index - 1).Trim();
                    Type type = Type.GetType(strType);
                    if (type != null)
                    {
                        Stream stream = type.Assembly.GetManifestResourceStream(type, resource);
                        if (stream != null)
                        {
                            return System.Drawing.Image.FromStream(stream);
                        }
                    }
                }

                //从已经载入的程序集中搜索资源
                Assembly[] allAssemblies = AppDomain.CurrentDomain.GetAssemblies();
                foreach (Assembly assembly in allAssemblies)
                {
                    Stream stream = assembly.GetManifestResourceStream(resourceName);
                    if (stream != null)
                    {
                        return System.Drawing.Image.FromStream(stream);
                    }
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
            }

            return null;
        }

        /// <summary>
        /// 作用：创建小组 分页下面的小组
        /// 作者：汪建龙
        /// 编写时间：2016年12月19日16:52:41
        /// </summary>
        /// <param name="ribbonControl"></param>
        /// <param name="xmlNode"></param>
        /// <returns></returns>
        private object CreateItemRibbonPageGroup(RibbonControl ribbonControl,XmlNode xmlNode)
        {
            if (xmlNode == null || xmlNode.NodeType == XmlNodeType.Comment || xmlNode.Attributes == null)
            {
                return null;
            }
            object obj = CreateInstance(AssemblyNameOfDevExpress, "Ribbon.RibbonPageGroup");
            RibbonPageGroup ribbonPageGroup = obj as RibbonPageGroup;
            if (ribbonPageGroup == null)
            {
                return null;
            }

            ribbonPageGroup.Name = GetAttribute(xmlNode, "Name");
            ribbonPageGroup.Text = GetAttribute(xmlNode, "Text");
            ribbonPageGroup.Visible = GetAttribute(xmlNode, "Visible").ToUpper() == "FALSE" ? false : true;
            ribbonPageGroup.ShowCaptionButton = false;

            XmlNodeList nodeListSubPageItems = xmlNode.ChildNodes;

            foreach(XmlNode nodeSubPageItem in nodeListSubPageItems)
            {
                object obj_node = CreateItem(ribbonControl, nodeSubPageItem);
                BarButtonItem item = obj_node as BarButtonItem;

                if(item!=null&&item.Tag is IUICommand)
                {
                    IUICommand uiCmd = item.Tag as IUICommand;
                    ribbonPageGroup.ItemLinks.Add(item, uiCmd.Group);
                    ribbonControl.Items.Add(item);
                }
                else
                {
                    BarSubItem subItem = obj_node as BarSubItem;
                    if (subItem != null)
                    {
                        ribbonPageGroup.ItemLinks.Add(subItem);
                        ribbonControl.Items.Add(subItem);
                    }
                    XmlNodeList subListItems = nodeSubPageItem.ChildNodes;
                    foreach(XmlNode subNodeSubItem in subListItems)
                    {
                        BarButtonItem item2 = CreateItem(ribbonControl, subNodeSubItem) as BarButtonItem;
                        if(item2!=null&&item2.Tag is IUICommand)
                        {
                            IUICommand uiCmd2 = item2.Tag as IUICommand;
                            subItem.ItemLinks.Add(item2, uiCmd2.Group);
                            ribbonControl.Items.Add(item2);
                        }
                    }

                }

            }

            return obj;

        }

        /// <summary>
        /// 作用：创建分页  并返回分页对象RibbonPage
        /// 作者：汪建龙
        /// 编写时间：2016年12月19日15:28:47
        /// 
        /// </summary>
        /// <param name="ribbonControl"></param>
        /// <param name="xmlNode"></param>
        /// <returns></returns>
        private object CreateItemRibbonPage(RibbonControl ribbonControl,XmlNode xmlNode)
        {
            if (xmlNode == null || xmlNode.NodeType == XmlNodeType.Comment || xmlNode.Attributes == null)
            {
                return null;
            }

            object obj = CreateInstance(AssemblyNameOfDevExpress, "Ribbon.RibbonPage") as RibbonPage;
            if (obj == null)
            {
                return null;
            }
            RibbonPage ribbonPage = obj as RibbonPage;
            ribbonPage.Name = GetAttribute(xmlNode, "Name");
            ribbonPage.Text = GetAttribute(xmlNode, "Text");
            bool visible = true;
            string strVisible = GetAttribute(xmlNode, "Visiable");
            if (strVisible.ToUpper().IndexOf("F") > -1)
            {
                visible = false;
            }
            ribbonPage.Visible = visible;
            XmlNodeList nodeListSubPageItems = xmlNode.ChildNodes;
            foreach(XmlNode nodeSubPageItem in nodeListSubPageItems)
            {
                RibbonPageGroup group = CreateItem(ribbonControl, nodeSubPageItem) as RibbonPageGroup;
                if (group != null)
                {
                    ribbonPage.Groups.Add(group);
                }
            }

            return obj;

        }
        /// <summary>
        /// 作用：实例化
        /// 作者：汪建龙
        /// 编写时间：2016年12月19日14:15:50
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        private object CreateInstance(string assemblyName, string className)
        {
            try
            {
                string createName = assemblyName == "" ? className : assemblyName + "." + className;
                Assembly ass = Assembly.Load(assemblyName);
                Type type = ass.GetType(createName);
                object obj = Activator.CreateInstance(type);
                return obj;

            }catch(Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("在程序集" + assemblyName + "中创建类" + className + "的实例失败，详细信息：" + ex, "创建实例");
                return null;
            }
        }
        /// <summary>
        /// 作用：创建RibbonControl
        /// 作者：汪建龙
        /// 编写时间：2016年12月19日12:45:18
        /// </summary>
        /// <param name="form"></param>
        private void CreateRibbonControl(FormMain form)
        {
            RibbonControl ribbonControl = new RibbonControl();
            WorkBench.RibbonControl = ribbonControl;
            ribbonControl.Name = "Ribbon";
            ribbonControl.ContextMenu = null;
            ribbonControl.ContextMenuStrip = null;
            ribbonControl.Minimized = false;
            ribbonControl.ShowToolbarCustomizeItem = false;
            ribbonControl.ShowCategoryInCaption = false;
            ribbonControl.ShowPageHeadersMode = ShowPageHeadersMode.ShowOnMultiplePages;
            ribbonControl.ShowToolbarCustomizeItem = false;

            string imageFile = System.IO.Path.Combine(Application.StartupPath, "Images", "AppIcon.png");
            if (System.IO.File.Exists(imageFile))
            {
                ribbonControl.ApplicationIcon = new System.Drawing.Bitmap(imageFile);
            }

            UIUpdater updater = new UIUpdater();
            ribbonControl.MouseMove += new MouseEventHandler(updater.ribbonControl_MouseMove);

            form.Controls.Add(ribbonControl);
        }
        /// <summary>
        /// 作用：获取XmlDocument属性值
        /// 作者：汪建龙
        /// 编写时间：2016年12月19日12:32:40
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="path"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        private string GetAttribute(XmlDocument xmlDoc,string path,string attribute)
        {
            try
            {
                XmlNode node = xmlDoc.SelectSingleNode(path);
                if (node != null && node.Attributes[attribute] != null)
                {
                    return node.Attributes[attribute].Value;
                }

            }catch(Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
            }
            return string.Empty;
        }
        private string GetAttribute(XmlNode node,string attributeName)
        {
            try
            {
                if (node != null && node.Attributes != null && node.Attributes.Count > 0 && node.Attributes[attributeName] != null)
                {
                    return node.Attributes[attributeName].Value;
                }
            }catch(Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.ToString());
            }
            return string.Empty;
        }
    }
}
