namespace LoowooTech.Planner.WorkBench.Forms
{
    partial class SketchForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SketchForm));
            this.label1 = new System.Windows.Forms.Label();
            this.mapMain = new ESRI.ArcGIS.Controls.AxMapControl();
            this.timerLoad = new System.Windows.Forms.Timer(this.components);
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.cmRasterMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.mapMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Window;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(834, 521);
            this.label1.TabIndex = 8;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mapMain
            // 
            this.mapMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapMain.Location = new System.Drawing.Point(0, 0);
            this.mapMain.Name = "mapMain";
            this.mapMain.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("mapMain.OcxState")));
            this.mapMain.Size = new System.Drawing.Size(834, 521);
            this.mapMain.TabIndex = 0;
            this.mapMain.OnMouseMove += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseMoveEventHandler(this.mapMain_OnMouseMove);
            this.mapMain.OnSelectionChanged += new System.EventHandler(this.mapMain_OnSelectionChanged);
            this.mapMain.OnExtentUpdated += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnExtentUpdatedEventHandler(this.mapMain_OnExtentUpdated);
            // 
            // timerLoad
            // 
            this.timerLoad.Interval = 1;
            this.timerLoad.Tick += new System.EventHandler(this.timerLoad_Tick);
            // 
            // dockManager1
            // 
            this.dockManager1.DockingOptions.FloatOnDblClick = false;
            this.dockManager1.DockingOptions.HideImmediatelyOnAutoHide = true;
            this.dockManager1.DockingOptions.ShowMaximizeButton = false;
            this.dockManager1.Form = this;
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "DevExpress.XtraBars.Navigation.OfficeNavigationBar"});
            // 
            // cmRasterMenu
            // 
            this.cmRasterMenu.Name = "cmRasterMenu";
            this.cmRasterMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // SketchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 521);
            this.ControlBox = false;
            this.Controls.Add(this.mapMain);
            this.Controls.Add(this.label1);
            this.Name = "SketchForm";
            this.Text = "主窗口";
            this.Load += new System.EventHandler(this.SketchForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mapMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ESRI.ArcGIS.Controls.AxMapControl mapMain;
        private System.Windows.Forms.Timer timerLoad;
        private System.Windows.Forms.ContextMenuStrip cmRasterMenu;
        public DevExpress.XtraBars.Docking.DockManager dockManager1;

        private System.Windows.Forms.Label label1;
    }
}