using ESRI.ArcGIS.Controls;
using LoowooTech.Planner.Controls;
using LoowooTech.Planner.WorkBench.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LoowooTech.Planner.WorkBench.Forms
{
    public partial class SketchForm : DevExpress.XtraEditors.XtraForm
    {

        private MapOperator _mapOperator { get; set; }
        public MapOperator MapOperator { get { return _mapOperator; }set { _mapOperator = value; } }
        private MapControlSelectionEnv _selectionEnv { get; set; }
        public MapControlSelectionEnv SelectionEnv { get { if (_selectionEnv == null) { _selectionEnv = new MapControlSelectionEnv(); } return _selectionEnv; } set { _selectionEnv = value; } }
        public SketchForm()
        {
            InitializeComponent();
        }

        private void timerLoad_Tick(object sender, EventArgs e)
        {

        }

        private void SketchForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.mapMain.ShowScrollbars = false;
                this.mapMain.Appearance = esriControlsAppearance.esriFlat;
                this.mapMain.AutoKeyboardScrolling = true;
                this.mapMain.AutoMouseWheel = true;
                this.mapMain.BorderStyle = esriControlsBorderStyle.esriNoBorder;
                this.mapMain.ShowScrollbars = false;

                _mapOperator = new MapOperator();
                _mapOperator.Map = mapMain.Map;

                label1.Text = "正在加载数据，请稍等.......";


                timerLoad.Interval = 1;
                WorkBench.SetSelectedPage("PageSJGL");
                


            }catch(Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
                throw new Exception(ex.ToString());
            }
        }

        private void mapMain_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            try
            {
                double xTemp = Math.Round(e.mapX, 4);
                double yTemp = Math.Round(e.mapY, 4);
                WorkBench.SetStatusBarValue("PointXY", string.Format("X={0},Y={1}", xTemp.ToString("f3"), yTemp.ToString("f3")));


            }catch(Exception ex)
            {
                WorkBench.SetStatusBarValue("PointXY", "未知错误");
                System.Diagnostics.Trace.Write(ex);
            }
        }
    }
}
