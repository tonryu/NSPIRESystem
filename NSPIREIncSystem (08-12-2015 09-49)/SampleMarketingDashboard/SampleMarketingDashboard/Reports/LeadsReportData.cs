using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSPIREIncSystem.LeadManagement.Reports
{
    class LeadsReportData
    {
        public LeadsReportData() { }
        public string ReportHeader { get; set; }
        public string ReportTitle { get; set; }
        public int TotalLeads { get; set; }

        public List<LeadsReportDetail> details { get; set; }
    }
    class LeadsReportDetail
    {
        public LeadsReportDetail() { }
        public int LeadNo { get; set; }
        public string CompanyName {get; set;}
        public string Address {get; set;}
        public string Territory {get; set;}
        public string SalesStageStatus { get; set; }

    }
}
