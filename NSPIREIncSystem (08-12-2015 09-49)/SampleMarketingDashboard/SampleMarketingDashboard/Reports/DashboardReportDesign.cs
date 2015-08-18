using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using DevExpress.XtraCharts;

namespace NSPIREIncSystem.Reports
{
    public partial class DashboardReportDesign : DevExpress.XtraReports.UI.XtraReport
    {
        public static List<Series> seriesList1 = new List<Series>();
        public static List<Series> seriesList2 = new List<Series>();
        public static List<Series> seriesList3 = new List<Series>();
        public static XRLabel lblActive = new XRLabel();
        public static XRLabel lblEngaged = new XRLabel();
        public static XRLabel lblNotActive = new XRLabel();
        DashboardReportData data = new DashboardReportData();

        public DashboardReportDesign()
        {
            InitializeComponent();
        }

        private void xrChart1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            foreach (var series in seriesList1) { xrChart1.Series.Add(series); }
            xrChart1.Legend.AlignmentVertical = LegendAlignmentVertical.Top;
            xrChart1.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.LeftOutside;
            xrChart1.AppearanceName = "Gray";
        }

        private void xrChart2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            foreach (var series in seriesList2) { xrChart2.Series.Add(series); }
            xrChart2.Legend.AlignmentVertical = LegendAlignmentVertical.Top;
            xrChart2.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.LeftOutside;
            xrChart2.AppearanceName = "Gray";
        }

        private void xrChart3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            foreach (var series in seriesList3) { xrChart3.Series.Add(series); }
            xrChart3.Legend.AlignmentVertical = LegendAlignmentVertical.Top;
            xrChart3.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.LeftOutside;
            xrChart3.AppearanceName = "Gray";
        }

        private void xrLabel6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel6.Text = lblActive.Text;
        }

        private void xrLabel8_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel8.Text = lblEngaged.Text;
        }

        private void xrLabel10_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel10.Text = lblNotActive.Text;
        }
    }
}
