namespace NSPIREIncSystem.Reports
{
    partial class DashboardReportDesign
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

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.PieSeriesLabel pieSeriesLabel1 = new DevExpress.XtraCharts.PieSeriesLabel();
            DevExpress.XtraCharts.PieSeriesView pieSeriesView1 = new DevExpress.XtraCharts.PieSeriesView();
            DevExpress.XtraCharts.PieSeriesLabel pieSeriesLabel2 = new DevExpress.XtraCharts.PieSeriesLabel();
            DevExpress.XtraCharts.PieSeriesView pieSeriesView2 = new DevExpress.XtraCharts.PieSeriesView();
            DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel1 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
            DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel2 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DashboardReportDesign));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.lblLeadsPerYear = new DevExpress.XtraReports.UI.XRLabel();
            this.lblLeadsPerSalesStage = new DevExpress.XtraReports.UI.XRLabel();
            this.lblLeadsPerMonth = new DevExpress.XtraReports.UI.XRLabel();
            this.lblOverallLeads = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrChart3 = new DevExpress.XtraReports.UI.XRChart();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrChart2 = new DevExpress.XtraReports.UI.XRChart();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrChart1 = new DevExpress.XtraReports.UI.XRChart();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.xrChart3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrChart2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrChart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblLeadsPerYear,
            this.lblLeadsPerSalesStage,
            this.lblLeadsPerMonth,
            this.lblOverallLeads,
            this.xrLabel11,
            this.xrLabel10,
            this.xrLabel9,
            this.xrLabel8,
            this.xrLabel7,
            this.xrLabel6,
            this.xrLabel5,
            this.xrChart3,
            this.xrLabel4,
            this.xrChart2,
            this.xrLabel3,
            this.xrChart1,
            this.xrLabel2});
            this.Detail.HeightF = 553.1251F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // lblLeadsPerYear
            // 
            this.lblLeadsPerYear.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TotalLeadsYear", "Total : {0}")});
            this.lblLeadsPerYear.Font = new System.Drawing.Font("Arial", 9.75F);
            this.lblLeadsPerYear.LocationFloat = new DevExpress.Utils.PointFloat(745.8333F, 23.54167F);
            this.lblLeadsPerYear.Name = "lblLeadsPerYear";
            this.lblLeadsPerYear.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 5, 5, 100F);
            this.lblLeadsPerYear.SizeF = new System.Drawing.SizeF(244.1667F, 23F);
            this.lblLeadsPerYear.StylePriority.UseFont = false;
            this.lblLeadsPerYear.StylePriority.UsePadding = false;
            this.lblLeadsPerYear.StylePriority.UseTextAlignment = false;
            this.lblLeadsPerYear.Text = "lblLeadsPerYear";
            this.lblLeadsPerYear.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lblLeadsPerSalesStage
            // 
            this.lblLeadsPerSalesStage.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TotalLeadsSalesStage", "Total : {0}")});
            this.lblLeadsPerSalesStage.Font = new System.Drawing.Font("Arial", 9.75F);
            this.lblLeadsPerSalesStage.LocationFloat = new DevExpress.Utils.PointFloat(256.875F, 283.3333F);
            this.lblLeadsPerSalesStage.Name = "lblLeadsPerSalesStage";
            this.lblLeadsPerSalesStage.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 5, 5, 100F);
            this.lblLeadsPerSalesStage.SizeF = new System.Drawing.SizeF(192.7084F, 23F);
            this.lblLeadsPerSalesStage.StylePriority.UseFont = false;
            this.lblLeadsPerSalesStage.StylePriority.UsePadding = false;
            this.lblLeadsPerSalesStage.StylePriority.UseTextAlignment = false;
            this.lblLeadsPerSalesStage.Text = "lblLeadsPerSalesStage";
            this.lblLeadsPerSalesStage.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lblLeadsPerMonth
            // 
            this.lblLeadsPerMonth.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TotalLeadsMonth", "Total : {0}")});
            this.lblLeadsPerMonth.Font = new System.Drawing.Font("Arial", 9.75F);
            this.lblLeadsPerMonth.LocationFloat = new DevExpress.Utils.PointFloat(256.875F, 23.54169F);
            this.lblLeadsPerMonth.Name = "lblLeadsPerMonth";
            this.lblLeadsPerMonth.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 5, 5, 100F);
            this.lblLeadsPerMonth.SizeF = new System.Drawing.SizeF(192.7084F, 23F);
            this.lblLeadsPerMonth.StylePriority.UseFont = false;
            this.lblLeadsPerMonth.StylePriority.UsePadding = false;
            this.lblLeadsPerMonth.StylePriority.UseTextAlignment = false;
            this.lblLeadsPerMonth.Text = "lblLeadsPerMonth";
            this.lblLeadsPerMonth.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lblOverallLeads
            // 
            this.lblOverallLeads.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TotalLeads", "Total : {0}")});
            this.lblOverallLeads.Font = new System.Drawing.Font("Arial", 9.75F);
            this.lblOverallLeads.LocationFloat = new DevExpress.Utils.PointFloat(745.8333F, 283.3333F);
            this.lblOverallLeads.Name = "lblOverallLeads";
            this.lblOverallLeads.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 5, 5, 100F);
            this.lblOverallLeads.SizeF = new System.Drawing.SizeF(244.1664F, 23F);
            this.lblOverallLeads.StylePriority.UseFont = false;
            this.lblOverallLeads.StylePriority.UsePadding = false;
            this.lblOverallLeads.StylePriority.UseTextAlignment = false;
            this.lblOverallLeads.Text = "lblOverallLeads";
            this.lblOverallLeads.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrLabel11
            // 
            this.xrLabel11.Font = new System.Drawing.Font("Arial", 9.75F);
            this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(837.9164F, 416.5833F);
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel11.SizeF = new System.Drawing.SizeF(152.0834F, 22.99997F);
            this.xrLabel11.StylePriority.UseFont = false;
            this.xrLabel11.StylePriority.UseTextAlignment = false;
            this.xrLabel11.Text = "of Leads are discontinued";
            this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel10
            // 
            this.xrLabel10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "LeadsDidNotContinue")});
            this.xrLabel10.Font = new System.Drawing.Font("Arial", 50F);
            this.xrLabel10.ForeColor = System.Drawing.Color.Red;
            this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(837.9164F, 306.3333F);
            this.xrLabel10.Name = "xrLabel10";
            this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 5, 5, 100F);
            this.xrLabel10.SizeF = new System.Drawing.SizeF(152.0834F, 110.25F);
            this.xrLabel10.StylePriority.UseFont = false;
            this.xrLabel10.StylePriority.UseForeColor = false;
            this.xrLabel10.StylePriority.UsePadding = false;
            this.xrLabel10.StylePriority.UseTextAlignment = false;
            this.xrLabel10.Text = "xrLabel10";
            this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrLabel10.WordWrap = false;
            this.xrLabel10.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabel10_BeforePrint);
            // 
            // xrLabel9
            // 
            this.xrLabel9.Font = new System.Drawing.Font("Arial", 9.75F);
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(667.7083F, 416.5833F);
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 5, 5, 100F);
            this.xrLabel9.SizeF = new System.Drawing.SizeF(152.0834F, 45.91669F);
            this.xrLabel9.StylePriority.UseFont = false;
            this.xrLabel9.StylePriority.UsePadding = false;
            this.xrLabel9.StylePriority.UseTextAlignment = false;
            this.xrLabel9.Text = "of Leads are engaged clients";
            this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel8
            // 
            this.xrLabel8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "LeadsEngaged")});
            this.xrLabel8.Font = new System.Drawing.Font("Arial", 50F);
            this.xrLabel8.ForeColor = System.Drawing.Color.Khaki;
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(667.7083F, 306.3331F);
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 5, 5, 100F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(152.0834F, 110.2502F);
            this.xrLabel8.StylePriority.UseFont = false;
            this.xrLabel8.StylePriority.UseForeColor = false;
            this.xrLabel8.StylePriority.UsePadding = false;
            this.xrLabel8.StylePriority.UseTextAlignment = false;
            this.xrLabel8.Text = "xrLabel8";
            this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrLabel8.WordWrap = false;
            this.xrLabel8.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabel8_BeforePrint);
            // 
            // xrLabel7
            // 
            this.xrLabel7.Font = new System.Drawing.Font("Arial", 9.75F);
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(498.9583F, 416.5833F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 5, 5, 100F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(152.0835F, 22.99994F);
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.StylePriority.UsePadding = false;
            this.xrLabel7.StylePriority.UseTextAlignment = false;
            this.xrLabel7.Text = "of Leads are active";
            this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel6
            // 
            this.xrLabel6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "LeadsActive")});
            this.xrLabel6.Font = new System.Drawing.Font("Arial", 50F);
            this.xrLabel6.ForeColor = System.Drawing.Color.DarkGreen;
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(498.9583F, 306.3333F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 5, 5, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(152.0835F, 110.25F);
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.StylePriority.UseForeColor = false;
            this.xrLabel6.StylePriority.UsePadding = false;
            this.xrLabel6.StylePriority.UseTextAlignment = false;
            this.xrLabel6.Text = "xrLabel6";
            this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrLabel6.WordWrap = false;
            this.xrLabel6.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabel6_BeforePrint);
            // 
            // xrLabel5
            // 
            this.xrLabel5.Font = new System.Drawing.Font("Arial", 13F);
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(498.9583F, 269.7916F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 5, 5, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(246.875F, 36.54167F);
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UsePadding = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            this.xrLabel5.Text = "Overall Leads";
            this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrChart3
            // 
            this.xrChart3.BorderColor = System.Drawing.Color.Black;
            this.xrChart3.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrChart3.LocationFloat = new DevExpress.Utils.PointFloat(10.00004F, 306.3333F);
            this.xrChart3.Name = "xrChart3";
            pieSeriesLabel1.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
            pieSeriesLabel1.TextPattern = "{A:G}: {VP:P2}";
            series1.Label = pieSeriesLabel1;
            series1.Name = "Series1";
            series1.View = pieSeriesView1;
            this.xrChart3.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            pieSeriesLabel2.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
            pieSeriesLabel2.TextPattern = "{VP:G4}";
            this.xrChart3.SeriesTemplate.Label = pieSeriesLabel2;
            this.xrChart3.SeriesTemplate.View = pieSeriesView2;
            this.xrChart3.SizeF = new System.Drawing.SizeF(439.5833F, 236.7917F);
            this.xrChart3.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrChart3_BeforePrint);
            // 
            // xrLabel4
            // 
            this.xrLabel4.Font = new System.Drawing.Font("Arial", 13F);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 269.7916F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 5, 5, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(246.875F, 36.54167F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UsePadding = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.Text = "Leads Per Sales Stage Status";
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrChart2
            // 
            this.xrChart2.BorderColor = System.Drawing.Color.Black;
            this.xrChart2.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrChart2.LocationFloat = new DevExpress.Utils.PointFloat(498.9583F, 46.54169F);
            this.xrChart2.Name = "xrChart2";
            this.xrChart2.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            sideBySideBarSeriesLabel1.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
            this.xrChart2.SeriesTemplate.Label = sideBySideBarSeriesLabel1;
            this.xrChart2.SizeF = new System.Drawing.SizeF(491.0417F, 200F);
            this.xrChart2.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrChart2_BeforePrint);
            // 
            // xrLabel3
            // 
            this.xrLabel3.Font = new System.Drawing.Font("Arial", 13F);
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(498.9583F, 10.00001F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 5, 5, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(246.875F, 36.54167F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UsePadding = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.Text = "Leads Per Year";
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrChart1
            // 
            this.xrChart1.BorderColor = System.Drawing.Color.Black;
            this.xrChart1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrChart1.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 46.54169F);
            this.xrChart1.Name = "xrChart1";
            this.xrChart1.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            sideBySideBarSeriesLabel2.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
            this.xrChart1.SeriesTemplate.Label = sideBySideBarSeriesLabel2;
            this.xrChart1.SizeF = new System.Drawing.SizeF(439.5834F, 200F);
            this.xrChart1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrChart1_BeforePrint);
            // 
            // xrLabel2
            // 
            this.xrLabel2.Font = new System.Drawing.Font("Arial", 13F);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 10.00001F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 5, 5, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(246.875F, 36.54167F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UsePadding = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "Leads Per Month";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 48F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 49.00001F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel12,
            this.xrPictureBox1,
            this.xrLabel1});
            this.PageHeader.HeightF = 150.0833F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrLabel12
            // 
            this.xrLabel12.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ReportHeader")});
            this.xrLabel12.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold);
            this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(336.4584F, 25.06024F);
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 5, 5, 100F);
            this.xrLabel12.SizeF = new System.Drawing.SizeF(499.9749F, 72.00703F);
            this.xrLabel12.StylePriority.UseFont = false;
            this.xrLabel12.StylePriority.UsePadding = false;
            this.xrLabel12.StylePriority.UseTextAlignment = false;
            this.xrLabel12.Text = "xrLabel12";
            this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox1.Image")));
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(171.875F, 0F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(164.5834F, 140.0833F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
            // 
            // xrLabel1
            // 
            this.xrLabel1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ReportTitle")});
            this.xrLabel1.Font = new System.Drawing.Font("Arial", 9.75F);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(336.4584F, 97.06727F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 5, 5, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(499.9749F, 28.67117F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UsePadding = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "xrLabel1";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(NSPIREIncSystem.Reports.DashboardReportData);
            // 
            // DashboardReportDesign
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader});
            this.DataSource = this.bindingSource1;
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(50, 50, 48, 49);
            this.PageHeight = 850;
            this.PageWidth = 1100;
            this.Version = "15.1";
            ((System.ComponentModel.ISupportInitialize)(pieSeriesLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrChart3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrChart2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrChart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel5;
        private DevExpress.XtraReports.UI.XRChart xrChart3;
        private DevExpress.XtraReports.UI.XRLabel xrLabel4;
        private DevExpress.XtraReports.UI.XRChart xrChart2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel3;
        private DevExpress.XtraReports.UI.XRChart xrChart1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel11;
        private DevExpress.XtraReports.UI.XRLabel xrLabel10;
        private DevExpress.XtraReports.UI.XRLabel xrLabel9;
        private DevExpress.XtraReports.UI.XRLabel xrLabel8;
        private DevExpress.XtraReports.UI.XRLabel xrLabel7;
        private DevExpress.XtraReports.UI.XRLabel xrLabel6;
        private DevExpress.XtraReports.UI.XRLabel lblLeadsPerYear;
        private DevExpress.XtraReports.UI.XRLabel lblLeadsPerSalesStage;
        private DevExpress.XtraReports.UI.XRLabel lblLeadsPerMonth;
        private DevExpress.XtraReports.UI.XRLabel lblOverallLeads;
        private DevExpress.XtraReports.UI.XRLabel xrLabel12;
        private DevExpress.XtraReports.UI.XRPictureBox xrPictureBox1;
    }
}
