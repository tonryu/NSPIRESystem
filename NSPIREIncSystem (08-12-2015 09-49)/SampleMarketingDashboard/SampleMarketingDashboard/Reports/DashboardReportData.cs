using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSPIREIncSystem.Reports
{
    class DashboardReportData
    {
        public DashboardReportData()  { }
        public string ReportHeader { get; set; }
        public string ReportTitle { get; set; }
        public string LeadsActive { get; set; }
        public string LeadsEngaged { get; set; }
        public string LeadsDidNotContinue { get; set; }
        public string OverallLeads { get; set; }
        public List<DashboardReportDetail> details { get; set; }

    }
    class DashboardReportDetail
    {
        public DashboardReportDetail() { }
       
    }
}
